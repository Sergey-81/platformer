using UnityEngine;

namespace PixelCrew.Components
{
    public class HealthPotionPickup : MonoBehaviour
    {
        public void Pickup(GameObject target)
        {
            var inventory = target.GetComponent<Inventory>();
            if (inventory != null)
            {
                inventory.AddPotion();
                Destroy(gameObject); // Уничтожаем зелье со сцены
            }
        }
    }
}