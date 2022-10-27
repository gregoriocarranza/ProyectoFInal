using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Gun : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private GunData gunData;
    [SerializeField] private Transform muzzle;
    [SerializeField] private Camera mainCamera;

    private float timeSinceLastShot;
    public static Action shotFired;
    public static Action<int> shotEnemy;
    public static Action<int> IniMunition;
    public static Action<int> OnChangeMunition;
    public static Action<Vector3, Quaternion> gunHit;

    private bool pausa;

    AudioSource shootSound;
    AudioSource reloadSound;

    private void OnEnable()
    {

        PlayerShoot.shootInput += Shoot;
        PlayerShoot.shootInput += OnTriggerPressed;
        PlayerShoot.reloadInput += StartReload;
        PlayerShoot.triggerRelease += OnTriggerRelease;
        UIManager.Pause += OnPause;
        OnChangeMunition?.Invoke(gunData.currentAmmo);
    }

    private void OnDisable()
    {
        PlayerShoot.shootInput -= Shoot;
        PlayerShoot.shootInput -= OnTriggerPressed;
        PlayerShoot.reloadInput -= StartReload;
        PlayerShoot.triggerRelease -= OnTriggerRelease;
        UIManager.Pause -= OnPause;
    }

    void Start()
    {
        AudioSource[] audios = GetComponents<AudioSource>();
        shootSound = audios[0];
        reloadSound = audios[1];
    }

    // Update is called once per frame
    void Update()
    {
        Color color = Color.blue;
        timeSinceLastShot += Time.deltaTime;

        Debug.DrawRay(muzzle.position, muzzle.forward * 10, color);
    }

    public void StartReload()
    {
        if (!gunData.reloading)
        {
            StartCoroutine(Reload());
        }
        else
        {
            Invoke("Desencasquillar", 5f);
        }
    }
    private void Desencasquillar()
    {
        gunData.reloading = false;
    }
    private IEnumerator Reload()
    {
        reloadSound.PlayOneShot(reloadSound.clip, 0.7F); // Play ReloadSound

        gunData.reloading = true;

        yield return new WaitForSeconds(gunData.reloadTime);

        gunData.currentAmmo = gunData.magSize;

        OnChangeMunition?.Invoke(gunData.currentAmmo);
        gunData.reloading = false;
    }

    private void OnTriggerPressed()
    {
        gunData.triggerPressed = true;
    }

    private void OnTriggerRelease()
    {
        gunData.triggerPressed = false;
    }

    //private bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);

    private bool CanShoot()
    {
        // gunData.weaponType == 0 devuelve true si el arma es automática
        if (gunData.weaponType == 0) return (!gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f));
        else return (!gunData.reloading && !gunData.triggerPressed);
    }
    public void OnPause(bool pauses)
    {
        pausa = pauses;
    }
    private void Shoot()
    {
        //shootSound.Play();


        if (gunData.currentAmmo > 0 && !pausa)
        {
            Debug.Log("Disparo de "+gameObject.name);
            if (CanShoot())
            {
                shotFired?.Invoke();
                // Play gun shooting sound
                shootSound.PlayOneShot(shootSound.clip, 0.7F);
                //AudioClip.PlayOneShot(shootSound, 0.7f);
                
                // if (Physics.Raycast(muzzle.position, muzzle.forward, out RaycastHit hitInfo, gunData.maxDistance))
                if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out RaycastHit hitInfo, gunData.maxDistance))
                {
                    // Debug.Log("Raycast Hit");

                    // Paso Vector3 Position y dirección normal del hit para hacer un vfx de impacto
                    gunHit?.Invoke(hitInfo.point, Quaternion.LookRotation(hitInfo.normal));

                    if (hitInfo.transform.CompareTag("Enemy"))
                    {

                        // Debug.Log("Hit Enemy: " + hitInfo.transform.tag);
                        // Debug.Log("Hit Enemy: " + hitInfo.transform.name);
                        // shotEnemy?.Invoke(gunData.damage);
                        hitInfo.collider.gameObject.GetComponent<EnemyBehaviour>().OnTakeDamage(gunData.damage);
                    }

                    if (hitInfo.transform.CompareTag("WoodenCrate"))
                    {
                        // Debug.Log("Hit WoodenCrate: " + hitInfo.transform.tag);
                        hitInfo.collider.gameObject.GetComponent<WoodenCrateBehaviour>().TakeDamage(gunData.damage);
                    }

                    if (hitInfo.transform.CompareTag("GlassBottle"))
                    {
                        // Debug.Log("Hit GlassBottle: " + hitInfo.transform.tag);
                        hitInfo.collider.gameObject.GetComponent<GlassBottleBehaviour>().TakeDamage(gunData.damage);
                    }
                }

                gunData.currentAmmo--;
                timeSinceLastShot = 0;


                OnChangeMunition?.Invoke(gunData.currentAmmo);
            }
        }
    }

}
