using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
 * Name: Beau Boulton
 * Last updated: 5/9/25
 * Description: Handles scene transition when player collides with the end of level portal
 */

public class PortalScript : MonoBehaviour
{
    // Set which scene to teleport to in the inspector
    public int sceneIndex;

    private void OnTriggerEnter(Collider other)
    {
        // If player collides with portal, transition to set scene
        if (other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }
}
