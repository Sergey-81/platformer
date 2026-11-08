using UnityEngine;

namespace PixelCrew.Components
{
    public class HealComponent : MonoBehaviour
    {
        [SerializeField] private int _healAmount = 1;

        public void Heal(GameObject target)
        {
            var health = target.GetComponent<HealthComponent>();

            if (health != null && !health.IsFull)
            {
                health.ApplyHeal(_healAmount);
            }
        }
    }
}