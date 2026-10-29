using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components
{
    public class WheelComponent : MonoBehaviour
    {
        [SerializeField] private Animator _wheelAnimator;
        [SerializeField] private UnityEvent _onWheelTurned;

        private bool _isTurned = false;

        public void Interact()
        {
            if (_isTurned) return; // Крутим только один раз
            _isTurned = true;

            if (_wheelAnimator != null)
            {
                _wheelAnimator.SetTrigger("interact"); // Запускаем анимацию штурвала
            }

            // Вызываем событие (открытие сундука)
            _onWheelTurned?.Invoke();
        }
    }
}