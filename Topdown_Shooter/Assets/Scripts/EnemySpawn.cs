using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
/// <summary>
/// Spawns enemies randomly in the tiles.
/// Also acts as an exit for the player.
/// 
/// I made enemies an array as a hint for future development. I have a list of enemy prefabs to toss into the spawner.
/// </summary>
public class EnemySpawn : MonoBehaviour
{
    private GameObject[] enemies = new GameObject[1] { null };
    private float spawnInterval = 1;
    private float timeUntilSpawn = 0;

    private List<Vector3> spawnPositions = new List<Vector3>();
    [SerializeField]
    private LevelManager levelManager;
    /// <summary>
    /// Get the enemy prefab.
    /// Get the positions of the tile spawners.
    /// The gameObject will spawn in the bottomleft corner of the spawner. Thus requires the offset.
    /// </summary>
    private void Awake()
    {
        GameObject enemy = Resources.Load<GameObject>("Prefabs/Enemy");
        enemies[0] = enemy;

        Tilemap spawnTilemap = GetComponent<Tilemap>();
        BoundsInt bounds = spawnTilemap.cellBounds;
        TileBase[] allTiles = spawnTilemap.GetTilesBlock(bounds);

        BoxCollider2D enemyCollider = enemy.GetComponent<BoxCollider2D>();
        Vector3 offset = new Vector3(enemyCollider.size.x, enemyCollider.size.y, 0);

        for (int x = 0; x< bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                Vector3Int cellPosition = new Vector3Int(bounds.x + x, bounds.y + y, bounds.position.z);
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile is not null)
                {
                    spawnPositions.Add(cellPosition + offset);
                }
            }
        }
    }
    private void FixedUpdate()
    {
        Spawn(enemies, spawnPositions);
    }
    /// <summary>
    /// Creates a spawn every interval in a random spawn position
    /// </summary>
    /// <param name="spawns"></param>
    /// <param name="spawnPositions"></param>
    private void Spawn(GameObject[] spawns, List<Vector3> spawnPositions)
    {
        
        timeUntilSpawn -= Time.deltaTime;
        if (timeUntilSpawn <= 0)
        {
            timeUntilSpawn = spawnInterval; 
            int randomIndex = Random.Range(0, spawnPositions.Count);
            GameObject enemy = Instantiate(spawns[0], spawnPositions[randomIndex], Quaternion.identity);
            EnemyMethods enemyMethods = enemy.GetComponent<EnemyMethods>();
            enemyMethods.Manager = levelManager;
        }
    }
    /// <summary>
    /// Go to Level2
    /// </summary>
    /// <param name="collider"></param>
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" && levelManager.KillCount >= levelManager.RequiredKills)
        {
            SceneManager.LoadScene("Level2");
        }
    }
}
