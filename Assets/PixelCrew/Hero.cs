using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpSpeed;
    [SerializeField] private float _jumpCutMultiplier = 0.5f;

    [SerializeField] private LayerCheck _groundCheck;

    private Rigidbody2D _rigidbody;
    private Vector2 _direction;
    private bool _isCrouching;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }

    public void Jump()
    {
        if (IsGrounded())
        {
            _rigidbody.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse);
        }
    }

    public void StopJump()
    {
        if (_rigidbody.velocity.y > 0)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y * _jumpCutMultiplier);
        }
    }

    public void StartCrouch()
    {
        _isCrouching = true;
        Debug.Log("Crouch: on");
    }

    public void StopCrouch()
    {
        _isCrouching = false;
        Debug.Log("Crouch: off");
    }

    private bool IsGrounded()
    {
        if (_groundCheck == null)
            return false;

        return _groundCheck.IsTouchingLayer;
    }

    public void SaySomething()
    {
        Debug.Log("Something!");
    }

    private void FixedUpdate()
    {
        var speed = _isCrouching ? _speed * 0.5f : _speed;

        _rigidbody.velocity = new Vector2(_direction.x * speed, _rigidbody.velocity.y);
    }
}