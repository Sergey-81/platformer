using UnityEngine;
using UnityEngine.InputSystem;

public class HeroInputReader : MonoBehaviour
{
    [SerializeField] private Hero _hero;

    public void OnMovement(InputAction.CallbackContext context)
    {
        var direction = context.ReadValue<Vector2>();
        _hero.SetDirection(direction);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
            _hero.Jump();
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.started)
            _hero.StartCrouch();
        else if (context.canceled)
            _hero.StopCrouch();
    }

    public void OnSaySomething(InputAction.CallbackContext context)
    {
        if (context.performed)
            _hero.SaySomething();
    }
}