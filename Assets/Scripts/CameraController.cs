using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
/*
 * Name: Beau Boulton
 * Last updated: 4/29/25
 * Description: Handles camera movement with player mouse inputs
 */

public class CameraController : MonoBehaviour
{
    // Mouse sensitivity
    public float sensX; 
    public float sensY;

    public Transform orientation;

    // Player rotation
    private float xRotation; 
    private float yRotation;

    // Toggle to hide/show cursor
    private bool hideCursor;

    // Start is called before the first frame update
    void Start()
    {
        // Cursor is locked and invisible by default
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        hideCursor = true;
    }

    // Update is called once per frame
    void Update()
    {
        CameraInput(); 

        HideCursor();
    }

    /// <summary>
    /// Controls camera movement with mouse inputs
    /// </summary>
    private void CameraInput()
    {
        // Mouse x and y values are set to mouse axis input * sensitivity * Time.deltaTime
        float mouseX = Input.GetAxisRaw("Mouse X") * sensX * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensY * Time.deltaTime;

        // Player rotation value is assigned based on mouse values 
        yRotation += mouseX;
        xRotation -= mouseY;

        // Clamps rotation so that player can't look up or down more than 90 degrees
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Uses rotation values to rotate camera on x and y axes and player on Y axis
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }


    /// <summary>
    /// Pressing escape locks and hides/unlocks and shows cursor
    /// </summary>
    private void HideCursor()
    {
        // If escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // If cursor is already hidden
            if (hideCursor == true)
            {
                // Unlocks and shows cursor
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                hideCursor = false;
            }

            // If cursor is visible
            if (hideCursor == false)
            {
                // Locks and hides cursor
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                hideCursor = true;
            }
        }
    }
}
