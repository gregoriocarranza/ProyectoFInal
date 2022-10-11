using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ComportamientoDeMenu : MonoBehaviour
{

    public bool IniciarNivel;
    public int IndiceNivel;

    void Update()
    {
        if (IniciarNivel)
        {
            CambiarNivel(IndiceNivel);

        }

    }

    public void CambiarNivel(int Indice)
    {
        SceneManager.LoadScene(Indice);
    }
}
