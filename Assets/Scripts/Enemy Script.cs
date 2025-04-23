using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Nick Sumek
 * 4/22/25
 * Sets up basic values for the enemys
 */


public class EnemyScript : MonoBehaviour
{

    //declerations
    public int enemyHealth;
    public int enemyMovement;
    public bool goingLeft = true;
    public bool goingForward = true;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //sets up basic enemy movement

        //moves right
        if (goingLeft == true)
        {
            transform.position += Vector3.left * enemyMovement * Time.deltaTime;
        }

        //moves right
        if (goingLeft == false)
        {
            transform.position += Vector3.right * enemyMovement * Time.deltaTime;
        }

        //sets moving forward
        if (goingForward == true)
        {
            transform.position += Vector3.forward * enemyHealth * Time.deltaTime;

        }

        //moves backwards
        if (goingForward == false)
        {
            transform.position += Vector3.back * enemyHealth * Time.deltaTime;

        }


    }
}
