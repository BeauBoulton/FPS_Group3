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

    public int weaponSelect;

    //private bool isInvincible;
    //private bool isBlinking = false; 

    // Reference to inventory script to get weapon select number
    public InventoryScript inventoryScript;

    //public PlayerController playerController;
    
    // Start is called before the first frame update
    void Start()
    {
        // Fills array with the mesh renderers of the wepons in the hud
        arrayMesh = GetComponentsInChildren<MeshRenderer>();

        // Disables all mesh renderers by default
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
        
        /*
        isInvincible = playerController.isInvincible;
        
        if (isInvincible)
        {
            if (!isBlinking)
            {
                StartCoroutine(BlinkTimer());
            }
        }
        */
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

    /*
    private void Blink()
    {
        if (weaponSelect == 0)
        {
            if (gameObject.name == "Pistol HUD")
            {
                foreach (MeshRenderer renderer in arrayMesh)
                {
                    renderer.enabled = false;
                    StartCoroutine(BlinkDelay());
                    renderer.enabled = true;
                }
            }
        }

        if (weaponSelect == 1)
        {
            if (gameObject.name == "Shotgun HUD")
            {
                foreach (MeshRenderer renderer in arrayMesh)
                {
                    renderer.enabled = false;
                    StartCoroutine(BlinkDelay());
                    renderer.enabled = true;
                }
            }
        }

        if (weaponSelect == 2)
        {
            if (gameObject.name == "Machine Gun HUD")
            {
                foreach (MeshRenderer renderer in arrayMesh)
                {
                    renderer.enabled = false;
                    StartCoroutine(BlinkDelay());
                    renderer.enabled = true;
                }
            }

        }
    }
    
    IEnumerator BlinkDelay()
    {
        yield return new WaitForSeconds(1f);
    }

    IEnumerator BlinkTimer()
    {
        isBlinking = true; 
        InvokeRepeating("Blink", 0, 2f);
        yield return new WaitForSeconds(playerController.iFramesTime);
        CancelInvoke("Blink"); 
        isBlinking = false;
    }
    */

}
