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

    private bool _active;

    public void Init(Vector3 position, Vector3 direction, IBulletSettings bulletSettings)
    {
        _sawBulletSettings = (SawBulletSettings)bulletSettings;
        transform.localPosition = position.WithZ(0);
        GetComponent<Movement>().Speed = _sawBulletSettings.speed;
        GetComponent<Movement>().Direction = direction;
        _active = true;
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

            if (contact.collider.CompareTag("Player") && !_active)
            {
                contact.collider.GetComponent<PlayerController>().PickupSaw();
                DestroyBullet();
                return;
            }
        }
    }
    


    private void DestroyBullet()
    {
        OnDestroy?.Invoke(this);
    }

    public void DeInit()
    {
        _sawBulletSettings = null!;
        _active = false;
        OnDestroy = null;
    }
}