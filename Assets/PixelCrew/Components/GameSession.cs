using UnityEngine;

namespace PixelCrew.Components
{
    public class GameSession : MonoBehaviour
    {
        public static GameSession Instance { get; private set; }

        [SerializeField] private int _health;
        [SerializeField] private int _coins;

        public int Health => _health;
        public int Coins => _coins;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void SetHealth(int value) => _health = value;
        public void SetCoins(int value) => _coins = value;
    }
}