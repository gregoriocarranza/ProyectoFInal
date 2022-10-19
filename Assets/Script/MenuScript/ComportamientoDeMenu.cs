using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ComportamientoDeMenu : MonoBehaviour
{
    private static ComportamientoDeMenu instance;
    [SerializeField] private GameObject[] Menues;
    private int i = 1;
    public bool IniciarNivel;
    public int IndiceNivel;

    void Update()
    {
        if (IniciarNivel)
        {
            CambiarNivel(IndiceNivel);
        }
    }
    public void exit()
    {
        Application.Quit();
    }
    public void CambiarNivel(int Indice)
    {
        SceneManager.LoadScene(Indice);
    }
    public void PasarPantallaMenu()
    {
        i += 1;
        Menues[i - 1].SetActive(false);
        Menues[i].SetActive(true);
    }
    public void VolverPantallaMenu()
    {
        i -= 1;
        Menues[i + 1].SetActive(false);
        Menues[i].SetActive(true);
    }


}
