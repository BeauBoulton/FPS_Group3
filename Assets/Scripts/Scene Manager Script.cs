using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
/*
 * Nick Sumek
 * updated 5/6/25
 * Sets up scene transitions as well as play again and game over function/play again
 * also handles the portal
 */

public class SceneManagerScript : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();

    }

    public void SwitchScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }


    /// <summary>
    /// sets up the portal for loading different scenes
    /// </summary>
    public Vector3 teleportPoint;

    public GameObject spawnPoint;

    private void Start()
    {
        teleportPoint = spawnPoint.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        //sets pos of touched obj to the teleport point
        other.transform.position = teleportPoint;

    }



}
