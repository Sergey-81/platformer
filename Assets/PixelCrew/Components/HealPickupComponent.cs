using UnityEngine;

namespace PixelCrew.Components
{
    public class HealPickupComponent : MonoBehaviour
    {
        [SerializeField] private int _healAmount = 5;

        private void OnTriggerEnter2D(Collider2D other)
        {
            var health = other.GetComponent<HealthComponent>();
            if (health == null)
                return;

            health.ApplyHeal(_healAmount);
            Destroy(gameObject);
        }
    }
}