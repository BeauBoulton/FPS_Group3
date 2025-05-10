using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Name: Nick Sumek, Beau Boulton
 * Last updated: 5/6/25
 * Description: Handles movement and damage for projectiles fired by enemies
 */

public class EnemyProjectile : MonoBehaviour
{
    public int damage = 1;
    public float speed = 10;
    Vector3 spawnPos;
    public EnemyScript enemyScript;

    // Start is called before the first frame update
    void Start()
    {
        // Sets spawn position at the current position when spawning
        spawnPos = transform.position;

        damage = enemyScript.GetComponent<EnemyScript>().enemyDamage;
    }

    // Update is called once per frame
    void Update()
    {
        // Start moving foraward based on speed set in inspector
        transform.position += speed * transform.forward * Time.deltaTime;

        // If projectile moves more than 20 units away from spawn position in any direction it despawns
        if (transform.position.x > (spawnPos.x + 20) || transform.position.x < (spawnPos.x - 20))
        {
            Destroy(gameObject);
        }
        if (transform.position.y > (spawnPos.y + 20) || transform.position.y < (spawnPos.y - 20))
        {
            Destroy(gameObject);
        }
        if (transform.position.z > (spawnPos.z + 20) || transform.position.z < (spawnPos.z - 20))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Bullet destroys itself if hitting anything other than an enemy
        if (other.gameObject.tag != "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
