using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/*
 * Nick Sumek
 * updated 4/24/25
 * Sets up basic values for the items
 */
public class ItemScript : MonoBehaviour
{
    //declares item pick ups
    public int healthPack;
    public bool hasKeyCard;
    public int movementBoost;
    public int doubleDamage;
    public int playerHealth;

    // Variables for iframes
    private bool isInvincible = false;
    public int iFramesTime = 5;

    //sets up item pick ups
    private void OnTriggerEnter(Collider other)
    {
        //Health Pick Ups
            {
            if (other.gameObject.tag == "Player")
                {
                 playerHealth = 100;
                Destroy(gameObject);
                }
            }

        //sets up double damage
        if (other.gameObject.tag == "Player")
        {
           //get the projectile damage and multiply by 2
        }

        //sets up invuln pick up
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(IFrames());
            Destroy(gameObject);
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

            // Stops invoking Blink
            CancelInvoke("Blink");

        }







    }



    







    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
