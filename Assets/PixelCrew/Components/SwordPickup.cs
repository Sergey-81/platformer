using UnityEngine;

namespace PixelCrew.Components
{
    public class SwordPickup : MonoBehaviour
    {
        public void Pickup(GameObject target)
        {
            var inventory = target.GetComponent<Inventory>();
            if (inventory != null)
            {
                inventory.AddSword();
                Destroy(gameObject); // Уничтожаем меч со сцены
            }
        }
    }
}