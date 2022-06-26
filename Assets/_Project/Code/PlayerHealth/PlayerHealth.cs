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
            GameWindow.Instance.SetHP(Mathf.Clamp(1.0f * _health / StartHealth, 0, 1));
            if (_health <= 0)
            {
                GetComponent<PlayerMovement>().PlayDeathAnimation();
                GameController.Instance.Lose();
            }
        }
    }
