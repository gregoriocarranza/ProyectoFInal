using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class GunVFX : MonoBehaviour
{
    [SerializeField] VisualEffect muzzleFlashEffectAsset;
    [SerializeField] GameObject bulletImpactEffectAsset;
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerShoot.shootInput += OnRoundShot;
        Gun.gunHit += OnGunHit;
    }

   
    private void OnRoundShot(){
        muzzleFlashEffectAsset.Play();
    }

    private void OnGunHit(Vector3 position, Quaternion rotation){
        //bulletImpactEffectAsset.transform.position = position;
        //bulletImpactEffectAsset.Play();
        Instantiate(bulletImpactEffectAsset, position, rotation);
    }
}
