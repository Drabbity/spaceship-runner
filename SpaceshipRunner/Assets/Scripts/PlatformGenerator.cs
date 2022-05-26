using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] [Range(0, 3)] private float _maxPlatformGap;
    [SerializeField] private GameObject[] _platformPrefabs;
    [SerializeField] private GameObject _startingPlatform;
    [SerializeField] [Range(-5, 5)] private float _maxPlatformYPosition;
    [SerializeField] [Range(-5, 5)] private float _minPlatformYPosition;

    private GameObject _lastPlatform = null;
    private float _rightCameraBorder;

    private void Start()
    {
        AssignPreFrameValues();
        _lastPlatform = _startingPlatform;

        while(IsPlatformNeeded())
        {
            PlaceNextPlatform();
        }
    }

    private void Update()
    {
        AssignPreFrameValues();

        if(IsPlatformNeeded())
        {
            PlaceNextPlatform();
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

    private Vector3 GetPlatformRightEndOffset(GameObject platform)
    {
        var collider = platform.GetComponent<BoxCollider2D>();
        return new Vector3(collider.offset.x + collider.size.x / 2, 0, 0);
    }

    private Vector3 GetPlatformLeftEndOffset(GameObject platform)
    {
        var collider = platform.GetComponent<BoxCollider2D>();
        return new Vector3(collider.offset.x - collider.size.x / 2, 0, 0);
    }

    
    private bool IsPlatformNeeded()
    {
        if (!_lastPlatform || (_lastPlatform.transform.position.x + GetPlatformLeftEndOffset(_lastPlatform).x) < _rightCameraBorder)
            return true;
        return false;
    }
    
    private void PlaceNextPlatform()
    {
        GameObject newPlatform = GetRandomPlatformPrefab();
        Vector3 newPlatformPosition = CalculateNextPlatformPosition(newPlatform);

        _lastPlatform = Instantiate(newPlatform, newPlatformPosition, Quaternion.identity);
    }

    private Vector3 CalculateNextPlatformPosition(GameObject newPlatform)
    {
        Vector3 lastPlatformRightEndPosition = _lastPlatform.transform.position + GetPlatformRightEndOffset(_lastPlatform);
        Vector3 offset = new Vector3(Random.Range(0, _maxPlatformGap), 0, 0);

        Vector3 newPlatformPosition = lastPlatformRightEndPosition + offset + GetPlatformRightEndOffset(newPlatform);
        newPlatformPosition.y = Random.Range(_minPlatformYPosition, _maxPlatformYPosition);
       
        return newPlatformPosition;
    }

    private GameObject GetRandomPlatformPrefab()
    {
        return _platformPrefabs[Random.Range(0, _platformPrefabs.Length)];
    }
}
