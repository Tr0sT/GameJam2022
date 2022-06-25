#nullable enable
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public int MaxHitCount;

    private int _hitCount;
    private Movement _movement = null!;
    
    private Vector3 _lastFrameVelocity;
    private Rigidbody2D _rigidbody2D = null!;

    private void OnEnable()
    {
        _movement = GetComponent<Movement>();
        _lastFrameVelocity = _movement.Direction * _movement.Speed;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        _lastFrameVelocity = _rigidbody2D.velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (var contact in collision.contacts)
        {
            if (contact.collider.CompareTag("Wall"))
            {
                Bounce(collision.contacts[0].normal);
                _hitCount++;
                
                if (_hitCount >= MaxHitCount)
                {
                    Destroy(this);
                    _movement.Direction = Vector2.zero;
                    return;
                }
            }
        }
    }
    
    private void Bounce(Vector3 collisionNormal)
    {
        var speed = _movement.Speed;
        var direction = Vector3.Reflect(_lastFrameVelocity.normalized, collisionNormal);

        _movement.Direction = direction;
    }
}