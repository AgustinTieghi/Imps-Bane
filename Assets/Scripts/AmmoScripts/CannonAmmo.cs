using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonAmmo : Ammo
{
    private void OnTriggerEnter(Collider other)
    {
        DoDamage(other);
        if(other.gameObject.layer == 8)
        {
            Destroy(this.gameObject);
        }
    }
}
