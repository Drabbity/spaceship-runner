using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : XAxisGenerator
{
    [SerializeField] [Range(0, 3)] private float _maxPlatformGap;
    [SerializeField] [Range(-5, 5)] private float _maxPlatformYPosition;
    [SerializeField] [Range(-5, 5)] private float _minPlatformYPosition;
    [SerializeField] private GameObject[] _spikes;
    [SerializeField] [Range(0, 1)]private float _spikeFrequency;

    protected override Vector3 CalculateNextPrefabSpawnPosition(GameObject prefabToSpawn)
    {
        Vector3 lastPrefabRightEndPosition = LastSpawnedPrefab.transform.position + GetGameObjectRightEndOffset(LastSpawnedPrefab);
        Vector3 offset = new Vector3(Random.Range(0, _maxPlatformGap), 0, 0);

        Vector3 newPrefabPosition = lastPrefabRightEndPosition + offset + GetGameObjectRightEndOffset(prefabToSpawn);
        newPrefabPosition.y = Random.Range(_minPlatformYPosition, _maxPlatformYPosition);

        return newPrefabPosition;
    }

    protected override void PlaceNextPrefab()
    {
        base.PlaceNextPrefab();
        GenerateSpikes(LastSpawnedPrefab);
    }

    private void GenerateSpikes(GameObject platform)
    {
        float platformYSize = platform.GetComponent<SpriteRenderer>().bounds.size.y;

        float platformTopYPosition = platform.transform.position.y + platformYSize / 2;
        float platformBottomYPosition = platform.transform.position.y - platformYSize / 2;

        GenerateSpikeRow(platform, platformTopYPosition, false);
        GenerateSpikeRow(platform, platformBottomYPosition, true);
    }

    private void GenerateSpikeRow(GameObject platform, float yPosition, bool isFlipped)
    {
        List<GameObject> spawnedSpikes = new List<GameObject>(0);

        int loopCount = Mathf.RoundToInt(platform.GetComponent<SpriteRenderer>().bounds.size.x * _spikeFrequency);
        for(int i = 0; i < loopCount; i++)
        {
            GameObject spikePrefab = GetRandomPrefab(_spikes);
            float xPosition = CalculateSpikeXPosition(spikePrefab, platform);

            Vector3 prefabSpawnPosition = new Vector3(xPosition, yPosition, 0);

            if (!HasOverlap(spawnedSpikes, spikePrefab, prefabSpawnPosition.x))
            {
                var spawnedSpike = Instantiate(spikePrefab, prefabSpawnPosition, Quaternion.Euler(new Vector3(0, 0, isFlipped ? 180 : 0)), platform.transform);
                spawnedSpikes.Add(spawnedSpike);
            }
        }
    }

    private float CalculateSpikeXPosition(GameObject spikePrefab, GameObject platform)
    {
        (float minXPosition, float maxXPosition) = GetSpikeSpawnXPositionBorders(spikePrefab, platform);

        return Random.Range(minXPosition, maxXPosition);
    }

    private (float, float) GetSpikeSpawnXPositionBorders(GameObject spikePrefab, GameObject platform)
    {
        Vector3 minPosition = platform.transform.position + GetGameObjectLeftEndOffset(platform) - GetGameObjectLeftEndOffset(spikePrefab);
        Vector3 maxPosition = platform.transform.position + GetGameObjectRightEndOffset(platform) - GetGameObjectRightEndOffset(spikePrefab);

        return (minPosition.x, maxPosition.x);
    }

    private bool HasOverlap(List<GameObject> spawnedGameObjects, GameObject prefab, float xPosition)
    {
        bool hasOverlap = false;
        foreach(var spawnedGameObject in spawnedGameObjects)
        {
            if(HasOverlap(spawnedGameObject, spawnedGameObject.transform.position.x, prefab, xPosition))
            {
                hasOverlap = true;
            }
        }

        return hasOverlap;
    }

    private bool HasOverlap(GameObject gameObjectA, float xPositionA, GameObject gameObjectB, float xPositionB)
    {
        if(xPositionA > xPositionB)
        {
            (xPositionA, xPositionB) = (xPositionB, xPositionA);
            (gameObjectA, gameObjectB) = (gameObjectB, gameObjectA);
        }

        return xPositionA + GetGameObjectRightEndOffset(gameObjectA).x > xPositionB + GetGameObjectLeftEndOffset(gameObjectB).x;
    }

}
