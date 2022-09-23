using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Create Enemy Data")]
public class EnemyData : ScriptableObject
{
    [Header("Enemy Data center")]
    [SerializeField][Range(1f, 5f)] public float Speed = 1f;
    [SerializeField][Range(0f, 1f)] public float DelayShoot = 1f;

    [SerializeField] public bool Is_follower;
    [SerializeField][Range(1f, 100f)] public float rayDistance = 40f;
    [SerializeField] public int life = 2;

    // public enum EnemyType
    // {
    //     Empty,
    //     Looking,
    //     Follow
    // }

    // [SerializeField] public EnemyType enemigo;
}
