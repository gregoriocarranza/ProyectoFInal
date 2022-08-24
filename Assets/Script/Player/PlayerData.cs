using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField]
    private int lifes = 3;

    [SerializeField]
    [Range(1f, 3f)]
    private int dificult = 1;

    void Start()
    {
        lifes = 3;
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
            Destroy (gameObject);
        }
    }

    public void heal(int a)
    {
        lifes = lifes + (a * dificult);
    }

    public void kill(int a)
    {
        lifes = lifes - (a * dificult);
    }
}
