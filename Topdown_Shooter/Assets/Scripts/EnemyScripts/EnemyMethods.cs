using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMethods : MonoBehaviour
{
    private Rigidbody2D enemyRigidBody2D;
    private EnemyStats enemyStats;
    // Start is called before the first frame update
    void Awake()
    {
        enemyStats = GetComponent<EnemyStats>();
        enemyRigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"Enemy Collided!");
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
