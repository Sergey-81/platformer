using System.Collections.Generic;
using UnityEngine;

namespace PixelCrew.Components
{
    public class Inventory : MonoBehaviour
    {
        private List<string> _items = new List<string>();

        [SerializeField] private int _swordsCount = 2; // Начальное количество мечей

        public int SwordsCount => _swordsCount;

        public void AddItem(string itemId)
        {
            _items.Add(itemId);
        }

        public bool HasItem(string itemId)
        {
            return _items.Contains(itemId);
        }

        public void AddSword()
        {
            _swordsCount++;
            Debug.Log($"Меч подобран! Теперь мечей: {_swordsCount}");
        }

        public bool TrySpendSword()
        {
            if (_swordsCount > 0)
            {
                _swordsCount--;
                Debug.Log($"Меч брошен! Осталось: {_swordsCount}");
                return true;
            }
            Debug.Log("Мечей нет!");
            return false;
        }

        public List<string> GetItems()
        {
            return _items;
        }
    }
}