using UnityEngine;
using UnityEngine.UI;


namespace PixelCrew.Components
{
    public class ShopUI : MonoBehaviour
    {
        [Header("Тексты и кнопки")]
        [SerializeField] private Text _coinsText;
        [SerializeField] private Button _buyPotionButton;
        [SerializeField] private Button _buySwordButton;

        [Header("Цены")]
        [SerializeField] private int _potionPrice = 10;
        [SerializeField] private int _swordPrice = 20;

        private Wallet _wallet;
        private Inventory _inventory;

        private void Start()
        {
            // Ищем героя и его компоненты
            var hero = FindObjectOfType<Hero>();
            if (hero != null)
            {
                _wallet = hero.GetComponent<Wallet>();
                _inventory = hero.GetComponent<Inventory>();
            }

            // Подписываем кнопки на покупку
            if (_buyPotionButton != null)
                _buyPotionButton.onClick.AddListener(BuyPotion);

            if (_buySwordButton != null)
                _buySwordButton.onClick.AddListener(BuySword);

            UpdateCoinsUI();
        }

        private void BuyPotion()
        {
            if (_wallet != null && _inventory != null)
            {
                if (_wallet.TrySpend(_potionPrice))
                {
                    _inventory.AddPotion(); // Добавляем зелье в инвентарь
                    UpdateCoinsUI(); // Обновляем счетчик монет
                }
            }
        }

        private void BuySword()
        {
            if (_wallet != null && _inventory != null)
            {
                if (_wallet.TrySpend(_swordPrice))
                {
                    _inventory.AddSword(); // Добавляем меч в инвентарь
                    UpdateCoinsUI();
                }
            }
        }

        private void UpdateCoinsUI()
        {
            if (_wallet != null && _coinsText != null)
            {
                _coinsText.text = $"Монеты: {_wallet.Total}";
            }
        }
    }
}