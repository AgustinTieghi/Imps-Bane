using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public int dmg;  
    public void DoDamage(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            other.gameObject.GetComponent<IDamagable>().GetDamage(dmg);
        }
    }
}
