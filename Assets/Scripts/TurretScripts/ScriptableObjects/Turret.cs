using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Turret", menuName = "Turret")]
public class Turret : ScriptableObject
{ 
    public int dmg;
    public int cost;  
    public float range;
    public bool upgradable;
    public float fireRate;

    [HideInInspector]
    public GameObject selected;
    [HideInInspector]
    public int sellValue;
    [HideInInspector]
    public SphereCollider rangeCollider;
    public enum Type
    {
        Cannon,
        Exploding,
        Ice
    }
    public Type type;
}
