using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static Unity.VisualScripting.Antlr3.Runtime.Tree.TreeWizard;
using UnityEngine.SceneManagement;
/*
 * Name: Beau Boulton, Nick Sumek
 * Last updated: 5/9/25
 * Description: Handles player movement, health, and shooting
 */
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

    //modifies player movement
    public int speedTimer = 5;
    [HideInInspector]
    public float normalSpeed;
    public float speedMultiplier;
    
    // Direction of movement
    private Vector3 moveDirection;
    
    private Rigidbody rigidBody;

    // Bullet and where to spawn bullet
    public GameObject bullet;
    public GameObject shotgunBlast;
    private GameObject bulletToUse;
    [HideInInspector]
    public int weaponSelect; 
    public Transform gunPosition;
    private bool canFire = true;
    private float shotCoolDown;
    public float pistolCoolDown;
    public float shotgunCoolDown;
    private bool machineGunIsFiring;

    //damage modifiers
    public int doubleDamageTime = 5;
    [HideInInspector]
    public bool doubleDamage = false;
   
    // Health variables
    public int maxPlayerHealth = 100;
    [HideInInspector]
    public int currentPlayerHealth = 100;
    [HideInInspector]
    public bool isInvincible = false;
    [HideInInspector]
    public bool isInvulnPowerUp = false; 
    //I frames time if player is hit
    public int iFramesHit;
    //I frames time if player gets invuln power up
    public int iFramesPowerUp; 
    public int iFramesTime;
    [HideInInspector]
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

        // Pulls weapon select variable from inventory and sets it to local variable
        weaponSelect = gameObject.GetComponent<InventoryScript>().weaponSelect;

        // Spawns bullet when lmb is pressed
        if (canFire && Input.GetKeyDown(KeyCode.Mouse0))
        {
            // Check if machine gun is equipped and invoke repeating the spawn projectile, else spawn it normally
            if (weaponSelect == 2)
            {
                InvokeRepeating("SpawnProjectile", 0, 0.1f);
                machineGunIsFiring = true;
            }

            else if (weaponSelect != 2)
            {
                {
                    SpawnProjectile();
                    StartCoroutine(ShotDelay());
                }
            }
        }

        // If mouse is released or if a different weapon is selected, cancel the machine gun fire
        if (Input.GetKeyUp(KeyCode.Mouse0) || weaponSelect == 0 || weaponSelect == 1)
        {
            if (machineGunIsFiring)
            {
                CancelInvoke("SpawnProjectile");
                machineGunIsFiring = false;
            }
        }
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }
    
    public void OnCollisionEnter(Collision collision)
    {
        // If the object colliding is tagged as Enemy, decrease health
        if (collision.gameObject.tag == "Enemy")
        {
            // Checks if the player is not invincible so that health isn't removed during iframes
            if (!isInvincible)
            {
                // Gets enemy damage variable from enemy and sets it to the local enemyDamage variable
                 enemyDamage = collision.gameObject.GetComponent<EnemyScript>().enemyDamage;
                // Removes health
                currentPlayerHealth -= enemyDamage;

                // Loads game over screen if health hits, else starts i frames
                if (currentPlayerHealth <= 0)
                {
                    SceneManager.LoadScene(6);
                }
                else
                {
                    StartCoroutine(IFrames());
                }

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If item is health, get the health amount from the item
        if(other.gameObject.tag == "Health")
        {
            // Only increase health if current hp is lower than max hp
            if (currentPlayerHealth < maxPlayerHealth)
            {
                currentPlayerHealth += other.gameObject.GetComponent<ItemScript>().playerHealth;
                
                // If current hp is higher than max hp, reset it to max hp
                if (currentPlayerHealth > maxPlayerHealth)
                {
                    currentPlayerHealth = maxPlayerHealth;
                }
            }
        }

        // If item is speed boost, start speed boost coroutine
        if(other.gameObject.tag == "Move")
        {
            // Get speed boost multiplier from item script
            speedMultiplier = other.GetComponent<ItemScript>().speedMultiplier; 
            StartCoroutine(SpeedBoostTimer());
        }

        // If item is double damage buff, start double damage coroutine
        if(other.gameObject.tag == "Damage Buff")
        {
            StartCoroutine(DoubleDamageTimer()); 
        }

        // If item is invuln, start i frames
        if (other.gameObject.tag == "Invulnerability")
        {
            isInvulnPowerUp = true; 
            StartCoroutine(IFrames()); 
        }

        //checks to see if the player is shot
        if (other.gameObject.tag == "Enemy Projectile")
        {
            // Checks if the player is not invincible so that health isn't remuved during iframes
            if (!isInvincible)
            {
                // Gets enemy damage variable from enemy projectile and sets it to the local enemy damage variable
                enemyDamage = other.gameObject.GetComponent<EnemyProjectile>().damage;
                // Removes health
                currentPlayerHealth -= enemyDamage;

                // Loads game over screen if health hits, else starts i frames
                if (currentPlayerHealth <= 0)
                {
                    SceneManager.LoadScene(6);
                }
                else
                {
                    StartCoroutine(IFrames());
                }
            }
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

        // Jump when space is pressed
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
       // Saves the current velocity in a new variable
        Vector3 flatVel = new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z);
        
        // If the velocity is higher than move speed, sets the current velocity to the move speed
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
        
        // If weaponSelect is 1 (shotgun), use shotgun pellets and shotgun cool down
        if (weaponSelect == 1)
        {
            bulletToUse = shotgunBlast;
            shotCoolDown = shotgunCoolDown;
        }
        
        // Weapon selct 2 (machine gun) uses the same as pistol settings
        if (weaponSelect == 2)
        {
            bulletToUse = bullet;
            shotCoolDown = pistolCoolDown; 
        }
        
        // Spawn chosen projectile at gun position facing the same direction the camera is facing
        GameObject projectile = Instantiate(bulletToUse, gunPosition.position, cameraOrientation.rotation);
        
        // Assign this script to the projectile script reference in the bullet
        projectile.GetComponent<ProjectileScript>().playerController = this;
    }

    /// <summary>
    /// This is a coroutine, it is a timer. It makes the player invincible for iFramesTime number of seconds
    /// </summary>
    /// <returns></returns>
    IEnumerator IFrames()
    {
        // If iframes are from the power up, sets iframes time to the duration for the power up
        if (isInvulnPowerUp == true)
        {
            iFramesTime = iFramesPowerUp;
        }

        // Else iframe time is the duration for being hit
        else
        {
            iFramesTime = iFramesHit;
        }

        // Set isInvincible to true so player can't take damage while coroutine is running
        isInvincible = true;
        // Sets a timer for iFramesTime number of seconds
        yield return new WaitForSeconds(iFramesTime);
        // Removes invincibility
        isInvincible = false;
        isInvulnPowerUp = false;
    }

    /// <summary>
    /// This coruoutine is a timer that sets a delay in between shotgun and pistol shots
    /// </summary>
    /// <returns></returns>
    IEnumerator ShotDelay()
    {
        canFire = false;
        yield return new WaitForSeconds(shotCoolDown);
        canFire = true;
    }

    /// <summary>
    /// Sets up the speed boost function/ timer
    /// </summary>
    /// <returns></returns>
    IEnumerator SpeedBoostTimer()
    {
        // Saves current move speed in normalSpeed variable
        normalSpeed = moveSpeed;

        // Multiplies speed by amount set in inspector
        moveSpeed = moveSpeed * speedMultiplier;

        // Sets a timer for speedTimer number of seconds
        yield return new WaitForSeconds(speedTimer);

        // Removes speed boost
        moveSpeed = normalSpeed; 
    }

    /// <summary>
    /// sets up double damage timer
    /// </summary>
    /// <returns></returns>
    IEnumerator DoubleDamageTimer()
    {
        // Sets bool to be pulled by projectile script to true
        doubleDamage = true;

        // Sets a timer for doubleDamageTime number of seconds
        yield return new WaitForSeconds(doubleDamageTime);

        // Removes double damage
        doubleDamage = false;
    }
}
