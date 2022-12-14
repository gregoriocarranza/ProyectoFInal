using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] protected EnemyData enemydata;
    public int EnemyHealth;
    private LayerMask Munition;
    AudioSource audioPlayer;

    private bool pause;

    void Start()
    {
        EnemyHealth = enemydata.health;
        audioPlayer = GetComponent<AudioSource>();
        Gun.shotEnemy += OnTakeDamage;
    }

    void Update()
    {
        DeathCheck();
    }

    protected void LookPlayer(Transform target, GameObject ToTansform)
    {
        ToTansform.transform.LookAt(target);

    }

    protected void SeguirJugador(Animator EnemyAnimator, Transform target, GameObject ToTansform)
    {
        LookPlayer(target, ToTansform);
        Vector3 direction = (target.position - transform.position);

        if (direction.magnitude > 2f)
        {
            ToTansform.transform.position += direction.normalized * enemydata.Speed * Time.deltaTime;
            EnemyAnimator.SetTrigger("adelante");
        }
        else
        {
            EnemyAnimator.SetTrigger("Reposo");
        }
    }

    // --------------------------------------------------------raycast



    private bool canShoot = true;

    [SerializeField] private Transform[] Outputs;
    private int i = 0;


    protected void EnemyRaycast(bool Is_follower, Transform FirstPoint, float rayDistance, Transform target, Animator EnemyAnimator, GameObject ToTansform)
    {
        RaycastHit hit;
        if (Physics.Raycast(FirstPoint.position, FirstPoint.TransformDirection(Vector3.forward), out hit, rayDistance, ~Munition, QueryTriggerInteraction.Ignore) && !pause)
        {
            // Debug.Log(hit.transform.tag);
            if (hit.transform.CompareTag("Player") && Is_follower == false)
            {
                LookPlayer(target, ToTansform);

                if (canShoot)
                {
                    if (i >= Outputs.Length)
                    {
                        i = 0;
                    }
                    // Debug.Log(i);
                    // Debug.Log(Outputs.Length);

                    Instantiate(enemydata.bullet, Outputs[i].transform.position, Outputs[i].transform.rotation);
                    i++;
                    canShoot = false;
                    Invoke("chargeShoot", enemydata.DelayShoot - 1f);
                    Invoke("delayShoot", enemydata.DelayShoot);
                }

            }
            else if (hit.transform.CompareTag("Player") && Is_follower)
            {
                SeguirJugador(EnemyAnimator, target, ToTansform);
            }

        }
    }
    void chargeShoot()
    {
        Debug.Log("Cargando");
        audioPlayer.PlayOneShot(enemydata.ChargeShoot);

    }
    void delayShoot()
    {
        audioPlayer.PlayOneShot(enemydata.Shoot, 0.7f);
        canShoot = true;


    }

    public void OnTakeDamage(int damage)
    {
        EnemyHealth -= damage;
    }

    private void DeathCheck()
    {
        if (EnemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        UIManager.Pause += OnPause;
    }

    private void OnDisable()
    {
        UIManager.Pause -= OnPause;
    }

    public void OnPause(bool pauses)
    {
        pause = pauses;
    }
}
