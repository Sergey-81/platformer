using UnityEngine;
using UnityEngine.UI;

namespace PixelCrew.Components
{
    public class HUDHealth : MonoBehaviour
    {
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private HealthComponent _healthComponent;

        private void Start()
        {
            if (_healthComponent != null && _healthSlider != null)
            {
                _healthSlider.maxValue = _healthComponent.MaxHealth;
                _healthSlider.value = _healthComponent.Health;
            }
        }

        private void Update()
        {
            if (_healthComponent != null && _healthSlider != null)
            {
                _healthSlider.value = _healthComponent.Health;
            }
        }
    }
}