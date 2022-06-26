#nullable enable
using Sirenix.OdinInspector;
using UnityEngine;

    public class PlayerHealth : MonoBehaviour
    {
        public int StartHealth = 10;

        [ShowInInspector]
        private int _health;

        public void OnEnable()
        {
            _health = StartHealth;
        }

        public void TakeDamage(int damage)
        {
            _health -= damage;
        }
    }
