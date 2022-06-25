﻿#nullable enable
using System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public class EnemyView : SerializedMonoBehaviour, IEnemy
    {
        private int _health;
        
        [NonSerialized, OdinSerialize] 
        private EnemySettings _enemySettings = null!;
        
        public void Init(Vector3 position, IEnemySettings enemySettings)
        {
            _enemySettings = (EnemySettings) enemySettings;
            transform.localPosition = position.WithZ(-1);

            _health = _enemySettings.Health;
        }
    
        public void DeInit()
        {
        }
        
        public void TakeDamage(int damage)
        {
            _health -= damage;
        }
    }
