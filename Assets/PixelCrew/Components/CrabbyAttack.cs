using UnityEngine;

namespace PixelCrew.Components
{
    public class CrabbyAttack : MonoBehaviour
    {
        [SerializeField] private int _damage = 1;
        [SerializeField] private float _attackRadius = 0.8f;
        [SerializeField] private float _cooldown = 1.5f;

        private float _currentCooldown = 0f;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (_currentCooldown > 0)
            {
                _currentCooldown -= Time.deltaTime;
            }
            else
            {
                TryAttack();
            }
        }

        private void TryAttack()
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _attackRadius);

            foreach (var hit in hits)
            {
                if (hit.CompareTag("Player"))
                {
                    var health = hit.GetComponent<HealthComponent>();
                    if (health != null)
                    {
                        health.ApplyDamage(_damage);
                        Debug.Log($"Краб укусил героя на {_damage} урона!");

                        if (_animator != null) _animator.SetTrigger("attack");

                        _currentCooldown = _cooldown;
                        break;
                    }
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _attackRadius);
        }
    }
}