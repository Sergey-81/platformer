
using UnityEngine;

namespace PixelCrew.Components
{
    public class Wallet : MonoBehaviour
    {
        public int Total { get; private set; }

        public void Add(int amount)
        {
            Total += amount;
            Debug.Log($"Монет собрано: {Total}");
        }
    }
}