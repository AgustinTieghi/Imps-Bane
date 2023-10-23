using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    public Turret turret;
    public Ammo ammo;
    public GameObject selected;
    public int dmg;
    public float fireRate;
    public int cost;
    public int sellValue;
    public float range;
    public bool upgradable;
    public Turret.Type type;
    [SerializeField] private SphereCollider rangeCollider;

    private void Start()
    {
        type = turret.type;
        dmg = ammo.dmg;
        fireRate = turret.fireRate;
        cost = turret.cost;
        sellValue = cost/2;
        range = turret.range;
        upgradable = turret.upgradable;
        rangeCollider.radius = range;
    }
}
