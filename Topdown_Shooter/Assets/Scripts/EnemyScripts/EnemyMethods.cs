using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
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
        enemyRigidBody.velocity = directionToPlayer * enemyStats.MovementSpeed;
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
            if (enemyStats.Health <= 0)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Flashing(300, 300);
            }
        }
    }
    /// <summary>
    /// This is copied over from playerHealth.
    /// Repeated code.
    /// But added in the IsDestroyed()
    /// </summary>
    /// <param name="flashDuration"></param>
    /// <param name="flashInterval"></param>
    private async void Flashing(int flashDuration, int flashInterval)
    {
        SpriteRenderer playerSprite = gameObject.GetComponentInChildren<SpriteRenderer>();
        Color palePink = new Color(245, 0, 0, 0.2f);
        Color noColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        bool flashingColor = false;

        for (int i = 0; i < flashDuration; i = i + flashInterval)
        {
            if (playerSprite.IsDestroyed())
            {
                return;
            }
            else if (flashingColor)
            {
                playerSprite.color = noColor;
            }
            else
            {
                playerSprite.color = palePink;
            }
            flashingColor = !flashingColor;
            await Task.Delay(flashInterval);
        }
        if (!playerSprite.IsDestroyed()){
            playerSprite.color = noColor;
        }
    }

}
