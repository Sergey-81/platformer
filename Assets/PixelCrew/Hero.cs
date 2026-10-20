using UnityEngine;
using PixelCrew.Components;

public class Hero : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpSpeed;
    [SerializeField] private float _jumpCutMultiplier = 0.5f;
    [SerializeField] private float _damageJumpSpeed;
    [SerializeField] private LayerCheck _groundCheck;

    [SerializeField] private float _interactionRadius;
    [SerializeField] private Collider2D[] _interactionResult = new Collider2D[1];
    [SerializeField] private LayerMask _interactionLayer;

    // --- ПАРТИКЛЫ ---
    [SerializeField] private GameObject _jumpParticlePrefab;
    [SerializeField] private Vector3 _jumpParticleOffset = new Vector3(0, -0.5f, 0);

    [SerializeField] private GameObject _landParticlePrefab;
    [SerializeField] private Vector3 _landParticleOffset = new Vector3(0, -0.5f, 0);

    // --- ПАРТИКЛ БЕГА ---
    [SerializeField] private GameObject _runParticlePrefab;
    [SerializeField] private Vector3 _runParticleOffset = new Vector3(0, -0.5f, 0);
    [SerializeField] private float _runParticleDelay = 0.3f;

    private Rigidbody2D _rigidbody;
    private Vector2 _direction;
    private bool _jumpPressed;
    private Animator _animator;
    private SpriteRenderer _sprite;
    private float _previousVelocityY;

    private float _runParticleTimer;

    private static readonly int IsGroundKey = Animator.StringToHash("is-ground");
    private static readonly int IsRunning = Animator.StringToHash("is-running");
    private static readonly int VerticalVelocity = Animator.StringToHash("vertical-velocity");
    private static readonly int Hit = Animator.StringToHash("hit");

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }

    public void Jump()
    {
        _jumpPressed = true;
    }

    public void StopJump()
    {
        _jumpPressed = false;
    }

    public void TakeDamage()
    {
        _animator.SetTrigger(Hit);
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _damageJumpSpeed);
    }

    private bool IsGrounded()
    {
        if (_groundCheck == null)
            return false;

        return _groundCheck.IsTouchingLayer;
    }

    private void FixedUpdate()
    {
        _previousVelocityY = _rigidbody.velocity.y;

        _rigidbody.velocity = new Vector2(_direction.x * _speed, _rigidbody.velocity.y);

        var isGrounded = IsGrounded();

        // --- ПРЫЖОК ---
        if (_jumpPressed)
        {
            if (isGrounded && _rigidbody.velocity.y <= 0)
            {
                _rigidbody.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse);

                if (_jumpParticlePrefab != null)
                {
                    Vector3 spawnPos = transform.position + _jumpParticleOffset;
                    Instantiate(_jumpParticlePrefab, spawnPos, Quaternion.identity);
                }
            }
        }
        else if (_rigidbody.velocity.y > 0)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y * _jumpCutMultiplier);
        }

        // --- ПЫЛЬ ПРИ БЕГЕ ---
        HandleRunParticles(isGrounded);

        // --- АНИМАЦИЯ ---
        _animator.SetBool(IsGroundKey, isGrounded);
        _animator.SetBool(IsRunning, _direction.x != 0);
        _animator.SetFloat(VerticalVelocity, _rigidbody.velocity.y);

        UpdateSpriteDirection();
    }

    private void HandleRunParticles(bool isGrounded)
    {
        if (isGrounded && Mathf.Abs(_direction.x) > 0.1f && _runParticlePrefab != null)
        {
            _runParticleTimer -= Time.fixedDeltaTime;
            if (_runParticleTimer <= 0)
            {
                float spawnDirection = _direction.x > 0 ? -1f : 1f;
                Vector3 spawnPos = transform.position + _runParticleOffset;
                spawnPos.x += spawnDirection * 0.3f;

                // Создаем партикл и сохраняем ссылку на него
                GameObject particle = Instantiate(_runParticlePrefab, spawnPos, Quaternion.identity);

                // Если бежим влево, переворачиваем (отражаем) префаб пыли по X
                if (_direction.x < 0)
                {
                    particle.transform.localScale = new Vector3(-1, 1, 1);
                }

                _runParticleTimer = _runParticleDelay;
            }
        }
        else
        {
            _runParticleTimer = 0;
        }
    }

    private void UpdateSpriteDirection()
    {
        if (_direction.x > 0) _sprite.flipX = false;
        else if (_direction.x < 0) _sprite.flipX = true;
    }

    public void Interact()
    {
        var size = Physics2D.OverlapCircleNonAlloc(transform.position, _interactionRadius, _interactionResult, _interactionLayer);
        for (int i = 0; i < size; i++)
        {
            var interactable = _interactionResult[i].GetComponent<InteractableComponent>();
            if (interactable != null) interactable.Interact(gameObject);
        }
    }

    // --- ПРИЗЕМЛЕНИЕ ---
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_previousVelocityY < -1f)
        {
            if (_landParticlePrefab != null)
            {
                Vector3 spawnPos = transform.position + _landParticleOffset;
                Instantiate(_landParticlePrefab, spawnPos, Quaternion.identity);
            }
        }
    }
}