#nullable enable
using System;
using UnityEngine;

public interface IEnemy
{
    public event Action<IEnemy>? OnDestroy;
    public void TakeDamage(int damage);

    public void Init(Vector3 position, IEnemySettings enemySettings);
    public void DeInit();
}