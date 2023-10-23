using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonRot : MonoBehaviour
{
    [SerializeField] private List<Collider> enemyList = new List<Collider>();
    [SerializeField] private int index = 0;
    private bool inRange;
    public Vector3 direction;
    public bool readyToFire;
   
    void Update()
    {
        if (enemyList.Count > 0 && inRange)
        {         
            Aim(enemyList[index]);
            readyToFire = true;
            if (enemyList[index] == null)
            {
                enemyList.Remove(enemyList[index]);
            }
        }
        else
        {
            readyToFire = false;
        }
    }

    private void Aim(Collider other)
    {
        if (other != null)
        {
            direction = other.transform.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 180).eulerAngles;
            Quaternion targetRot = Quaternion.Euler(rotation.x, rotation.y, 0);
            transform.rotation = targetRot;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            enemyList.Add(other);

            if (!inRange)
            {
                inRange = true;
                index = 0;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            if (enemyList.Contains(other))
            {
                enemyList.Remove(other);

                // Check if the index is still within the valid range.
                if (index >= enemyList.Count)
                {
                    inRange = false;
                }
                else if (other != null && other.gameObject == enemyList[index].gameObject)
                {
                    // Only increment the index if the removed enemy was the current target.
                    index++;
                }
            }
        }
    }
}