#nullable enable
using System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public class EnemyView : SerializedMonoBehaviour, IEnemy
{
    [NonSerialized] [OdinSerialize] 
    private EnemySettings _enemySettings = null!;


    public void Init(Vector3 position, IEnemySettings enemySettings)
    {
        _enemySettings = (EnemySettings) enemySettings;
        transform.localPosition = position.WithZ(-1);
    }
    
    

    public void DeInit()
    {
    }

    public void TakeDamage(int damage)
    {
        //_health -= damage;
    }
}