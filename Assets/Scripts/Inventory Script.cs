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
    private ItemScript[] arrayInventory;
    public int invetorySize = 6;

    // Start is called before the first frame update
    void Start()
    {
        //sets the max array size to 6 
        arrayInventory = new ItemScript[invetorySize];
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Key Card")
        {
            ItemScript newItem = other.gameObject.GetComponent<ItemScript>();
            if (AddItem(newItem))
                other.gameObject.SetActive(false);

        }
    }


    // Update is called once per frame
    void Update()
    {

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
}
    