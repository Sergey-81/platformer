
using UnityEngine;

namespace PixelCrew.Components
{
    public class HeroAttackComponent : MonoBehaviour
    {
        [SerializeField] private LayerCheck _groundCheck;
        [SerializeField] private GameObject _attackEffectPrefab;
        [SerializeField] private float _attackRadius = 1.5f;

        private Animator _animator;
        private Hero _hero;
        private int _groundAttackIndex = 0;
        private int _airAttackIndex = 0;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _hero = GetComponent<Hero>();
        }

        public void Attack()
        {
            bool isGrounded = _groundCheck != null && _groundCheck.IsTouchingLayer;

            if (isGrounded)
            {
                _groundAttackIndex = (_groundAttackIndex % 3) + 1;
                _animator.SetTrigger($"attack{_groundAttackIndex}");
            }
            else
            {
                _airAttackIndex = (_airAttackIndex % 2) + 1;
                _animator.SetTrigger($"attack_air_{_airAttackIndex}");
            }

            if (_attackEffectPrefab != null && _hero != null)
            {
                Transform spawnPoint = _hero.GetSwordTip();
                if (spawnPoint != null)
                {
                    GameObject effect = Instantiate(_attackEffectPrefab, spawnPoint.position, Quaternion.identity);

                    if (_hero.GetComponent<SpriteRenderer>().flipX)
                    {
                        effect.transform.localScale = new Vector3(-1, 1, 1);
                    }
                }
            }

            DealDamage();
        }

        private void DealDamage()
        {
            // Ищем ВСЕ коллайдеры в радиусе
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _attackRadius);

            foreach (var hit in hits)
            {
                // Пропускаем самого героя
                if (hit.gameObject == gameObject) continue;

                // Если у объекта есть здоровье, наносим урон
                var health = hit.GetComponent<HealthComponent>();
                if (health != null)
                {
                    health.ApplyDamage(1);
                    Debug.Log($"Герой ударил {hit.name}!");
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _attackRadius);
        }
    }
}