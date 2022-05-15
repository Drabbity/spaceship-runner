using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private Vector3 _followDirections;
    [SerializeField] private Vector3 _offsets;
    
    private Vector3 _newPosition;

    private void Update()
    {
        _newPosition = Vector3.Scale(_target.transform.position, _followDirections) + _offsets;
        gameObject.transform.position = _newPosition;
    }
}
