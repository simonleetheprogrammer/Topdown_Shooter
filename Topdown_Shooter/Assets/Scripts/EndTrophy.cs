using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTrophy : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject player = GameObject.FindWithTag("Player");
            SceneManager.LoadScene("GameOverWin");
            Destroy(player);
        }
    }
}
