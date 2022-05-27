using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class XAxisGenerator : MonoBehaviour
{
    [SerializeField] protected GameObject[] PrefabsToGenerate;
    [SerializeField] protected GameObject StartingPrefab;

    protected GameObject LastSpawnedPrefab = null;
    private float _rightCameraBorder;

    protected virtual void Start()
    {
        AssignPreFrameValues();
        LastSpawnedPrefab = StartingPrefab;

        for (int loop = 0; CanSpawnNextPrefab() && loop < 100; loop++)
        {
            PlaceNextPrefab();
        }
    }

    protected virtual void Update()
    {
        AssignPreFrameValues();

        if (CanSpawnNextPrefab())
        {
            PlaceNextPrefab();
        }
    }

    private void AssignPreFrameValues()
    {
        _rightCameraBorder = GetRightCameraBorder();
    }

    private float GetRightCameraBorder()
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
    }

    protected Vector3 GetGameObjectRightEndOffset(GameObject gameObject)
    {
        var renderer = gameObject.GetComponent<Renderer>();
        return new Vector3(renderer.bounds.size.x / 2, 0, 0);
    }

    protected Vector3 GetGameObjectLeftEndOffset(GameObject gameObject)
    {
        var renderer = gameObject.GetComponent<Renderer>();
        return new Vector3(-renderer.bounds.size.x / 2, 0, 0);
    }


    private bool CanSpawnNextPrefab()
    {
        if (!LastSpawnedPrefab || (LastSpawnedPrefab.transform.position.x + GetGameObjectLeftEndOffset(LastSpawnedPrefab).x) < _rightCameraBorder)
            return true;
        return false;
    }

    private void PlaceNextPrefab()
    {
        GameObject prefabToSpawn = GetRandomPrefab();
        Vector3 prefabToSpawnPosition = CalculateNextPrefabSpawnPosition(prefabToSpawn);

        LastSpawnedPrefab = Instantiate(prefabToSpawn, prefabToSpawnPosition, Quaternion.identity);
    }

    protected abstract Vector3 CalculateNextPrefabSpawnPosition(GameObject prefabToSpawn);

    private GameObject GetRandomPrefab()
    {
        return PrefabsToGenerate[Random.Range(0, PrefabsToGenerate.Length)];
    }
}
