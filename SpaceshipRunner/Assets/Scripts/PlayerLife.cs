using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private LayerMask _obstacleMask;
    [SerializeField] private float _boxCastDistance = 0.05f;
    
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
        if (!IsAlive())
        {
            GameManager.Instance.PlayerDied();
        }
        _lastXPosition = transform.position.x;
        _timer += Time.fixedDeltaTime;
    }

    private bool IsAlive()
    {
        return !(IsStuck() || HasHitObstacle());
    }

    private bool IsStuck()
    {
        return _lastXPosition == transform.position.x && _timer > 1f;
    }

    private bool HasHitObstacle()
    {
        return Physics2D.BoxCast(_collider.bounds.center, _collider.bounds.size / 2, 0, (_playerController.IsFlipped ? Vector2.up : Vector2.down), _boxCastDistance, _obstacleMask).collider;
    }
}
