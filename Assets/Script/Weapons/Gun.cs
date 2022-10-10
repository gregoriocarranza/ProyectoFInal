using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private GunData gunData;
    [SerializeField] private Transform muzzle;

    private float timeSinceLastShot; 
    public static Action<int> shotEnemy;
    public static Action<Vector3, Quaternion> gunHit;

    AudioSource audioData;
    
    // Start is called before the first frame update
    void Start()
    {
        audioData = GetComponent<AudioSource>();

        PlayerShoot.shootInput += Shoot;
        PlayerShoot.shootInput += OnTriggerPressed;
        PlayerShoot.reloadInput += StartReload;
        PlayerShoot.triggerRelease += OnTriggerRelease;
    }

    // Update is called once per frame
    void Update()
    {
        Color color = Color.blue;
        timeSinceLastShot += Time.deltaTime;

        Debug.DrawRay(muzzle.position, muzzle.forward, color);
    }

    public void StartReload() {
        if (!gunData.reloading) {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload() {
        gunData.reloading = true;

        yield return new WaitForSeconds(gunData.reloadTime);

        gunData.currentAmmo = gunData.magSize;

        gunData.reloading = false;
    }

    private void OnTriggerPressed(){
        gunData.triggerPressed = true;
    }

    private void OnTriggerRelease(){
        gunData.triggerPressed = false;
    }

    //private bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);

    private bool CanShoot(){
        // gunData.weaponType == 0 devuelve true si el arma es automática
        if (gunData.weaponType == 0) return (!gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f));
        else return (!gunData.reloading && !gunData.triggerPressed);
    } 

    private void Shoot()
    {    
        //audioData.Play();
        
        Debug.Log("Entro a Shoot de Gun");
        if (gunData.currentAmmo > 0)
        {
            Debug.Log("currentAmmo mayor a 0");
            if (CanShoot())
            {
                // Play gun shooting sound
                audioData.PlayOneShot(audioData.clip, 0.7F);
                Debug.Log("Can shoot");

                if (Physics.Raycast(muzzle.position, muzzle.forward, out RaycastHit hitInfo, gunData.maxDistance))
                {
                    Debug.Log("Raycast Hit");

                    // Paso Vector3 Position y dirección normal del hit para hacer un vfx de impacto
                    gunHit?.Invoke(hitInfo.point, Quaternion.LookRotation(hitInfo.normal));

                    if (hitInfo.transform.CompareTag("Enemy")){
                        Debug.Log("Hit Enemy: " + hitInfo.transform.tag);
                        shotEnemy?.Invoke(gunData.damage);
                    }

                    if (hitInfo.transform.CompareTag("WoodenCrate")){
                        Debug.Log("Hit WoodenCrate: " + hitInfo.transform.tag);
                        //hitInfo.SendMessage("TakeDamage", gunData.damage);
                        //hitInfo.transform.TakeDamage(gunData.damage);
                        hitInfo.collider.gameObject.GetComponent<WoodenCrateBehaviour>().TakeDamage(gunData.damage);
                    }

                    //IDamageable damageable = hitInfo.transform.GetComponent<IDamageable>();
                    //damageable?.TakeDamage(gunData.damage);
                }

                gunData.currentAmmo--;
                timeSinceLastShot = 0;
            }
        }
    }
}
