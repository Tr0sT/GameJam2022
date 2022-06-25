#nullable enable
using System;
using UnityEngine;

public class SawBulletView : MonoBehaviour, IBullet
{
    public event Action<IBullet>? OnDestroy;
    private Vector3 _direction;
    private SawBulletSettings _sawBulletSettings = null!;
    
    private int _hitCount;
    private bool active = false;

    public void Init(IBulletSettings bulletSettings)
    {
        _sawBulletSettings = (SawBulletSettings)bulletSettings;
        transform.localPosition = _sawBulletSettings.StartPosition.WithZ(0);
        _direction = _sawBulletSettings.StartDirection;
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
                contact.collider.GetComponent<IEnemy>().TakeDamage(_sawBulletSettings.damage);
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