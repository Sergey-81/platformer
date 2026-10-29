
using UnityEngine;

namespace PixelCrew.Components
{
    public class ChestComponent : MonoBehaviour
    {
        [SerializeField] private Animator _chestAnimator;

        public void OpenChest()
        {
            if (_chestAnimator != null)
            {
                _chestAnimator.SetTrigger("Open"); // Только анимация
            }
        }

        // Этот метод будет вызываться при нажатии E на сундуке
        public void Loot()
        {
            var inventory = FindObjectOfType<Inventory>();
            if (inventory != null)
            {
                inventory.AddItem("Map");
                Debug.Log("Карта добавлена в инвентарь!");
            }
        }
    }
}
