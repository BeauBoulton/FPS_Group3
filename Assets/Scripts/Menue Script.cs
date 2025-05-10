using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Name: Nick
 * Last updated: 5/10/25
 * Description: Handles scene transition from menues
 */

public class MenueScript : MonoBehaviour
{
    public int sceneIndex;
    //quits the app
    public void QuitGame()
    {
        Application.Quit();

    }
    //sets up scene selection
    public void SwitchScene()
    {
        SceneManager.LoadScene(sceneIndex);
    }






    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
