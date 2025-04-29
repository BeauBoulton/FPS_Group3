using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PlayerController playerController;
    public TMP_Text healthText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.currentPlayerHealth > 0)
        {
            healthText.text = "Health: " + playerController.currentPlayerHealth;
        }

        if (playerController.currentPlayerHealth <= 0)
        {
            healthText.text = "Health: 0";
        }
    }
}
