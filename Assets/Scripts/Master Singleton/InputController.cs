using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class InputController : MonoBehaviour
{
    private Vector2 _movementDirection;

    public Vector2 MovementDirection { get { return _movementDirection; } }

    public UnityEvent OnMenuButtonPressed;

    public void ReceiveMovementFromUnityInputSystem(CallbackContext context)
    {
        _movementDirection = context.ReadValue<Vector2>();
        Debug.Log("Vector is: " + _movementDirection);
    }

    public void ReceiveMenuButtonPressFromUnityInputSystem(CallbackContext context)
    {
        if(context.performed == true)
        {
            Debug.Log("Menu button pressed");
            OnMenuButtonPressed.Invoke();
        }
    }
}
