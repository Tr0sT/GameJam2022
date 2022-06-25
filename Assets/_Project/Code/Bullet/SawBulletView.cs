#nullable enable
using System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public class SawBulletView : SerializedMonoBehaviour, IBullet
{
    public event Action<IBullet>? OnDestroy;

    [SerializeField] 
    private Rigidbody2D _rigidbody2D = null!;
    
    [NonSerialized, OdinSerialize]
    private SawBulletSettings _sawBulletSettings = null!;

    private Vector3 _lastFrameVelocity;

    private int _hitCount;
    private bool _active;

    public void Init(Vector3 position, Vector3 direction, IBulletSettings bulletSettings)
    {
        _sawBulletSettings = (SawBulletSettings)bulletSettings;
        transform.localPosition = position.WithZ(0);
        _lastFrameVelocity = direction;
        _active = true;

        _rigidbody2D.velocity =  _lastFrameVelocity.normalized * _sawBulletSettings.speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (var contact in collision.contacts)
        {
            if (contact.collider.CompareTag("Enemy") && _active)
            {
                contact.collider.GetComponent<IEnemy>().TakeDamage(_sawBulletSettings.Damage);
                DestroyBullet();
                return;
            }
            if (contact.collider.CompareTag("Wall") && _active)
            {
                Bounce(collision.contacts[0].normal);
                _hitCount++;
                
                if (_hitCount >= _sawBulletSettings.MaxHitCount)
                {
                    _active = false;
                    _rigidbody2D.velocity = Vector2.zero;
                    return;
                }
            }

            if (contact.collider.CompareTag("Player") && !_active)
            {
                //contact.collider.GetComponent<PlayerController>().PickupSaw();
                DestroyBullet();
                return;
            }
        }
    }
    
    private void Bounce(Vector3 collisionNormal)
    {
        var speed = _sawBulletSettings.speed;
        var direction = Vector3.Reflect(_lastFrameVelocity.normalized, collisionNormal);

        _rigidbody2D.velocity = direction * speed;
    }

    private void Update()
    {
        if (!_active)
            return;
        
        _lastFrameVelocity = _rigidbody2D.velocity;
    }

    private void DestroyBullet()
    {
        OnDestroy?.Invoke(this);
    }

    public void DeInit()
    {
        _hitCount = 0;
        _sawBulletSettings = null!;
        _active = false;
        OnDestroy = null;
    }
}