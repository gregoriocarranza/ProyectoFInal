using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Botiquin : MonoBehaviour
{
    public int BotiquinHealed = 1;
    public static Action playerHealed;


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerHealed?.Invoke();
            other.gameObject.GetComponent<PlayerData>().heal(BotiquinHealed);
            Destroy(gameObject);
        }
    }
}
