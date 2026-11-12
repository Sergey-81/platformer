using UnityEngine;

namespace PixelCrew.Components
{
    public class CrabbyAI : MonoBehaviour
    {
        [SerializeField] private float _speed = 2f;
        [SerializeField] private float _detectionRadius = 5f;

        private Rigidbody2D _rigidbody;
        private SpriteRenderer _sprite;
        private Animator _animator;
        private Transform _heroTransform;

        private static readonly int IsRunningKey = Animator.StringToHash("isRunning");

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _sprite = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            // Ищем ВСЕ объекты в радиусе
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _detectionRadius);

            _heroTransform = null;

            foreach (var hit in hits)
            {
                // Если нашли объект с тегом "Player" - это наш герой
                if (hit.CompareTag("Player"))
                {
                    _heroTransform = hit.transform;
                    break;
                }
            }

            if (_heroTransform != null)
            {
                MoveTowardsHero();
            }
            else
            {
                StopMoving();
            }
        }

        private void MoveTowardsHero()
        {
            if (_heroTransform == null) return;

            float distance = Mathf.Abs(_heroTransform.position.x - transform.position.x);

            if (distance < 0.5f)
            {
                StopMoving();
                return;
            }

            float directionToHero = Mathf.Sign(_heroTransform.position.x - transform.position.x);

            _rigidbody.velocity = new Vector2(directionToHero * _speed, _rigidbody.velocity.y);

            if (directionToHero > 0) _sprite.flipX = false;
            else if (directionToHero < 0) _sprite.flipX = true;

            if (_animator != null) _animator.SetBool(IsRunningKey, true);
        }

        private void StopMoving()
        {
            _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
            if (_animator != null) _animator.SetBool(IsRunningKey, false);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _detectionRadius);
        }
    }
}