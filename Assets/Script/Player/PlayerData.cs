using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerData : MonoBehaviour
{
    [SerializeField]
    private int lifes = 3;

    [SerializeField]
    [Range(1f, 3f)]
    private int dificult = 1;

    public static event Action OnDead;
    public static event Action<int> OnChangeHP;
    public static event Action<int> InitHp;
    private void Awake()
    {
        PlayerCollision.Damage += kill;
        PlayerCollision.Heal += heal;
    }
    void Start()
    {
        InitHp?.Invoke(lifes);
    }

    // Update is called once per frame
    void Update()
    {
        checkLifes();
    }

    void checkLifes()
    {
        if (lifes == 0)
        {
            Debug.Log("Player Has been kiled");
            Destroy(gameObject);
        }
    }

    public void heal(int a)
    {
        lifes = lifes + (a * dificult);
        OnChangeHP?.Invoke(lifes);
    }

    public void kill(int a)
    {
        lifes = lifes - (a * dificult);
        OnChangeHP?.Invoke(lifes);
    }
}
