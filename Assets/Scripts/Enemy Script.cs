using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
/*
 * Nick Sumek
 * updated 5/6/25
 * Sets up basic enemy behavior
 */

public class EnemyScript : MonoBehaviour
{
    public GameObject player; 
    public Transform playerPos;

    public float sightRange;
    public int enemyDamage;
    public int enemyHealth;
    public Transform enemyGunPosition;
    public GameObject bullet;
    public NavMeshAgent agent;
    

    public float spawnDelay;
    public float timeBetweenShots;
   
    void Start()
    {
        // On spawn, finds player and assigns it to local variable
        player = GameObject.Find("Player");
        playerPos = player.transform; 
        
        // Starts spawning projectiles
        InvokeRepeating("SpawnProjectile", spawnDelay, timeBetweenShots); 
    }

    /// <summary>
    /// spawns enemy projectile
    /// </summary>
    public void SpawnProjectile()
    {
        GameObject projectile = Instantiate(bullet, enemyGunPosition.position, transform.rotation);
        // Assign this script to the enemy script reference in the bullet
        projectile.GetComponent<EnemyProjectile>().enemyScript = this;

        // Assign the agent to the nav mesh agent on this object
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // Destroy self if health reaches 0
        if (enemyHealth <= 0)
        {
            Destroy (gameObject);
        }

        // Set the player's position as the destination for the nav mesh agent
        agent.SetDestination(playerPos.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        // If hitting a projectile
        if (other.gameObject.tag == "Projectile")
        {
            // Get damage from projectile script and lose that amount of health
            enemyHealth -= other.GetComponent<ProjectileScript>().damage;
        }
    }
}

