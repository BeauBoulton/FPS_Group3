using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Name: Beau Boulton
 * Last updated: 5/1/25
 * Description: Makes the camera follow the player's position
 */
public class MoveCamera : MonoBehaviour
{
    public Transform cameraPosition; 

    // Update is called once per frame
    void Update()
    {
        transform.position = cameraPosition.position;
    }
}
