using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWeapons : MonoBehaviour
{
    
    private MeshRenderer[] arrayPistolMesh;
    private MeshRenderer[] arrayShotgunMesh;
    private MeshRenderer[] arrayMachineGunMesh;

    public int weaponSelect; 

    public InventoryScript inventoryScript;
    

    // Start is called before the first frame update
    void Start()
    {
        
        arrayPistolMesh = GetComponentsInChildren<MeshRenderer>();
        arrayShotgunMesh = GetComponentsInChildren<MeshRenderer>();
        arrayMachineGunMesh = GetComponentsInChildren<MeshRenderer>();
        
        foreach (MeshRenderer renderer in arrayPistolMesh)
        {
            renderer.enabled = true;
        }

        foreach (MeshRenderer renderer in arrayShotgunMesh)
        {
            renderer.enabled = false;
        }

        foreach (MeshRenderer renderer in arrayMachineGunMesh)
            {
                renderer.enabled = false;
            }

    }

    // Update is called once per frame
    void Update()
    {
        
        weaponSelect = inventoryScript.weaponSelect;
        
    }
}
