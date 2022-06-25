#nullable enable
using UnityEngine;

    public class EnemyView : MonoBehaviour
    {
        public void Init(Vector2 position, object enemySettings)
        {
            transform.localPosition = position.WithZ(-1);
        }
    
    
        public void DeInit()
        {
        }
    }
