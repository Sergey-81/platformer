using UnityEngine;

namespace PixelCrew.Components
{
    public class CircularProjectileSpawner : MonoBehaviour
    {
        [Header("Настройки стрельбы")]
        [SerializeField] private GameObject _projectilePrefab; // Префаб пули/снаряда
        [SerializeField] private int _itemsPerBurst = 5;       // Количество снарядов за раз (твой ItemPerBurst)
        [SerializeField] private float _radius = 1f;           // Радиус, по которому распределяются снаряды

        [Header("Направление и скорость")]
        [SerializeField] private float _projectileSpeed = 10f; // Скорость снарядов
        [SerializeField] private bool _aimAtTarget = false;    // Стрелять в сторону цели?
        [SerializeField] private Transform _target;            // Цель (если _aimAtTarget = true)

        // Метод для вызова стрельбы
        public void Shoot()
        {
            if (_projectilePrefab == null) return;

            // Вычисляем угол между снарядами (360 градусов делим на количество)
            float angleStep = 360f / _itemsPerBurst;

            for (int i = 0; i < _itemsPerBurst; i++)
            {
                // Вычисляем угол для текущего снаряда
                float angle = i * angleStep * Mathf.Deg2Rad;

                // Вычисляем позицию для спавна (по кругу)
                Vector3 spawnPos = transform.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * _radius;

                // Создаем снаряд
                GameObject projectile = Instantiate(_projectilePrefab, spawnPos, Quaternion.identity);

                // Настраиваем направление движения
                Vector2 direction;
                if (_aimAtTarget && _target != null)
                {
                    direction = (_target.position - spawnPos).normalized;
                }
                else
                {
                    direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;
                }

                // Задаем скорость (если у снаряда есть Rigidbody2D)
                var rb = projectile.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.velocity = direction * _projectileSpeed;
                }
            }
        }
    }
}