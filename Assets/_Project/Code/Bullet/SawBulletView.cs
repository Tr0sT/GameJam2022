#nullable enable
using System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public class SawBulletView : SerializedMonoBehaviour, IBullet
{
    public event Action<IBullet>? OnDestroy;

    [NonSerialized, OdinSerialize]
    private SawBulletSettings _sawBulletSettings = null!;

    private Vector3 _direction;

    private int _hitCount;
    private bool active = false;

    public void Init(Vector3 position, Vector3 direction, IBulletSettings bulletSettings)
    {
        _sawBulletSettings = (SawBulletSettings)bulletSettings;
        transform.localPosition = position.WithZ(0);
        _direction = direction;
        active = true;
    }

    public void Update()
    {
        if (!active)
            return;
        
        transform.localPosition += _direction * (_sawBulletSettings.speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!active)
            return;
        
        foreach (var contact in collision.contacts)
        {
            if (contact.collider.CompareTag("Enemy"))
            {
                contact.collider.GetComponent<IEnemy>().TakeDamage(_sawBulletSettings.Damage);
                DestroyBullet();
                return;
            }
            if (contact.collider.CompareTag("Wall"))
            {
                _direction = Vector3.Reflect(_direction, contact.normal);
                _hitCount++;
                
                if (_hitCount >= _sawBulletSettings.MaxHitCount)
                {
                    DestroyBullet();
                    return;
                }
            }
        }
    }

    private void DestroyBullet()
    {
        OnDestroy?.Invoke(this);
    }

    public void DeInit()
    {
        _hitCount = 0;
        _direction = Vector3.zero;
        _sawBulletSettings = null!;
        active = false;
        OnDestroy = null;
    }
}