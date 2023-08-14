using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed = 5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("Collided bullet!");
        Destroy(this.gameObject);
    }
}
