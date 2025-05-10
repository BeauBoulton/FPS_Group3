using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Name: Beau Boulton
 * Last Updated: 5/9/25
 * Description: Prevents object from being destroyed on scene load
 */

public class DontDestroyScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
