using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botiquin : MonoBehaviour
{
    public int BotiquinHealed = 1;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            other.gameObject.GetComponent<PlayerData>().heal(BotiquinHealed);
            Destroy(gameObject);
        }
    }
}
