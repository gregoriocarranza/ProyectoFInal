using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Munition Data", menuName = "Create Munition Data")]
public class MunnitionData : ScriptableObject
{
    [Header("Datos de municion")]
    [SerializeField][Range(0f, 50f)] public float Speed = 1f;
    [SerializeField][Range(1f, 10f)] public float DestroyTime = 1f;

    [Header("Efectos de la municion")]

    [SerializeField] public GameObject EfectoDeDestruccion;
}
