using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int MaxHealth { get; set; } = 10;
    public int Health { get; set; }
    private Rigidbody2D playerRigidBody;

    [SerializeField]
    private int invulnerabilityTime = 2000;
    private bool playerInvulnerable;

    private int enemyLayer;

    private void Awake()
    {
        playerRigidBody= GetComponent<Rigidbody2D>();
        enemyLayer = LayerMask.NameToLayer("Enemy");
        Health = MaxHealth;
    }

    private async void OnCollisionStay2D(Collision2D collision)
    {
        GameObject enemy = collision.gameObject;
        if (enemy.layer == enemyLayer && !playerInvulnerable)
        {
            EnemyStats enemyStats = enemy.GetComponent<EnemyStats>();
            Health -= enemyStats.Damage;
            await MakePlayerInvulnerable();
        }
    }

    private async Task MakePlayerInvulnerable()
    {
        playerInvulnerable= true;
        print("Player Invulnerable");
        await Task.Delay(invulnerabilityTime);
        playerInvulnerable = false;
        print("Player No Longer Invulnerable");
    }

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(20, 20, 250, 120));
        GUILayout.Label($"Health {Health}/{MaxHealth}");
        GUILayout.EndArea();
    }
}
