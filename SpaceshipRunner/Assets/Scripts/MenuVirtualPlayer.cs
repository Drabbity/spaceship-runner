using UnityEngine;

public class MenuVirtualPlayer : MonoBehaviour
{
    [SerializeField] float speed = 2;

    private void Update()
    {
        MoveForward();
    }

    private void MoveForward()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
