using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int _maxHealth = 5;
        [SerializeField] private int _health = 5;
        [SerializeField] private UnityEvent _onDamage;
        [SerializeField] private UnityEvent _onDie;
        [SerializeField] private UnityEvent _onHeal;

        public int Health => _health;
        public int MaxHealth => _maxHealth;
        public bool IsFull => _health >= _maxHealth;

        private void Start()
        {
            if (GameSession.Instance != null)
            {
                _health = GameSession.Instance.Health;
            }
        }

        public void ApplyDamage(int damageValue)
        {
            if (_health <= 0) return;

            _health -= damageValue;
            _onDamage?.Invoke();

            if (GameSession.Instance != null)
            {
                GameSession.Instance.SetHealth(_health);
            }

            if (_health <= 0)
            {
                _onDie?.Invoke();
            }
        }

        public void ApplyHeal(int healValue)
        {
            if (IsFull) return;

            _health += healValue;

            if (_health > _maxHealth) _health = _maxHealth;

            _onHeal?.Invoke();

            if (GameSession.Instance != null)
            {
                GameSession.Instance.SetHealth(_health);
            }
        }
    }
}