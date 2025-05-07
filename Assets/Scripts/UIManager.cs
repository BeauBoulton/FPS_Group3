using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/*
 * Name: Beau Boulton 
 * Last updated: 5/1/25
 * Description: Handles UI text for currently equipped weapon and health
 */

public class UIManager : MonoBehaviour
{
    public PlayerController playerController;
    public TMP_Text healthText;
    public TMP_Text weaponText; 

    // Update is called once per frame
    void Update()
    {
        // Displays current health
        if (playerController.currentPlayerHealth > 0)
        {
            healthText.text = "Health: " + playerController.currentPlayerHealth + "/" + playerController.maxPlayerHealth;
        }

        // Makes sure health doesn't go below 0 in display
        if (playerController.currentPlayerHealth <= 0)
        {
            healthText.text = "Health: 0" + "/" + playerController.maxPlayerHealth;
        }

        // Displays which weapon is equipped
        if (playerController.weaponSelect == 0)
        {
            weaponText.text = "Pistol Equipped"; 
        }

        if (playerController.weaponSelect == 1)
        {
            weaponText.text = "Shotgun Equipped";
        }
        
        if (playerController.weaponSelect == 2)
        {
            weaponText.text = "Machine Gun Equipped";
        }
    }
}
