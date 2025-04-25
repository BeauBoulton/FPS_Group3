using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public int damage = 1; 
    public float speed = 10;
    Vector3 spawnPos; 

    // Start is called before the first frame update
    void Start()
    {
        spawnPos = transform.position; 
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
}
