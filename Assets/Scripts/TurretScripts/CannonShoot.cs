using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class CannonShoot : MonoBehaviour
{
    [SerializeField] private CannonRot cannonRot;
    [SerializeField] private GameObject ammo;
    [SerializeField] private float forceAmount;
    [SerializeField] private AudioSource shootSound;
    public Vector3 direction;    
    public TurretScript turretScript;
    private float timer;

    private void Update()
    {
        if(cannonRot.readyToFire)
        {
            timer += Time.deltaTime;
            if (timer >= turretScript.fireRate)
            {
                Shoot();
                timer = 0;
            }
        }    
    }
    public void Shoot()
    {    
        shootSound.Play();
        GameObject spawnedAmmo = Instantiate(ammo, this.transform.position, Quaternion.identity);
        spawnedAmmo.GetComponent<Rigidbody>().AddForce(cannonRot.direction * forceAmount, ForceMode.Impulse);
    }
}
