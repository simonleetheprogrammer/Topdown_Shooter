using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int MaxHealth { get; set; } = 2123;
    public int Health { get; set; }
    private Rigidbody2D playerRigidBody;

    [SerializeField]
    private int invulnerabilityTime = 2000;
    private bool playerInvulnerable;    // So far this does nothing.
    private int enemyLayer;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        playerRigidBody = GetComponent<Rigidbody2D>();
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
            if (Health <= 0)
            {
                Destroy(gameObject);
                SceneManager.LoadScene("GameOver");
            }
            else
            {
                await MakePlayerInvulnerable();
            }
        }
    }
    /// <summary>
    /// Force the OnCollisionStay2D to wait.
    /// </summary>
    /// <returns></returns>
    private async Task MakePlayerInvulnerable()
    {
        playerInvulnerable = true;
        await PlayerFlashing(invulnerabilityTime, 300);
        playerInvulnerable = false;
    }
    /// <summary>
    /// Flash player pink for some time.
    /// Used for Iframes.
    /// </summary>
    /// <returns></returns>
    private async Task PlayerFlashing(int flashDuration, int flashInterval)
    {
        SpriteRenderer playerSprite = gameObject.GetComponentInChildren<SpriteRenderer>();
        Color palePink = new Color(245, 0, 0, 0.2f);
        Color noColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        bool flashingColor = false;

        for (int i = 0; i < flashDuration; i = i + flashInterval)
        {
            if (flashingColor)
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
        playerSprite.color = noColor;

    }

    /// <summary>
    /// Make red text.
    /// Draw Health to screen
    /// </summary>
    void OnGUI()
    {
        GUIStyle labelStyle = new GUIStyle();
        labelStyle.normal.textColor = Color.red;

        GUILayout.BeginArea(new Rect(20, 20, 250, 120));
        GUILayout.Label($"Health {Health}/{MaxHealth}", labelStyle);
        GUILayout.EndArea();
    }
}
