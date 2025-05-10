using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/*
 * Nick Sumek
 * updated 5/6/25
 * Sets up basic function for the locked door
 */
public class DoorScript : MonoBehaviour
{
    public int blueLock;
    public bool hasKeyCard = false;
    private ItemScript[] arrayInventory;

    //sets a check  to see if the player has the key
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<InventoryScript>())
        {
            arrayInventory = collision.gameObject.GetComponent<InventoryScript>().arrayInventory;

            for (int i = 0; i < arrayInventory.Length; i++)
            {
                //if the player has the key card 
                if (arrayInventory[i].name == "Key Card")
                {
                    //gets rid of the locked door and uses the key 
                    Destroy(arrayInventory[i].gameObject);   
                    Destroy (gameObject);
                }
            }
        }
    }
}
