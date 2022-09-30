using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MuzzleFlash : MonoBehaviour
{
    [SerializeField] VisualEffect visualEffectAsset;
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerShoot.shootInput += OnRoundShot;
    }

   
    private void OnRoundShot(){
        visualEffectAsset.Play();
    }
}
