using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HealthBarrManager : MonoBehaviour
{
    private static HealthBarrManager instance;
    public static HealthBarrManager Instance { get => instance; }
    private PlayerData playerdata;

    [SerializeField] private Slider HealthBarr;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        PlayerData.OnDead += GameOver;
        PlayerData.InitHp += initHealthBarr;
        PlayerData.OnChangeHP += ajustHealthBarr;
    }
    void Start()
    {
        // playerdata = GetComponent<PlayerData>();
        // initHealthBarr(playerdata.checkLifes());

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void initHealthBarr(int init)
    {
        Debug.Log(init);
        instance.HealthBarr.minValue = 0;
        instance.HealthBarr.maxValue = init;
    }
    public static void ajustHealthBarr(int num)
    {

        instance.HealthBarr.value = num;
    }
    private void GameOver()
    {
        Debug.Log("Holaaa");
    }
    private void OnDisable()
    {
        PlayerData.OnDead -= GameOver;
        PlayerData.InitHp -= initHealthBarr;
        PlayerData.OnChangeHP -= ajustHealthBarr;
    }
}
