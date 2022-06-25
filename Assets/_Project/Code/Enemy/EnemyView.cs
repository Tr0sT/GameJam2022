#nullable enable
using UnityEngine;

    public class EnemyView : MonoBehaviour, IEnemy
    {
        private int _health;
        public void Init(Vector2 position, object enemySettings)
        {
            _health = 100;
            transform.localPosition = position.WithZ(-1);
        }
    
    
        public void DeInit()
        {
        }
        
        public void TakeDamage(int damage)
        {
            _health -= damage;
        }
    }
