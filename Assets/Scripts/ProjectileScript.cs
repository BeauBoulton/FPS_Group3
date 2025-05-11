using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Name: Beau Boulton, Nick Sumek
 * Last updated: 5/10/25
 * Description: handles movement and damage of projectile
 */

public class ProjectileScript : MonoBehaviour
{
    public int damage = 1; 
    public float speed = 10;
    Vector3 spawnPos;
    
    [HideInInspector]
    public bool doubleDamageBullet = false;

    [HideInInspector]
    public PlayerController playerController; 

    // Start is called before the first frame update
    void Start()
    {
        // Sets spawn position at the current position when spawning
        spawnPos = transform.position;

        // On spawn, checks if the player script has double damage power up active
        doubleDamageBullet = playerController.GetComponent<PlayerController>().doubleDamage;
        
        // If player controller has double damage active, double damage of bullet
        if (doubleDamageBullet == true)
        {
            damage = (damage * 2);
        }
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
        // Bullet destroys itself when colliding with anything other than the player
        if (other.gameObject.tag != "Player")
        {
            // Also checks to not destroy itself when colliding with enemy bullets
            if (other.gameObject.tag != "Enemy Projectile")
            {
                Destroy(gameObject);
            }
        }
    }
}
