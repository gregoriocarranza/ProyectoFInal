using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

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

    [Header("Pantalla de Pausa")]
    public GameObject MenuPausa;
    public bool pausa = false;
    public static Action<bool> Pause;

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

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {

        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pausa = !pausa;
                MenuPausa.SetActive(pausa);
                if (!pausa) Cursor.lockState = CursorLockMode.Locked; else Cursor.lockState = CursorLockMode.None;

                Pause?.Invoke(pausa);
            }
        }
    }
    // Botones menu-------------------------------------------

    public void CambiarNivel(int Indice)
    {
        SceneManager.LoadScene(Indice);
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
