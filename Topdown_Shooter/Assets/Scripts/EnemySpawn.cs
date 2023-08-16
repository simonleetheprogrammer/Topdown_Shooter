using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawn : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        print("Yes");
        if (collider.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("Level2");
        }
    }
}
