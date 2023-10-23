using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ExplodingAmmo : Ammo
{
    public ParticleSystem particles;
    public float radius;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            PlayParticles();

            Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, radius);
            foreach (Collider hitCollider in hitColliders)
            {
                DoDamage(hitCollider);         
            }         
            Destroy(this.gameObject);
        }
    }

    private void PlayParticles()
    {
        ParticleSystem newParticles = Instantiate(particles, this.transform.position, Quaternion.identity);
        newParticles.Play();
        Destroy(newParticles.gameObject, newParticles.main.duration);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, radius);
    }
}
