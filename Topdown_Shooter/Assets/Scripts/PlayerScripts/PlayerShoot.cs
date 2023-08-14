using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Code adapted from lessons learned from Samyam's Input System Tutorial: https://www.youtube.com/watch?v=m5WsmlEOFiA&list=PLKUARkaoYQT2nKuWy0mKwYURe2roBGJdr&ab_channel=samyam
/// Instead of using the more simple Update implementation from PlayerMove, I decided to make use of the Input System's lifecycle.
/// Started and Canceled to signal when the player started and stopped holding down the shoot buttons.
/// 
/// TODO velocity, damage, Firing Rate
/// </summary>
public class PlayerShoot : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction shootAction;

    private Rigidbody2D playerRigidBody;

    private GameObject playerBullet;
    private bool isShooting = false;


    private void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();

        playerInput = GetComponent<PlayerInput>();
        shootAction = playerInput.actions["Shoot"];

        // Does this rename ALL prefabs to PlayerBullet?
        playerBullet = (GameObject)Resources.Load("Prefabs/Bullet1");
        playerBullet.layer = LayerMask.NameToLayer("PlayerBullet");

        shootAction.started += StartedShoot;
        shootAction.canceled += CanceledShoot;
    }

    private void OnDisable()
    {
        shootAction.started -= StartedShoot;
        shootAction.canceled -= CanceledShoot;
    }

    private void FixedUpdate()
    {
        if (isShooting)
        {
            ShootBullet();
        }
    }

    private void StartedShoot(InputAction.CallbackContext context)
    {
        isShooting = true;
        ShootBullet();
    }
    private void CanceledShoot(InputAction.CallbackContext context)
    {
        isShooting = false;
    }


    private void ShootBullet()
    {
        Vector2 shootInput = shootAction.ReadValue<Vector2>();

        GameObject firedBullet = Instantiate(playerBullet, transform.position, transform.rotation);
        Rigidbody2D firedBulletRigidBody2D = firedBullet.GetComponent<Rigidbody2D>();
        firedBulletRigidBody2D.velocity = shootInput;
    }
}
