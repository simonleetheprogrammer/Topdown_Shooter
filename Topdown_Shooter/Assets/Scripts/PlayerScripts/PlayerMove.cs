using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Applies Vector2 force onto Player.
/// Code partially adapted from Ketra's 2d topdown tutorial. But instead of using OnMove event, I grabbed the move action manually.
/// 
/// </summary>
public class PlayerMove : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction moveAction;

    private Rigidbody2D playerRigidBody;

    [SerializeField]
    private float playerSpeed = 3f;
    [SerializeField]
    private float playerSmoothTime = 0.1f;

    private Vector2 movementInput;
    private Vector2 smoothMovementInput;
    private Vector2 smoothVelocity;

    private void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();

        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
    }
    void FixedUpdate()
    {
        SetPlayerVelocity();
    }
    private void SetPlayerVelocity()
    {
        movementInput = moveAction.ReadValue<Vector2> ();
        smoothMovementInput = Vector2.SmoothDamp(
            smoothMovementInput,
            movementInput,
            ref smoothVelocity,
            playerSmoothTime);
        playerRigidBody.velocity = smoothMovementInput * playerSpeed;
    }
}
