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

    [Header("UI Monedas")]
    [SerializeField] private Text CoinCount;
    private int PlayerCoins = 0;
    public int MaxCoins = 5;


    [Header("Sonidos de Ganar o Perder")]
    public AudioClip WinGame;
    public AudioClip LoseGame;
    AudioSource Audio;

    [Header("Game Over Screen")]
    public GameObject GameOverScreen;
    private bool GameOverFlag = false;

    [Header("Win Screen")]
    public GameObject WinScreen;
    private bool GameFlag = false;

    [Header("Pantalla de Pausa")]
    public GameObject MenuPausa;
    public bool pausa = false;
    public static Action<bool> Pause;

    private void Awake()
    {
        Audio = GetComponent<AudioSource>();
        WinScreen.SetActive(false);
        GameOverScreen.SetActive(false);


        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        initCoin(0);

        PlayerData.OnDead += GameOver;

        PlayerData.InitHp += initHealthBarr;
        PlayerData.OnChangeHP += ajustHealthBarr;

        Gun.IniMunition += initMunition;
        Gun.OnChangeMunition += ajustMunition;

        CoinBehaviour.CoinGrabed += CoinGrabed;

        Cursor.lockState = CursorLockMode.Locked;

        Pause?.Invoke(false);
    }

    private void Update()
    {

        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !GameFlag && !GameOverFlag)
            {
                pausa = !pausa;
                MenuPausa.SetActive(pausa);
                if (!pausa) Cursor.lockState = CursorLockMode.Locked; else Cursor.lockState = CursorLockMode.None;

                Pause?.Invoke(pausa);
            }
        }

        if (instance.HealthBarr.value <= 0 && !GameOverFlag)
        {
            GameOver();
        }
        if (instance.PlayerCoins >= instance.MaxCoins && !GameFlag)
        {
            GameWin();
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

    // CoinCount-------------------------------------------

    public void initCoin(int init)
    {
        instance.CoinCount.text = init.ToString() + "/" + instance.MaxCoins.ToString();
        instance.PlayerCoins = 0;
    }

    public static void CoinGrabed(int coin)
    {
        instance.PlayerCoins += coin;

        instance.CoinCount.text = instance.PlayerCoins.ToString() + "/" + instance.MaxCoins.ToString();
    }



    private void GameWin()
    {
        GameFlag = true;
        Debug.Log("GameWin");
        Audio.PlayOneShot(WinGame, 0.7F);
        WinScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Pause?.Invoke(true);
    }
    private void GameOver()
    {
        GameOverFlag = true;
        Audio.PlayOneShot(LoseGame, 0.7F);
        GameOverScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Pause?.Invoke(true);
    }
    private void OnDisable()
    {
        PlayerData.OnDead -= GameOver;
        PlayerData.InitHp -= initHealthBarr;
        PlayerData.OnChangeHP -= ajustHealthBarr;


        Gun.IniMunition -= initHealthBarr;
        Gun.OnChangeMunition -= ajustHealthBarr;

        CoinBehaviour.CoinGrabed -= CoinGrabed;
    }
}
