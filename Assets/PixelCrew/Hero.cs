using PixelCrew.Components;
using UnityEngine;

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

    [SerializeField] private RuntimeAnimatorController _swordController;
    [SerializeField] private Transform _swordTipRight;
    [SerializeField] private Transform _swordTipLeft;

    [SerializeField] private GameObject _swordProjectilePrefab;

    [SerializeField] private GameObject _jumpParticlePrefab;
    [SerializeField] private Vector3 _jumpParticleOffset = new Vector3(0, -0.5f, 0);
    [SerializeField] private GameObject _landParticlePrefab;
    [SerializeField] private Vector3 _landParticleOffset = new Vector3(0, -0.5f, 0);
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

    private RuntimeAnimatorController _baseController;
    private bool _isSwordEquipped = false;

    private static readonly int IsGroundKey = Animator.StringToHash("is-ground");
    private static readonly int IsRunning = Animator.StringToHash("is-running");
    private static readonly int VerticalVelocity = Animator.StringToHash("vertical-velocity");
    private static readonly int Hit = Animator.StringToHash("hit");

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();

        if (_animator != null)
            _baseController = _animator.runtimeAnimatorController;

        DontDestroyOnLoad(gameObject);
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

    public void ToggleSword()
    {
        if (_animator == null) return;

        _isSwordEquipped = !_isSwordEquipped;

        if (_isSwordEquipped)
            _animator.runtimeAnimatorController = _swordController;
        else
            _animator.runtimeAnimatorController = _baseController;

        Debug.Log($"Меч в руках: {_isSwordEquipped}");
    }

    public Transform GetSwordTip()
    {
        return _sprite.flipX ? _swordTipLeft : _swordTipRight;
    }

    public void ThrowSword()
    {
        var inventory = GetComponent<Inventory>();
        if (inventory != null && inventory.TrySpendSword())
        {
            if (_animator != null)
            {
                _animator.SetTrigger("Throw");
            }

            Transform spawnPoint = GetSwordTip();
            if (_swordProjectilePrefab != null && spawnPoint != null)
            {
                GameObject sword = Instantiate(_swordProjectilePrefab, spawnPoint.position, Quaternion.identity);
                var rb = sword.GetComponent<Rigidbody2D>();
                var spriteRenderer = sword.GetComponent<SpriteRenderer>();

                if (_sprite.flipX)
                {
                    if (spriteRenderer != null) spriteRenderer.flipX = true;
                    if (rb != null) rb.velocity = new Vector2(-10f, 0);
                }
                else
                {
                    if (spriteRenderer != null) spriteRenderer.flipX = false;
                    if (rb != null) rb.velocity = new Vector2(10f, 0);
                }
            }
        }
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

        HandleRunParticles(isGrounded);

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

                GameObject particle = Instantiate(_runParticlePrefab, spawnPos, Quaternion.identity);
                if (_direction.x < 0) particle.transform.localScale = new Vector3(-1, 1, 1);

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
            if (interactable != null)
            {
                interactable.Interact(gameObject);
            }
            else
            {
                var swordPickup = _interactionResult[i].GetComponent<SwordPickup>();
                if (swordPickup != null)
                {
                    swordPickup.Pickup(gameObject);
                }
            }
        }
    }

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