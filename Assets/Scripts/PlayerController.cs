using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static Unity.VisualScripting.Antlr3.Runtime.Tree.TreeWizard;

public class PlayerController : MonoBehaviour
{
    // Movement variables
    public float moveSpeed;
    public float groundDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump = true; 

    // To check if touching ground
    bool grounded; 

    // Orientation of orientation object and camera object
    public Transform orientation;
    public Transform cameraOrientation; 

    // For storing movement inputs
    private float horizontalInput; 
    private float verticalInput;

    // Direction of movement
    private Vector3 moveDirection;

    private float distanceToWallRight; 
    private float distanceToWallLeft; 
    private float distanceToWallFront; 
    private float distanceToWallBack; 
    
    private Rigidbody rigidBody;

    // Bullet and where to spawn bullet
    public GameObject bullet;
    public GameObject shotgunBlast;
    private GameObject bulletToUse;
    public int weaponSelect; 
    public Transform gunPosition;
    private bool canFire = true;
    private float shotCoolDown;
    public float pistolCoolDown;
    public float shotgunCoolDown; 


    // Health variables
    public int maxPlayerHealth = 100;
    public int currentPlayerHealth = 100;
    private bool isInvincible = false;
    public int iFramesTime = 5;
    public int enemyDamage = 15;
 

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Raycast down to find the ground
        if (Physics.Raycast(transform.position, Vector3.down, 1.5f))
        {
            grounded = true;
        }

        else
        {
            grounded = false;
        }

        PlayerInput(); 

        SpeedControl();

        // Adds drag if player is touching the ground
        if (grounded)
        {
            rigidBody.drag = groundDrag; 
        }
        else if (!grounded)
        {
            rigidBody.drag = 0; 
        }

        weaponSelect = gameObject.GetComponent<InventoryScript>().weaponSelect;

        // Spawns bullet when lmb is pressed
        if (canFire && Input.GetKeyDown(KeyCode.Mouse0))
        {
            SpawnProjectile();
            StartCoroutine(ShotDelay());
        }

        /*
        if ((distanceToWallBack <= 0.6 && verticalInput > 0) || (distanceToWallFront <= 0.6 && verticalInput < 0))
        {
            verticalInput = 0;
        }
        if ((distanceToWallRight <= 0.6 && horizontalInput < 0) || (distanceToWallLeft <= 0.6 && horizontalInput > 0))
        {
            horizontalInput = 0;
        }
        */
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        // If the object colliding is tagged as Hazard, decrease health
        if (collision.gameObject.tag == "Enemy")
        {
            // Checks if the player is not invincible so that health isn't remuved during iframes
            if (!isInvincible)
            {
                // Gets enemy damage variable from enemy and sets it to the local enemyDamage variable
                 enemyDamage = collision.gameObject.GetComponent<EnemyScript>().enemyDamage;
                // Removes health and starts iframes
                currentPlayerHealth -= enemyDamage;
                StartCoroutine(IFrames());
            }
        }
    }


    /// <summary>
    /// Sets up the colliders for health and othe buff pick ups
    /// </summary>

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Health")
        {
            currentPlayerHealth += other.gameObject.GetComponent<ItemScript>().playerHealth;
        }


    }
    /// <summary>
    /// Assigns player inputs to variables to use in the movement script
    /// </summary>
    private void PlayerInput()
    {
        // Gets player inputs and assigns them to variables
        horizontalInput = Input.GetAxisRaw("Horizontal"); 
        verticalInput = Input.GetAxisRaw("Vertical"); 

        if (Input.GetKeyDown (KeyCode.Space) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump(); 
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    /// <summary>
    /// Takes keyboard inputs and translates them into player movement
    /// </summary>
    private void PlayerMove()
    {
        // Direction is the orientation direction * keyboard inputs
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // Adds force to the player in the direction they are facing
        // On ground
        if (grounded)
        {
            rigidBody.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }

        // In air
        else if (!grounded)
        {
            rigidBody.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    /// <summary>
    /// Caps the max velocity to the speed set in inspector
    /// </summary>
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z);
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed; 
            rigidBody.velocity = new Vector3(limitedVel.x, rigidBody.velocity.y, limitedVel.z);
        }
    }

    /// <summary>
    /// Handles jumping
    /// </summary>
    private void Jump()
    {
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z); 

        rigidBody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    /// <summary>
    /// Prevents player from jumping again while in air and lets them continue jumping while holding down space and on the ground
    /// </summary>
    private void ResetJump()
    {
        readyToJump = true; 
    }

    /// <summary>
    /// Finds the distance to the nearest wall and halts movement if touching it
    /// </summary>
    /*private void DistanceToWall()
    {
        RaycastHit hit;
        Ray rightRay = new Ray(transform.position, transform.right);
        Ray leftRay = new Ray(transform.position, -transform.right);
        Ray frontRay = new Ray(transform.position, transform.forward);
        Ray backRay = new Ray(transform.position, -transform.forward);

        if (Physics.Raycast(rightRay, out hit) && hit.collider.tag == "Wall")
        {
            distanceToWallRight = hit.distance; 
        }
        else
        {
            distanceToWallRight = 3;
        }
        
        if (Physics.Raycast(leftRay, out hit) && hit.collider.tag == "Wall")
        {
            distanceToWallLeft = hit.distance; 
        }
        else
        {
            distanceToWallLeft = 3;
        }
        
        if (Physics.Raycast(frontRay, out hit) && hit.collider.tag == "Wall")
        {
            distanceToWallFront = hit.distance; 
        }
        else
        {
            distanceToWallFront = 3;
        }
        
        if (Physics.Raycast(backRay, out hit) && hit.collider.tag == "Wall")
        {
            distanceToWallBack = hit.distance; 
        }
        else
        {
            distanceToWallBack = 3;
        }

    }
    */

    /// <summary>
    /// Spawns bullet
    /// </summary>
    public void SpawnProjectile()
    {
        // Bullet to use is normal bullet by default
        if (weaponSelect == 0)
        {
            bulletToUse = bullet;
            shotCoolDown = pistolCoolDown;
        }
        
        // If hasHeavyBullets form PlayerController is true, bullet to use is heavy bullet
        if (weaponSelect == 1)
        {
            bulletToUse = shotgunBlast;
            shotCoolDown = shotgunCoolDown;
        }
        GameObject projectile = Instantiate(bulletToUse, gunPosition.position, cameraOrientation.rotation);
    }

    // This is a coroutine, it is a timer. It makes the player invincible and invokes Blink repeating for iFramesTime number of seconds
    IEnumerator IFrames()
    {
        // Set isInvincible to true so player can't take damage while coroutine is running
        isInvincible = true;
        // Sets a timer for iFramesTime number of seconds
        yield return new WaitForSeconds(iFramesTime);
        // Removes invincibility
        isInvincible = false;
    }

    IEnumerator ShotDelay()
    {
        canFire = false;
        yield return new WaitForSeconds(shotCoolDown);
        canFire = true;
    }
}
