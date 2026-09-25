using UnityEngine;

namespace PixelCrew.Components
{
    public class Coin : MonoBehaviour
    {
        [SerializeField] private int _value = 1;
        [SerializeField] private AudioClip _pickupSound;

        private void OnTriggerEnter2D(Collider2D other)
        {
            var wallet = other.GetComponent<Wallet>();
            if (wallet == null)
                return;

            wallet.Add(_value);

            if (_pickupSound != null)
            {
                var audioManager = FindObjectOfType<AudioManager>();
                if (audioManager != null)
                {
                    audioManager.PlaySfx(_pickupSound);
                }
            }

            Destroy(gameObject);
        }
    }
}