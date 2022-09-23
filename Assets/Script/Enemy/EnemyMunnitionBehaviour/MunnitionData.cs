using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Munition Data", menuName = "Create Munition Data")]
public class MunnitionData : ScriptableObject
{
    [Header("Munition Data center")]
    [SerializeField] public Vector3 direction = new Vector3(0f, 1f, 0f);

    [SerializeField][Range(0f, 5f)] public float Speed = 1f;
    [SerializeField][Range(1f, 10f)] public float DestroyTime = 1f;
}
