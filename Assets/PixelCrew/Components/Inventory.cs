using System.Collections.Generic;
using UnityEngine;

namespace PixelCrew.Components
{
    public class Inventory : MonoBehaviour
    {
        // Список для хранения названий предметов (строк)
        private List<string> _items = new List<string>();

        public void AddItem(string itemId)
        {
            _items.Add(itemId);
            Debug.Log($"Предмет {itemId} добавлен в инвентарь! Всего предметов: {_items.Count}");
        }

        public bool HasItem(string itemId)
        {
            return _items.Contains(itemId);
        }

        // Позже мы свяжем это с UI
        public List<string> GetItems()
        {
            return _items;
        }
    }
}