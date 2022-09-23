using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] protected EnemyData enemydata;
    public LayerMask Munition;

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



    protected void EnemyRaycast(bool Is_follower, Transform FirstPoint, float rayDistance, Transform target, Animator EnemyAnimator, GameObject ToTansform)
    {
        RaycastHit hit;
        if (Physics.Raycast(FirstPoint.position, FirstPoint.TransformDirection(Vector3.forward), out hit, rayDistance, ~Munition, QueryTriggerInteraction.Ignore))
        {
            // Debug.Log(hit.transform.tag);
            if (hit.transform.CompareTag("Player") && Is_follower == false)
            {
                LookPlayer(target, ToTansform);
                if (canShoot)
                {
                    Instantiate(enemydata.bullet, FirstPoint.transform.position, FirstPoint.transform.rotation);
                    canShoot = false;
                    Invoke("delayShoot", enemydata.DelayShoot);
                }

            }
            else if (hit.transform.CompareTag("Player") && Is_follower)
            {
                SeguirJugador(EnemyAnimator, target, ToTansform);
            }
        }
    }
    void delayShoot()
    {
        canShoot = true;
    }
}
