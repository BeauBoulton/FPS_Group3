using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rigidBody;
    public int speed;
    public GameObject bullet;
    public Vector3 bulletSpawn;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnProjectile();
        }
        
        float translation = Input.GetAxis("Vertical") * 10 * Time.deltaTime;
        float straffe = Input.GetAxis("Horizontal") * 10 * Time.deltaTime;

        transform.Translate(straffe, 0, translation);
    }

    public void SpawnProjectile()
    {
        bulletSpawn.x = transform.position.x;
        bulletSpawn.z = transform.position.z; 
        bulletSpawn.y = transform.position.y - 0.2f; 
        GameObject projectile = Instantiate(bullet, bulletSpawn, transform.rotation);
    }
}
