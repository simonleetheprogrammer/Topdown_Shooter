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
    private GameObject playerBullet;

    private Rigidbody2D playerRigidBody2D;

    private Vector2 playerMovementInput;
    private Vector2 playerSmoothMovementInput;
    private Vector2 playerSmoothVelocity;


    private void Awake()
    {
        playerRigidBody2D = GetComponent<Rigidbody2D>();
        playerBullet = (GameObject)Resources.Load("Prefabs/Bullet1");
        playerBullet.layer = LayerMask.NameToLayer("PlayerBullet");
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
    private void OnFire(InputValue inputValue)
    {
        Vector2 firedBulletVelocity = inputValue.Get<Vector2>();
        print($"Bullet Velocity {firedBulletVelocity}");
        print($"Bullet layer {playerBullet.layer}");
        GameObject firedBullet = Instantiate(playerBullet, transform.position, transform.rotation);
        Rigidbody2D firedBulletRigidBody2D = firedBullet.GetComponent<Rigidbody2D>();
        firedBulletRigidBody2D.velocity = firedBulletVelocity;



    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"Player Collided!");
    }
}
