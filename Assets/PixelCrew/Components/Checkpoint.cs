
using UnityEngine;

namespace PixelCrew.Components
{
    public class Checkpoint : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                // Запоминаем позицию игрока в GameSession
                if (GameSession.Instance != null)
                {
                    // Сохраняем позицию героя
                    GameSession.Instance.SetCheckpoint(other.transform.position);

                    // Визуально можно как-то отметить чекпоинт (например, выключить анимацию)
                    Debug.Log("Чекпоинт активирован!");
                }
            }
        }
    }
}