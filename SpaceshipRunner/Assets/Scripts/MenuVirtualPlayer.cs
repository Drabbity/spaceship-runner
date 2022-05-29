using UnityEngine;

public class MenuVirtualPlayer : MonoBehaviour
{
    [SerializeField] private float _speed = 2;

    private void Update()
    {
        MoveForward();
    }

    private void MoveForward()
    {
        transform.Translate(Vector3.right * _speed * Time.deltaTime);
    }
}
