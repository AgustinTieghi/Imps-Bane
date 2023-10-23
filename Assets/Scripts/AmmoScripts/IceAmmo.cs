using PathCreation.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceAmmo : Ammo
{
    PathFollower enemyScript;
    private float initialSpeed;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            enemyScript = other.GetComponent<PathFollower>();
            initialSpeed = enemyScript.speed;
            enemyScript.speed /= 2;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        enemyScript.speed = initialSpeed;
    }
}
