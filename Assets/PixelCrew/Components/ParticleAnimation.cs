using UnityEngine;

namespace PixelCrew.Components
{
    public class ParticleAnimation : MonoBehaviour
    {
        [SerializeField] private Sprite[] _sprites;
        [SerializeField] private float _frameRate = 12f;
        [SerializeField] private bool _loop = false;

        private SpriteRenderer _renderer;
        private float _timer;
        private int _currentFrame;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (_sprites == null || _sprites.Length == 0) return;

            _timer += Time.deltaTime;
            if (_timer >= 1f / _frameRate)
            {
                _timer -= 1f / _frameRate;
                _currentFrame++;

                if (_currentFrame >= _sprites.Length)
                {
                    if (_loop)
                    {
                        _currentFrame = 0;
                    }
                    else
                    {
                        Destroy(gameObject);
                        return;
                    }
                }

                _renderer.sprite = _sprites[_currentFrame];
            }
        }
    }
}