using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce = 5f;

    private Vector2 _direction;
    private bool _isCrouching;

    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }

    public void Jump()
    {
        Debug.Log("Jump!");
        // Здесь позже будет физика прыжка через Rigidbody2D
    }

    public void StartCrouch()
    {
        _isCrouching = true;
        Debug.Log("Crouch: on");
        // Здесь позже будет смена анимации и уменьшение коллайдера
    }

    public void StopCrouch()
    {
        _isCrouching = false;
        Debug.Log("Crouch: off");
    }

    public void SaySomething()
    {
        Debug.Log("Something!");
    }

    private void Update()
    {
        if (_direction != Vector2.zero)
        {
            var delta = _direction * (_speed * Time.deltaTime);

            var newPosition = new Vector3(
                transform.position.x + delta.x,
                transform.position.y + delta.y,
                transform.position.z
            );

            transform.position = newPosition;
        }
    }
}