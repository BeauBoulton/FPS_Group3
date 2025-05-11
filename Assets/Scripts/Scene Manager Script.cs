using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
/*
 * Nick Sumek
 * updated 5/6/25
 * Sets up scene transitions and spawn points for portals
 */

public class SceneManagerScript : MonoBehaviour
{
    public int sceneIndex;
    public GameObject spawnPoint;

    /// <summary>
    /// Loads the scene set in inspector
    /// </summary>
    public void SwitchScene()
    {
        SceneManager.LoadScene(sceneIndex);
    }

    private void OnTriggerEnter(Collider other)
    {
        // If player collides
        if (other.gameObject.tag == "Player")
        {
            //sets pos of touched obj to the teleport point
            other.transform.position = spawnPoint.transform.position;
            SwitchScene();
        }
    }



}
