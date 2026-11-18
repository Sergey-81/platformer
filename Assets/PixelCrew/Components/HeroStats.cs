
using UnityEngine;

namespace PixelCrew.Components
{
    public class HeroStats : MonoBehaviour
    {
        // Текущий уровень урона
        public int DamageLevel { get; private set; } = 1;

        // Базовый урон и прибавка за уровень
        [SerializeField] private int _baseDamage = 1;
        [SerializeField] private int _damagePerLevel = 1;

        // Геттер для получения текущего урона
        public int CurrentDamage => _baseDamage + (DamageLevel - 1) * _damagePerLevel;

        // Метод для повышения уровня урона (вызовем из UI)
        public void UpgradeDamage()
        {
            DamageLevel++;
            Debug.Log($"Урон повышен! Теперь урон: {CurrentDamage}, Уровень: {DamageLevel}");
        }
    }
}