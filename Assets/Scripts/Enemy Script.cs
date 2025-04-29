using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Nick Sumek
 * updated 4/24/25
 * Sets up basic enemy behavior
 */


public class EnemyScript : MonoBehaviour
{
    public Transform Player;
    int MoveSpeed = 2;
    int MaxDist = 10;
    int MinDist = 5;
    public int enemyDamage;



    void Start()
    {

    }

    void Update()
    {
        transform.LookAt(Player);

        if (Vector3.Distance(transform.position, Player.position) >= MinDist)
        {

            transform.position += transform.forward * MoveSpeed * Time.deltaTime;



            if (Vector3.Distance(transform.position, Player.position) <= MaxDist)
            {
                //Here Call any function U want Like Shoot at here or something
            }

        }
    }
}

