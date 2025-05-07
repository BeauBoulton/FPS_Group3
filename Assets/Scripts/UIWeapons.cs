using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Name: Beau Boulton
 * Last updated: 5/7/25
 * Description: Handles displaying the currently equipped weaponin the HUD
 */
public class UIWeapons : MonoBehaviour
{
    // Arrays to hold the mesh renderers of the ui items
    private MeshRenderer[] arrayPistolMesh;
    private MeshRenderer[] arrayShotgunMesh;
    private MeshRenderer[] arrayMachineGunMesh;

    public int weaponSelect; 

    // Reference to inventory script to get weapon select number
    public InventoryScript inventoryScript;
    

    // Start is called before the first frame update
    void Start()
    {
        // Fills arrays with the mesh renderers of the wepons in the hud
        arrayPistolMesh = GetComponentsInChildren<MeshRenderer>();
        arrayShotgunMesh = GetComponentsInChildren<MeshRenderer>();
        arrayMachineGunMesh = GetComponentsInChildren<MeshRenderer>();

        // Disables all mesh renderers by default
        if (gameObject.name == "Pistol HUD")
        {
            foreach (MeshRenderer renderer in arrayPistolMesh)
            {
                renderer.enabled = false;
            }
        }

        if (gameObject.name == "Shotgun HUD")
        {
            foreach (MeshRenderer renderer in arrayShotgunMesh)
            {
                renderer.enabled = false;
            }
        }

        if (gameObject.name == "Machine Gun HUD")
        {
            foreach (MeshRenderer renderer in arrayMachineGunMesh)
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

    private void WeaponSelector()
    {
        // If pistol is equipped
        if (weaponSelect == 0)
        {
            // Enable pistol renderers and disable all others
            if (gameObject.name == "Pistol HUD")
            {
                foreach (MeshRenderer renderer in arrayPistolMesh)
                {
                    renderer.enabled = true;
                }
            }
            
            if (gameObject.name == "Shotgun HUD")
            {
                foreach (MeshRenderer renderer in arrayShotgunMesh)
                {
                    renderer.enabled = false;
                }
            }

            if (gameObject.name == "Machine Gun HUD")
            {
                foreach (MeshRenderer renderer in arrayMachineGunMesh)
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
                foreach (MeshRenderer renderer in arrayPistolMesh)
                {
                    renderer.enabled = false;
                }
            }
            
            if (gameObject.name == "Shotgun HUD")
            {
                foreach (MeshRenderer renderer in arrayShotgunMesh)
                {
                    renderer.enabled = true;
                }
            }

            if (gameObject.name == "Machine Gun HUD")
            {
                foreach (MeshRenderer renderer in arrayMachineGunMesh)
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
                foreach (MeshRenderer renderer in arrayPistolMesh)
                {
                    renderer.enabled = false;
                }
            }
            
            if (gameObject.name == "Shotgun HUD")
            {
                foreach (MeshRenderer renderer in arrayShotgunMesh)
                {
                    renderer.enabled = false;
                }
            }

            if (gameObject.name == "Machine Gun HUD")
            {
                foreach (MeshRenderer renderer in arrayMachineGunMesh)
                {
                    renderer.enabled = true;
                }
            }
        }
    }
}
