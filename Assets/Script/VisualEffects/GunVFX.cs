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
        Gun.shotFired += OnRoundShot;
        Gun.gunHit += OnGunHit;
    }


    private void OnRoundShot()
    {
        muzzleFlashEffectAsset.Play();
    }

    private void OnGunHit(Vector3 position, Quaternion rotation)
    {
        //bulletImpactEffectAsset.transform.position = position;
        //bulletImpactEffectAsset.Play();
        GameObject _bulletImpactEffectAsset = Instantiate(bulletImpactEffectAsset, position, rotation);
        Destroy(_bulletImpactEffectAsset, 1f);
    }
}
