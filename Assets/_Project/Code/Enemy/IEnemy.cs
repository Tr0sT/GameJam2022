#nullable enable
using UnityEngine;

public interface IEnemy
{
    public void TakeDamage(int damage);

    public void Init(Vector3 position, IEnemySettings enemySettings);
    public void DeInit();
}