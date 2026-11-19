
using UnityEngine;

namespace PixelCrew.Components
{
    public class Wallet : MonoBehaviour
    {
        public int Total { get; private set; }

        private void Start()
        {
            if (GameSession.Instance != null)
            {
                Total = GameSession.Instance.Coins;
            }
        }

        public void Add(int amount)
        {
            Total += amount;
            Debug.Log($"Монет собрано: {Total}");

            if (GameSession.Instance != null)
            {
                GameSession.Instance.SetCoins(Total);
            }
        }

        // Добавляем метод для траты монет
        public bool TrySpend(int amount)
        {
            if (Total >= amount)
            {
                Total -= amount;
                Debug.Log($"Потрачено {amount} монет. Осталось: {Total}");

                if (GameSession.Instance != null)
                {
                    GameSession.Instance.SetCoins(Total);
                }

                return true;
            }

            Debug.Log("Недостаточно монет!");
            return false;
        }
    }
}