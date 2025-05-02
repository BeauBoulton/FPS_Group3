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
        arrayPistolMesh = new MeshRenderer[2];
        arrayShotgunMesh = new MeshRenderer[4];
        arrayMachineGunMesh = new MeshRenderer[10];
    }

    // Update is called once per frame
    void Update()
    {
        weaponSelect = inventoryScript.weaponSelect;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Pistol UI")
        {
            for (int i = 0; i < arrayPistolMesh.Length; i++)
            {
                MeshRenderer newMesh = other.gameObject.GetComponent<MeshRenderer>();
                AddMesh(newMesh);
            }
        }

        if (other.gameObject.name == "Shotgun UI")
        {
            for (int i = 0; i < arrayShotgunMesh.Length; i++)
            {
                MeshRenderer newMesh = other.gameObject.GetComponent<MeshRenderer>();
                AddMesh(newMesh);
            }
        }
        
        if (other.gameObject.name == "Machine Gun UI")
        {
            for (int i = 0; i < arrayMachineGunMesh.Length; i++)
            {
                MeshRenderer newMesh = other.gameObject.GetComponent<MeshRenderer>();
                AddMesh(newMesh);
            }
        }
    }

    private bool AddMesh(MeshRenderer meshToAdd)
    {
        bool success = false;
        for (int i = 0; i < arrayPistolMesh.Length; i++)
        {
            if (arrayPistolMesh[i] == null)
            {
                arrayPistolMesh[i] = meshToAdd;
                success = true;
                break;
            }
        }
        return success;
    }
}
