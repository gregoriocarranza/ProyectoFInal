using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class GunData : using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "Weapons/Gun")]
public class GunData : ScriptableObject {
    
    [Header("Info")]
    public new string name;

    [Header("Shooting")]
    public int damage;
    public float maxDistance;
    public float fireRate;

    [Header("Ammo & Reload")]
    public int currentAmmo;
    public int magSize;
    public float reloadTime;

    [HideInInspector]
    public bool reloading;
}
