using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float playerMovementSpeed = 6.0f;
    [SerializeField]
    private float playerSmoothTime = 0.1f;

    private Rigidbody2D playerRigidBody2D;

    private Vector2 playerMovementInput;
    private Vector2 playerSmoothMovementInput;
    private Vector2 playerSmoothVelocity;


    private void Awake()
    {
        playerRigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        SetPlayerVelocity();
    }

    private void SetPlayerVelocity()
    {
        playerSmoothMovementInput = Vector2.SmoothDamp(
            playerSmoothMovementInput,
            playerMovementInput,
            ref playerSmoothVelocity,
            playerSmoothTime);
        playerRigidBody2D.velocity = playerSmoothMovementInput * playerMovementSpeed;
    }

    private void OnMove(InputValue inputValue)
    {
        playerMovementInput = inputValue.Get<Vector2>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log($"Player Collided!");
    }
}
