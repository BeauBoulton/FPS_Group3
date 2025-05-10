using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/*
 * Nick Sumek
 * updated 5/6/25
 * Sets up basic values for the items
 */

public class ItemScript : MonoBehaviour
{
    //declares item pick ups
    public float speedMultiplier;
    public int playerHealth;

    public bool destructibleItem = false;
   
    //sets up item pick ups
    private void OnTriggerEnter(Collider other)
    {
        // Some items are destructible but some items have to be held in player inventory
        // If it's an item thet isn't held in inventory, destroy the game object on pickup
        if (destructibleItem == true)
        {
            if (other.gameObject.tag == "Player")
            {
                Destroy(gameObject);
            }
        }
    }
}
