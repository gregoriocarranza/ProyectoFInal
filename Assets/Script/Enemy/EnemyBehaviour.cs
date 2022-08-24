using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public Transform target;

    [SerializeField]
    [Range(1f, 5f)]
    private float Speed = 1f;

    enum EnemyType
    {
        Empty,
        Looking,
        Follow
    }

    [SerializeField]
    EnemyType enemigo;
    [SerializeField]
    private Animator EnemyAnimator;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        switch (enemigo)
        {
            case EnemyType.Empty:
                break;
            case EnemyType.Follow:
                SeguirJugador();
                break;
            case EnemyType.Looking:
                LookPlayer();
                break;
            default:
                break;
        }
    }

    void LookPlayer()
    {
        transform.LookAt(target);
        
    }

    void SeguirJugador()
    {
        LookPlayer();
        Vector3 direction = (target.position - transform.position);

        if (direction.magnitude > 2f)
        {
            transform.position += direction.normalized * Speed * Time.deltaTime;
            EnemyAnimator.SetTrigger("adelante");
        }
        else
        {
            EnemyAnimator.SetTrigger("Reposo");
        }
    }
}
