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

    AudioSource audioData;
    
    // Start is called before the first frame update
    void Start()
    {
        audioData = GetComponent<AudioSource>();

        PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += StartReload;
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

    private bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);

    private void Shoot()
    {    
        //audioData.Play();
        
        Debug.Log("Entro a Shoot de Gun");
        if (gunData.currentAmmo > 0)
        {
            if (CanShoot())
            {
                // Play gun shooting sound
                audioData.PlayOneShot(audioData.clip, 0.7F);

                if (Physics.Raycast(muzzle.position, muzzle.forward, out RaycastHit hitInfo, gunData.maxDistance))
                {
                    Debug.Log("Raycast Hit");

                    if (hitInfo.transform.CompareTag("Enemy")){
                        Debug.Log("Hit Enemy: " + hitInfo.transform.tag);
                        shotEnemy?.Invoke(gunData.damage);
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
