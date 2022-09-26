using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTarget : MonoBehaviour, IDamageable {

    public float health = 100f;

    public void TakeDamage(float damage)
    {
        Debug.Log("Entro a TakeDamage en PlayerTarget");
        health -= damage;
        if (health <= 0) Destroy(gameObject);
    }
}