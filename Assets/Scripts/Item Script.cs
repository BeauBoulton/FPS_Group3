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
    public float speedMultiplier;
    public int playerHealth;

    public bool isInvuln = false;
    public bool isHealth = false;
    public bool doubleDamage =false;
    public bool speedBoost =false;
    // Variables for iframes
    private bool isInvincible = false;
    public int iFramesTime = 5;
   

    //sets up item pick ups
    private void OnTriggerEnter(Collider other)
    {
        //Health Pick Ups
        
        if (isHealth == true)
        {
            if (other.gameObject.tag == "Player")
            {
                Destroy(gameObject);
            }
        }

        
        

        //sets up invuln pick up
        if (isInvuln == true)
        {
            if (other.gameObject.tag == "Player")
            {
                StartCoroutine(IFrames());
                Destroy(gameObject);
            }
        }


        
        if(speedBoost == true)
        {
            if(other.gameObject.tag == "Player")
                { 
                Destroy(gameObject); 
                }


        }
        if (doubleDamage == true)
        {
            if (other.gameObject.tag == "Player")
            {
                Destroy(gameObject);
            }


        }
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

    // This is a coroutine, it is a timer. It makes the player deal double damage for a time
    



}
