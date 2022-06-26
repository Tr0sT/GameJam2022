#nullable enable
using System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public class EnemyView : SerializedMonoBehaviour, IEnemy
{
    public event Action<IEnemy>? OnDestroy;

    [NonSerialized]
    [OdinSerialize]
    private EnemySettings _enemySettings = null!;

    private int _health;


    public void Init(Vector3 position, IEnemySettings enemySettings)
    {
        _enemySettings = (EnemySettings)enemySettings;
        transform.localPosition = position.WithZ(0);
        _health = _enemySettings.Health;
        GetComponent<Movement>().Speed = _enemySettings.Speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (var contact in collision.contacts)
        {
            if (contact.collider.CompareTag("Player"))
            {
                contact.collider.GetComponent<PlayerHealth>().TakeDamage(_enemySettings.Damage);

                return;
            }
        }
    }

    public void DeInit()
    {
        OnDestroy = null;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            OnDestroy?.Invoke(this);
        }
    }
}