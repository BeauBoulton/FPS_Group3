using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
/*
 * Nick Sumek
 * updated 4/24/25
 * Sets up basic enemy behavior
 */


public class EnemyScript : MonoBehaviour
{
    public Transform Player;
    int MoveSpeed = 2;
    int MaxDist = 10;
    int MinDist = 5;
    public int enemyDamage;
    public int enemyHealth;
    public Transform enemyGunPosition;
    public GameObject bullet;
    public NavMeshAgent agent;
    public float sightRange;
    //private GameObject playerTracking;


    public float spawnDelay;
    public float timeBetweenShots;
    void Start()
    {
        InvokeRepeating("SpawnProjectile", spawnDelay, timeBetweenShots); 


    }

    //spawns enemy projectile
    public void SpawnProjectile()
    {
        GameObject projectile = Instantiate(bullet, enemyGunPosition.position, transform.rotation);
    }


    void Update()
    {
        
        //transform.LookAt(Player);

       

        
        if (enemyHealth <= 0)
        {
            Destroy (gameObject);
        }

        /*
        //sets up enemy movement using the nav mesh
       RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward*sightRange, Color.red);
        
        if (Physics.Raycast( transform.position,transform.forward, out hit, sightRange))
        {
            if (hit.collider.tag == "Player")
            {
                agent.SetDestination(hit.point);
            }
        }
        */


        agent.SetDestination(Player.transform.position);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile")

        {
            enemyHealth -= other.GetComponent<ProjectileScript>().damage;
        }
    }
}

