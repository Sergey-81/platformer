using UnityEngine;
using UnityEngine.InputSystem;

public class HeroInputReader : MonoBehaviour
{
    [SerializeField] private Hero _hero;

    private Vector2 _movementInput;

    public void OnMovement(InputAction.CallbackContext context)
    {
        _movementInput = context.ReadValue<Vector2>();
        _hero.SetDirection(_movementInput);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _hero.Jump();
        }
        else if (context.canceled)
        {
            _hero.StopJump();
        }
    }
}