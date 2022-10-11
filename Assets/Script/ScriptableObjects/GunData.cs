using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class GunData : using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "Weapons/GunData")]
public class GunData : ScriptableObject
{

    public enum WeaponType
    {
        Automatic,
        SingleShot,
    };

    [Header("Info")]
    public new string name;
    [SerializeField] public WeaponType weaponType;

    [Header("Shooting")]
    public int damage;
    public float maxDistance;
    public float fireRate;

    [Header("Ammo & Reload")]
    public int currentAmmo;
    public int magSize;
    public float reloadTime;

    //[HideInInspector]
    public bool reloading = false;
    public bool triggerPressed;
}
