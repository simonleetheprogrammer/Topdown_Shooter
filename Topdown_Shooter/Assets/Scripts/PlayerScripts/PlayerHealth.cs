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
    private bool playerInvulnerable;    // So far this does nothing.
    private int enemyLayer;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        playerRigidBody= GetComponent<Rigidbody2D>();
        enemyLayer = LayerMask.NameToLayer("Enemy");
        Health = MaxHealth;
    }
    /// <summary>
    /// For Taking damage, and Invulnerability.
    /// I found this to be kinda clever, but if the collisionsstay is used for any logic other than taking damage, then I would need to take out the await in OnCollisionStay2D.
    /// </summary>
    /// <param name="collision"></param>
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
    /// <summary>
    /// Force the OnCollisionStay2D to wait.
    /// </summary>
    /// <returns></returns>
    private async Task MakePlayerInvulnerable()
    {
        playerInvulnerable= true;
        print("Player Invulnerable");
        await Task.Delay(invulnerabilityTime);
        playerInvulnerable = false;
        print("Player No Longer Invulnerable");
    }
    /// <summary>
    /// Draw Health to screen
    /// </summary>
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(20, 20, 250, 120));
        GUILayout.Label($"Health {Health}/{MaxHealth}");
        GUILayout.EndArea();
    }
}
