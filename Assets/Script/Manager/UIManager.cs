using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance { get => instance; }
    private PlayerData playerdata;

    [Header("UI Vida")]
    [SerializeField] private Slider HealthBarr;
    [SerializeField] private Text HealtCount;

    [Header("UI Armas")]
    [SerializeField] private Text MunitionCount;


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

        Gun.IniMunition += initMunition;
        Gun.OnChangeMunition += ajustMunition;
    }

    // LifesCount-------------------------------------------
    public void initHealthBarr(int init)
    {
        Debug.Log(init);
        instance.HealthBarr.minValue = 0;
        instance.HealthBarr.maxValue = init;
        instance.HealtCount.text = init.ToString();
    }
    public static void ajustHealthBarr(int num)
    {

        instance.HealthBarr.value = num;
        instance.HealtCount.text = num.ToString();
    }
    // MunitionCount-------------------------------------------

    public void initMunition(int init)
    {
        instance.MunitionCount.text = init.ToString();
    }

    public static void ajustMunition(int num)
    {
        instance.MunitionCount.text = num.ToString();
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


        Gun.IniMunition -= initHealthBarr;
        Gun.OnChangeMunition -= ajustHealthBarr;
    }
}
