using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerEnemy : EnemyBehaviour
{
    [SerializeField] public Transform target;
    [SerializeField] public Animator EnemyAnimator;
    [SerializeField] public Transform FirstPoint;
    [SerializeField] protected GameObject ToTansform;

    private void FixedUpdate()
    {

        EnemyRaycast(enemydata.Is_follower, FirstPoint, enemydata.rayDistance, target, EnemyAnimator, ToTansform);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 direction = FirstPoint.TransformDirection(Vector3.forward) * enemydata.rayDistance;
        Gizmos.DrawRay(FirstPoint.transform.position, direction);
    }
}
