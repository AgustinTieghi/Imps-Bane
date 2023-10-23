using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseScript : MonoBehaviour
{
    public ManagementScript manager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {     
            manager.hp -= other.gameObject.GetComponent<EnemyScript>().damage;
            Destroy(other.gameObject);
        }
    }
}
