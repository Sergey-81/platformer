using UnityEngine;
using UnityEngine.UI;


namespace PixelCrew.Components
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private GameObject _inventoryPanel;

        [Header("Bottle Slot")]
        [SerializeField] private Image _barrelIconSlot;
        [SerializeField] private Sprite _barrelSprite;

        [Header("Map Slot")]
        [SerializeField] private Image _mapIconSlot;
        [SerializeField] private Sprite _mapSprite;

        private bool _isOpen = false;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            _inventoryPanel.SetActive(false);
            _barrelIconSlot.gameObject.SetActive(false);
            _mapIconSlot.gameObject.SetActive(false); // Жестко выключаем карту при старте
        }

        public void ToggleInventory()
        {
            _isOpen = !_isOpen;
            _inventoryPanel.SetActive(_isOpen);

            if (_isOpen)
            {
                UpdateUI();
            }
        }

        private void UpdateUI()
        {
            var inventory = FindObjectOfType<Inventory>();

            if (inventory != null)
            {
                // Бочка
                if (inventory.HasItem("Barrel"))
                {
                    _barrelIconSlot.gameObject.SetActive(true);
                    _barrelIconSlot.sprite = _barrelSprite;
                }
                else
                {
                    _barrelIconSlot.gameObject.SetActive(false);
                }

                // Карта (включается ТОЛЬКО если мы реально её получили)
                if (inventory.HasItem("Map"))
                {
                    _mapIconSlot.gameObject.SetActive(true);
                    _mapIconSlot.sprite = _mapSprite;
                }
                else
                {
                    _mapIconSlot.gameObject.SetActive(false);
                }
            }
        }
    }
}