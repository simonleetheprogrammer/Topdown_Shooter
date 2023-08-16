using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Spawns enemies randomly in the tiles.
/// Also acts as an exit for the player.
/// 
/// I made enemies an array as a hint for future development. I have a list of enemy prefabs to toss into the spawner.
/// </summary>
public class EnemySpawn : MonoBehaviour
{
    private GameObject[] enemies = new GameObject[1] { null };
    private float spawnInterval = 3;
    private float timeUntilSpawn = 0;

    private void Awake()
    {
        GameObject enemy = Resources.Load<GameObject>("Prefabs/Enemy");
        enemies[0] = enemy;

    }
    private void FixedUpdate()
    {
    }
    private void Spawn(GameObject[] spawns)
    {
        timeUntilSpawn -= Time.deltaTime;
        if (timeUntilSpawn <= 0)
        {
            Instantiate(spawns[0], transform.position, Quaternion.identity);
            timeUntilSpawn = spawnInterval;
        }

    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        print("Yes");
        if (collider.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("Level2");
        }
    }
}
