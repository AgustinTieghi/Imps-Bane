using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour, IDamagable
{
    [Header("Enemy Settings")]
    private int currentHP;
    public int damage;
    [SerializeField] private int initialHP;

    [Header("Enemy References")]
    [SerializeField] private Image healthbar;
    [SerializeField] private GameObject canvas;
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private AudioSource deathSound;

    private enum Type
    {
        Normal,
        Tank,
        Fast
    }
    [SerializeField] private Type type;


    private void Start()
    {
        damage = initialHP;
        currentHP = initialHP;
    }
    void Update()
    {       
        if (currentHP <= 0)
        {        
            Destroy(this.gameObject);
        }
        //Try not to check this on every frame
        if(currentHP < initialHP)
        {
            canvas.SetActive(true); 
            UpdateHealthbar(initialHP, currentHP);
        }
    }  
    
    private void ParticlesOnDeath()
    {
        ParticleSystem newParticles = Instantiate(particles, this.transform.position, particles.gameObject.transform.localRotation);
        newParticles.Play();
        Destroy(newParticles.gameObject, newParticles.main.duration);
    }
    public void UpdateHealthbar(float maxHealth, float currentHealth)
    {
        healthbar.fillAmount = currentHealth / maxHealth;
    }
    private void OnDestroy()
    {
        ParticlesOnDeath();
        switch (type)
        {
            case Type.Normal:
                ManagementScript.instance.money += 50;
                break;
            case Type.Tank:
                ManagementScript.instance.money += 100;
                break;
            case Type.Fast:
                ManagementScript.instance.money += 25;
                break;
            default:
                ManagementScript.instance.money += initialHP/2;
                break;
        }       
        EnemySpawner.instance.enemiesLeft -= 1;
    }

    public void GetDamage(int dmg)
    {
        currentHP -= dmg;
        deathSound.PlayOneShot(deathSound.clip);
    }
}
