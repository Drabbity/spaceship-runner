using UnityEngine;

public class BackgroundSpawner : XAxisGenerator
{
    protected override Vector3 CalculateNextPrefabSpawnPosition(GameObject prefabToSpawn)
    {
        Vector3 lastPrefabRightEndPosition = LastSpawnedPrefab.transform.position + GetGameObjectRightEndOffset(LastSpawnedPrefab);
        Vector3 offsetToCenter = GetGameObjectLeftEndOffset(prefabToSpawn);

        return lastPrefabRightEndPosition - offsetToCenter;
    }
}
