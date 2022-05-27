using UnityEngine;

public class PlatformGenerator : XAxisGenerator
{
    [SerializeField] [Range(0, 3)] private float _maxPlatformGap;
    [SerializeField] [Range(-5, 5)] private float _maxPlatformYPosition;
    [SerializeField] [Range(-5, 5)] private float _minPlatformYPosition;

    protected override Vector3 CalculateNextPrefabSpawnPosition(GameObject prefabToSpawn)
    {
        Vector3 lastPrefabRightEndPosition = LastSpawnedPrefab.transform.position + GetGameObjectRightEndOffset(LastSpawnedPrefab);
        Vector3 offset = new Vector3(Random.Range(0, _maxPlatformGap), 0, 0);

        Vector3 newPrefabPosition = lastPrefabRightEndPosition + offset + GetGameObjectRightEndOffset(prefabToSpawn);
        newPrefabPosition.y = Random.Range(_minPlatformYPosition, _maxPlatformYPosition);

        return newPrefabPosition;
    }
}
