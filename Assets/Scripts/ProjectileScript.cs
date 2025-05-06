using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public int damage = 1; 
    public float speed = 10;
    Vector3 spawnPos;
    public bool enemyProjectile;
    public bool doubleDamage = false;
    // Start is called before the first frame update
    void Start()
    {
        spawnPos = transform.position; 
        if(doubleDamage == true)
        {
            damage = damage * 2;
        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += speed * transform.forward * Time.deltaTime;

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

    /// <summary>
    /// sets up checks for how the projectiles are handled
    /// based on which objects are shhooting them
    /// </summary>
    /// <param name="other"></param>

    private void OnTriggerEnter(Collider other)
    {
        if (enemyProjectile == false)
        {
            if (other.gameObject.tag != "Player")
            {
                Destroy(gameObject);
            }
        }

        else if (enemyProjectile == true)
        {
            if (other.gameObject.tag != "Enemy")
            {
                Destroy(gameObject);
            }
        }

    }
}
