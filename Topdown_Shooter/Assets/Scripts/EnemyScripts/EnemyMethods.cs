using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Enemy Object can be damaged and destroyed by bullets
/// 
/// </summary>
public class EnemyMethods : MonoBehaviour
{
    private Rigidbody2D enemyRigidBody;
    private EnemyStats enemyStats;

    private GameObject player;

    void Awake()
    {
        enemyStats = GetComponent<EnemyStats>();
        enemyRigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        ChasePlayer();
    }
    
    private void ChasePlayer()
    {
        Vector2 enemyToPlayerVector = player.transform.position - transform.position;
        Vector2 directionToPlayer = enemyToPlayerVector.normalized;
        enemyRigidBody.velocity= directionToPlayer * enemyStats.MovementSpeed;
    }
    /// <summary>
    /// Takes damage from bullets.
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerBullet"))
        {
            Destroy(collision.gameObject);
            enemyStats.Health -= 1;
            print($" e.health: { enemyStats.Health}");
            if (enemyStats.Health <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

}
