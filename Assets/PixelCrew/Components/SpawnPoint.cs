using UnityEngine;

namespace PixelCrew.Components
{
    public class SpawnPoint : MonoBehaviour
    {
        private void Start()
        {
            var hero = FindObjectOfType<Hero>();
            if (hero != null)
            {
                // Проверяем, есть ли сохраненный чекпоинт в GameSession
                if (GameSession.Instance != null && GameSession.Instance.CheckpointPosition != Vector2.zero)
                {
                    // Если есть — ставим героя туда
                    hero.transform.position = GameSession.Instance.CheckpointPosition;
                }
                else
                {
                    // Если нет — ставим на стандартную точку (где стоит SpawnPoint)
                    hero.transform.position = transform.position;
                }
            }
        }
    }
}