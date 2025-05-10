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
    
    /*
    int MoveSpeed = 2;
    int MaxDist = 10;
    int MinDist = 5;
    */

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
        player = GameObject.Find("Player");
        playerPos = player.transform; 
        InvokeRepeating("SpawnProjectile", spawnDelay, timeBetweenShots); 
    }

    //spawns enemy projectile
    public void SpawnProjectile()
    {
        GameObject projectile = Instantiate(bullet, enemyGunPosition.position, transform.rotation);
        // Assign this script to the projectile script reference in the bullet
        projectile.GetComponent<EnemyProjectile>().enemyScript = this;

        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (enemyHealth <= 0)
        {
            Destroy (gameObject);
        }

        agent.SetDestination(playerPos.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile")

        {
            enemyHealth -= other.GetComponent<ProjectileScript>().damage;
        }
    }
}

