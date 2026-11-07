using UnityEngine;

namespace PixelCrew.Components
{
    public class SwordProjectile : MonoBehaviour
    {
        [SerializeField] private float _lifetime = 5f;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private float _rotationSpeed = 720f;

        private Rigidbody2D _rigidbody;
        private bool _isStuck = false;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (!_isStuck)
            {
                transform.Rotate(0, 0, _rotationSpeed * Time.deltaTime);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            // Если меч попал в землю и еще не застрял
            if (!_isStuck && ((1 << collision.gameObject.layer) & _groundLayer) != 0)
            {
                _isStuck = true;
                _rigidbody.velocity = Vector2.zero;
                _rigidbody.bodyType = RigidbodyType2D.Kinematic;

                // 1. Меняем слой на interactables, чтобы Hero его видел
                gameObject.layer = LayerMask.NameToLayer("interactables");

                // 2. Добавляем компонент для подбора
                gameObject.AddComponent<SwordPickup>();

                // 3. Добавляем коллайдер-триггер, чтобы Hero мог с ним взаимодействовать
                var collider = gameObject.GetComponent<BoxCollider2D>();
                if (collider != null) collider.isTrigger = true;
            }
        }
    }
}