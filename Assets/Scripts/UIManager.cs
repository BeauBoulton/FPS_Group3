using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PlayerController playerController;
    public TMP_Text healthText;
    public TMP_Text weaponText; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.currentPlayerHealth > 0)
        {
            healthText.text = "Health: " + playerController.currentPlayerHealth + "/" + playerController.maxPlayerHealth;
        }

        if (playerController.currentPlayerHealth <= 0)
        {
            healthText.text = "Health: 0" + "/" + playerController.maxPlayerHealth;
        }

        if (playerController.weaponSelect == 0)
        {
            weaponText.text = "Pistol Equipped"; 
        }

        if (playerController.weaponSelect == 1)
        {
            weaponText.text = "Shotgun Equipped";
        }
    }
}
