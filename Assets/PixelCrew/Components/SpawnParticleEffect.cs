using UnityEngine;

namespace PixelCrew.Components
{
    public class SpawnParticleEffect : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particlePrefab;
        [SerializeField] private Transform _spawnPoint;

        public void Spawn()
        {
            if (_particlePrefab == null || _spawnPoint == null) return;

            var particle = Instantiate(_particlePrefab, _spawnPoint.position, Quaternion.identity);
            particle.Play();

            // Уничтожаем объект после завершения анимации партикла
            Destroy(particle.gameObject, particle.main.duration + particle.main.startLifetime.constantMax);
        }
    }
}