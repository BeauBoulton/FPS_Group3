using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/*
 * Name: Beau Boulton
 * Last updated: 5/7/25
 * Description: Handles displaying the currently equipped weapon in the HUD
 */
public class UIWeapons : MonoBehaviour
{
    // Array to hold the mesh renderers of the ui items
    private MeshRenderer[] arrayMesh;

    [HideInInspector]
    public int weaponSelect;

    // Reference to inventory script to get weapon select number
    public InventoryScript inventoryScript;
    
    // Start is called before the first frame update
    void Start()
    {
        // Fills array with the mesh renderers of the wepons in the hud
        arrayMesh = GetComponentsInChildren<MeshRenderer>();

        // Disables all mesh renderers by default
        if (gameObject.name == "Pistol HUD")
        {
            // Loops through each mesh renderer in the array
            foreach (MeshRenderer renderer in arrayMesh)
            {
                // Disables the mesh
                renderer.enabled = false;
            }
        }

        if (gameObject.name == "Shotgun HUD")
        {
            foreach (MeshRenderer renderer in arrayMesh)
            {
                renderer.enabled = false;
            }
        }

        if (gameObject.name == "Machine Gun HUD")
        {
            foreach (MeshRenderer renderer in arrayMesh)
            {
                renderer.enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Assigns local weapon select variable to inventory script's weapon select value
        weaponSelect = inventoryScript.weaponSelect;

        WeaponSelector();
    }

    /// <summary>
    /// Enables and disables mesh renderers based on which weapon is selected
    /// </summary>
    private void WeaponSelector()
    {
        // If pistol is equipped
        if (weaponSelect == 0)
        {
            // Enable pistol renderers and disable all others
            if (gameObject.name == "Pistol HUD")
            {
                foreach (MeshRenderer renderer in arrayMesh)
                {
                   renderer.enabled = true;
                } 
            }
            
            if (gameObject.name == "Shotgun HUD")
            {
                foreach (MeshRenderer renderer in arrayMesh)
                {
                    renderer.enabled = false;
                }
            }

            if (gameObject.name == "Machine Gun HUD")
            {
                foreach (MeshRenderer renderer in arrayMesh)
                {
                    renderer.enabled = false;
                }
            }
        }
        
        // If shotgun is equipped
        if (weaponSelect == 1)
        {
            // Enable shotgun renderers and disable all others
            if (gameObject.name == "Pistol HUD")
            {
                foreach (MeshRenderer renderer in arrayMesh)
                {
                    renderer.enabled = false;
                }
            }
            
            if (gameObject.name == "Shotgun HUD")
            {
                foreach (MeshRenderer renderer in arrayMesh)
                {
                    renderer.enabled = true;
                }
            }

            if (gameObject.name == "Machine Gun HUD")
            {
                foreach (MeshRenderer renderer in arrayMesh)
                {
                    renderer.enabled = false;
                }
            }
        }
        
        // If machine gun is equipped
        if (weaponSelect == 2)
        {
            // Enable machine gun renderers and disable all others
            if (gameObject.name == "Pistol HUD")
            {
                foreach (MeshRenderer renderer in arrayMesh)
                {
                    renderer.enabled = false;
                }
            }
            
            if (gameObject.name == "Shotgun HUD")
            {
                foreach (MeshRenderer renderer in arrayMesh)
                {
                    renderer.enabled = false;
                }
            }

            if (gameObject.name == "Machine Gun HUD")
            {
                foreach (MeshRenderer renderer in arrayMesh)
                {
                    renderer.enabled = true;
                }
            }
        }
    }
}
