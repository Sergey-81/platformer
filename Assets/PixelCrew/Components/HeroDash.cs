using UnityEngine;
using UnityEngine.InputSystem;

namespace PixelCrew.Components
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class HeroDash : MonoBehaviour
    {
        [SerializeField] private float _dashSpeed = 15f;
        [SerializeField] private float _dashDuration = 0.2f;
        [SerializeField] private float _dashCooldown = 1f;

        private Rigidbody2D _rigidbody;
        private float _dashTimer;
        private float _cooldownTimer;
        private bool _isDashing;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (_cooldownTimer > 0)
            {
                _cooldownTimer -= Time.deltaTime;
            }

            if (_isDashing)
            {
                _dashTimer -= Time.deltaTime;
                if (_dashTimer <= 0)
                {
                    EndDash();
                }
            }
        }

        // Этот метод мы вызовем из HeroInputReader
        public void Dash()
        {
            if (_isDashing || _cooldownTimer > 0) return;

            _isDashing = true;
            _dashTimer = _dashDuration;
            _cooldownTimer = _dashCooldown;

            // Берем направление движения из Hero
            var hero = GetComponent<Hero>();
            // Если герой стоит, рывок идет вправо по умолчанию
            float directionX = 1f;
            if (hero != null)
            {
                // В твоем Hero есть переменная _direction, но она приватная. 
                // Поэтому мы просто используем скорость Rigidbody или смотрим по SpriteRenderer
                var sprite = GetComponent<SpriteRenderer>();
                if (sprite != null && sprite.flipX) directionX = -1f;
            }

            _rigidbody.velocity = new Vector2(directionX * _dashSpeed, 0);
        }

        private void EndDash()
        {
            _isDashing = false;
            _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
        }
    }
}