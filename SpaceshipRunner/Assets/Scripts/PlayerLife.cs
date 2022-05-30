using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private LayerMask _obstacleMask;
    [SerializeField] private float _boxCastDistance = 0.05f;
    [SerializeField] private float minYPosition = -6;
    [SerializeField] private float maxYPosition = 6;
    
    private PlayerController _playerController;
    private BoxCollider2D _collider;  

    private float _lastXPosition = float.MinValue;
    private float _timer = 0f;

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
        _collider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {  
        if (IsDead())
        {
            GameManager.Instance.PlayerDied();
        }
        _lastXPosition = transform.position.x;
        _timer += Time.fixedDeltaTime;
    }

    private bool IsDead()
    {
        return (IsStuck() || HasHitObstacle() || HasFallenOff());
    }

    private bool IsStuck()
    {
        return _lastXPosition == transform.position.x && _timer > 1f;
    }

    private bool HasHitObstacle()
    {
        return Physics2D.BoxCast(_collider.bounds.center, _collider.bounds.size, 0, (_playerController.IsFlipped ? Vector2.up : Vector2.down), _boxCastDistance, _obstacleMask).collider;
    }

    private bool HasFallenOff()
    {
        return (transform.position.y < minYPosition || transform.position.y > maxYPosition);
    }
}
