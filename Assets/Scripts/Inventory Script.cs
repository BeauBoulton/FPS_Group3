using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*
 * Nick Sumek
 * updated 4/24/25
 * Sets up basic array for item pick ups
 */

public class InventoryScript : MonoBehaviour
{
    //sets up arrayInventory
    public ItemScript[] arrayInventory;
    public int invetorySize = 6;

    public int weaponSelect;

   

    // Start is called before the first frame update
    void Start()
    {
        //sets the max array size to 6 
        arrayInventory = new ItemScript[invetorySize];
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Item")
        {
            ItemScript newItem = other.gameObject.GetComponent<ItemScript>();
            if (AddItem(newItem))
            {
                ItemScript lastItem = FindLastItem();

                if (lastItem.name == "Shotgun")
                {
                    weaponSelect = 1;
                }

                if (lastItem.name == "Machine Gun")
                {
                    weaponSelect = 2;
                }

                other.gameObject.SetActive(false);
            }

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


    private bool AddItem(ItemScript itemToAdd)
    {
        bool success = false;
        for (int i = 0; i < arrayInventory.Length; i++)
        {
            if (arrayInventory[i] == null)
            {
                arrayInventory[i] = itemToAdd;
                success = true;
                break;
            }
        }
        return success;
    }

    private void WeaponSwap()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weaponSelect = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            for (int i = 0; i < arrayInventory.Length; i++)
            {
                if (arrayInventory[i].name == "Shotgun")
                {
                    weaponSelect = 1;
                    break;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            for (int i = 0; i < arrayInventory.Length; i++)
            {
                if (arrayInventory[i].name == "Machine Gun")
                {
                    weaponSelect = 2;
                    break;
                }
            }
        }


    }

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
    