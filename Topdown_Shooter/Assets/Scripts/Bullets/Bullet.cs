using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float BulletSpeed { get; set; } = 3f;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("Collided bullet!");
        Destroy(this.gameObject);
    }
}
