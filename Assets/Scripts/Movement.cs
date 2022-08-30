using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private bool changeRotation;

    private Rigidbody2D _rb2D;

    private void Awake()
    {
        _rb2D = GetComponent<Rigidbody2D>();
    }
    
    public void Move(Vector2 direction)
    {
        _rb2D.MovePosition(_rb2D.position + (direction * (speed * Time.fixedDeltaTime)));
        if (changeRotation)
        {
            ChangeRotation(direction);
        }
    }

    private void ChangeRotation(Vector2 direction)
    {
        if (direction.magnitude == 0)
        {
            // standing still, don't change rotation
            return;
        }
        // determine direction to face
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            // more horizontal movement than vertical
            if (direction.x < 0)
            {
                // face left
                _rb2D.SetRotation(Quaternion.Euler(0f, 0f, 180f));
            }
            else
            {
                // face right
                _rb2D.SetRotation(Quaternion.Euler(0f, 0f, 0f));
            }
        }
        else
        {
            // more vertical movement than horizontal
            if (direction.y < 0)
            {
                // face down
                _rb2D.SetRotation(Quaternion.Euler(0f, 0f, 270f));
            }
            else
            {
                // face up
                _rb2D.SetRotation(Quaternion.Euler(0f, 0f, 90f));
            }
        }
    }
}
