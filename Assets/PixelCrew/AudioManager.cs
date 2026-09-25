using UnityEngine;

namespace PixelCrew.Components
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _ambientSource;
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _sfxSource;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            _ambientSource.loop = true;
            _musicSource.loop = true;

            _ambientSource.Play();
            _musicSource.Play();
        }

        public void PlaySfx(AudioClip clip)
        {
            if (clip != null)
            {
                _sfxSource.PlayOneShot(clip);
            }
        }
    }
}