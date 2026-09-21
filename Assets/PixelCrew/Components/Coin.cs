using UnityEngine;

namespace PixelCrew.Components
{
    public class Coin : MonoBehaviour
    {
        [SerializeField] private int _value = 1;

        private void OnTriggerEnter2D(Collider2D other)
        {
            var wallet = other.GetComponent<Wallet>();
            if (wallet == null)
                return;

            wallet.Add(_value);
            Destroy(gameObject);
        }
    }
}