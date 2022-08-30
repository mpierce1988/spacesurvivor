using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Player : MonoBehaviour
{
    private Movement _movement;
    private InputController _inputController;

    private void Awake()
    {
        _movement = GetComponent<Movement>();
        _inputController = MasterSingleton.instance.InputController;
    }

    private void FixedUpdate()
    {
        // get movement direction
        Vector2 movementDirection = _inputController.MovementDirection;
        // move player
        _movement.Move(movementDirection);
    }
}
