using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteAnimation : MonoBehaviour
    {
        [SerializeField] private int _frameRate;
        [SerializeField] private bool _loop;
        [SerializeField] private Sprite[] _sprites;
        [SerializeField] private UnityEvent _onComplete;

        private SpriteRenderer _renderer;
        private float _secondsPerFrame;
        private int _currentSprite;
        private float _spriteUpdateTime;

        private bool _isPlaying = true;

        private void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        private void OnEnable()
        {
            _secondsPerFrame = 1f / _frameRate;
            _spriteUpdateTime = Time.time + _secondsPerFrame;
            _currentSprite = 0;
            _isPlaying = true;
        }

        private void Update()
        {
            if (!_isPlaying || _spriteUpdateTime > Time.time) return;

            if (_currentSprite >= _sprites.Length)
            {
                if (_loop)
                {
                    _currentSprite = 0;
                }
                else
                {
                    _isPlaying = false;
                    _onComplete?.Invoke();
                    return;
                }
            }

            _renderer.sprite = _sprites[_currentSprite];
            _spriteUpdateTime += _secondsPerFrame;
            _currentSprite++;
        }
    }
}