using UnityEngine;

namespace PixelCrew.Components
{
    public class PickupItemComponent : MonoBehaviour
    {
        [SerializeField] private string _itemId;

        // Этот метод мы вызовем из Hero.Interact()
        public void Pickup(GameObject target)
        {
            var inventory = target.GetComponent<Inventory>();

            if (inventory != null)
            {
                inventory.AddItem(_itemId);
                Destroy(gameObject); // Уничтожаем предмет со сцены
            }
        }
    }
}