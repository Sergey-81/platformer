using UnityEngine;
using UnityEngine.InputSystem;
using PixelCrew.Components;

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

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            _hero.Interact();
        }
    }

    // --- Обработка атаки ---
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            var attackComponent = _hero.GetComponent<HeroAttackComponent>();
            if (attackComponent != null)
            {
                attackComponent.Attack();
            }
        }
    }

    // --- Обработка броска меча ---
    public void OnThrow(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _hero.ThrowSword();
        }
    }
}