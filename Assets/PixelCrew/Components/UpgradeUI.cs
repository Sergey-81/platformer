using UnityEngine;
using UnityEngine.UI;


namespace PixelCrew.Components
{
    public class UpgradeUI : MonoBehaviour
    {
        [SerializeField] private Text _damageText;
        [SerializeField] private Button _upgradeButton;

        private HeroStats _heroStats;

        private void Start()
        {
            // Ищем героя на сцене
            _heroStats = FindObjectOfType<HeroStats>();

            if (_upgradeButton != null)
            {
                _upgradeButton.onClick.AddListener(OnUpgradeClicked);
            }

            UpdateUI();
        }

        private void OnUpgradeClicked()
        {
            if (_heroStats != null)
            {
                _heroStats.UpgradeDamage();
                UpdateUI();
            }
        }

        private void UpdateUI()
        {
            if (_heroStats != null && _damageText != null)
            {
                // Обновляем текст на экране
                _damageText.text = $"Урон: {_heroStats.CurrentDamage} (Ур. {_heroStats.DamageLevel})";
            }
        }
    }
}