using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Name: Beau Boulton
 * Last Updated: 5/10/25
 * Description: Finds the player and sets itself as the parent to remove the DontDestroyOnLoad function from the player system. 
 * This is used in the game over and you win screens to destroy the current instance of the player when play again is selected. 
 */

public class DestroyPlayer : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        // Find player and assign it to local game object variable
        player = GameObject.Find("PlayerSystem");

        // Set this object as the player's parent
        player.transform.SetParent(gameObject.transform);

        // Disable the canvas in the player so that the hud elements don't show up in the you win or game over screen
        GetComponentInChildren<Canvas>().enabled = false; 
    }
}
