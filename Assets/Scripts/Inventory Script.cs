using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*
 * Name: Nick Sumek, Beau Boulton
 * Last updated: 5/6/25
 * Description: Uses an array to handle items held in inventory
 */

public class InventoryScript : MonoBehaviour
{
    //sets up arrayInventory
    public ItemScript[] arrayInventory;
    public int invetorySize = 6;

    // Handles which weapon is currently selected 0 = pistol and is default, 1 = shotgun, 2 = machine gun
    public int weaponSelect = 0;

    // Start is called before the first frame update
    void Start()
    {
        //sets the max array size to size set in the inspector (6 by default) 
        arrayInventory = new ItemScript[invetorySize];
    }


    private void OnTriggerEnter(Collider other)
    {
        // If collider is tagged as an item, adds it to the inventory 
        if (other.gameObject.tag == "Item")
        {
            // Retrieves script from item
            ItemScript newItem = other.gameObject.GetComponent<ItemScript>();
            if (AddItem(newItem))
            {
                // Finds which item was added most recently to inventory
                ItemScript lastItem = FindLastItem();

                // If last item was a weapon, instantly equips that weapon
                if (lastItem.name == "Shotgun")
                {
                    weaponSelect = 1;
                }

                if (lastItem.name == "Machine Gun")
                {
                    weaponSelect = 2;
                }

                // Disables item that was picked up
                other.gameObject.SetActive(false);
            }

            // If inventory is full
            else
            {
                print("Not enough room for " + newItem.name);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        WeaponSwap();
    }

    /// <summary>
    /// Looks for an empty slot in inventory and adds item to that slot, to be called in on trigger enter
    /// </summary>
    /// <param name="itemToAdd"></param>
    /// <returns>Returns whether item was successfully added to inventory</returns>
    private bool AddItem(ItemScript itemToAdd)
    {
        bool success = false;
        for (int i = 0; i < arrayInventory.Length; i++)
        {
            // If an empty inventory slot is found, the item is added
            if (arrayInventory[i] == null)
            {
                arrayInventory[i] = itemToAdd;
                success = true;
                break;
            }
        }
        return success;
    }

    /// <summary>
    /// Swaps weapons when pressing number keys
    /// </summary>
    private void WeaponSwap()
    {
        // Pressing 1 sets weapon to 0 (pistol)
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weaponSelect = 0;
        }

        // Pressing 2 sets weapon to 1 (shotgun)
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // Searches inventory for shotgun and only changes weapon select if there is a shotgun in inventory
            for (int i = 0; i < arrayInventory.Length; i++)
            {
                if (arrayInventory[i] != null)
                {
                    if (arrayInventory[i].name == "Shotgun")
                    {
                        weaponSelect = 1;
                        break;
                    }
                }
            }
        }

        // Pressing 3 sets weapon to 2 (machine gun)
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            // Searches inventory for machine gun and only changes weapon select if machine gun is in inventory
            for (int i = 0; i < arrayInventory.Length; i++)
            {
                if (arrayInventory[i] != null)
                {
                    if (arrayInventory[i].name == "Machine Gun")
                    {
                        weaponSelect = 2;
                        break;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Finds last item added to inventory. to be used to auto equip last weapon picked up
    /// </summary>
    /// <returns>Returns the last item added to inventory</returns>
    private ItemScript FindLastItem()
    {
        ItemScript lastItem = null;
        int itemsInInventory = 0;
        for (int i = 0; i < arrayInventory.Length; i++)
        {
            if (arrayInventory[i] != null)
            {
                itemsInInventory++;
            }
        }

        if (itemsInInventory != 0)
        {
            lastItem = arrayInventory[itemsInInventory - 1];
        }

        return lastItem;
    }
}
    