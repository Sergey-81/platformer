using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private UnityEvent _onDamage;
        [SerializeField] private UnityEvent _onDie;
        [SerializeField] private UnityEvent _onHeal;

        public int Health => _health;

        private void Start()
        {
            if (GameSession.Instance != null)
            {
                _health = GameSession.Instance.Health;
            }
        }

        public void ApplyDamage(int damageValue)
        {
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
            _health += healValue;
            _onHeal?.Invoke();

            if (GameSession.Instance != null)
            {
                GameSession.Instance.SetHealth(_health);
            }
        }
    }
}