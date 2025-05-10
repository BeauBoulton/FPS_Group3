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
    public int sceneIndex;
    public GameObject spawnPoint;
    public void QuitGame()
    {
        Application.Quit();

    }

    public void SwitchScene()
    {
        SceneManager.LoadScene(sceneIndex);
    }

    private void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //sets pos of touched obj to the teleport point
            other.transform.position = spawnPoint.transform.position;
            SwitchScene();
        }
    }



}
