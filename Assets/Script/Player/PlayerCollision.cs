using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerCollision : MonoBehaviour
{
    private PlayerData playerdata;

    public static event Action<int> Damage;
    public static event Action<int> Heal;

    void Start()
    {
        playerdata = GetComponent<PlayerData>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Powerup":
                Destroy(other.gameObject);
                playerdata.heal(2);
                break;
            case "Enemy":
                Destroy(other.gameObject);
                Damage?.Invoke(1);
                break;
            case "TankMunition":
                Damage?.Invoke(1);
                Destroy(other.gameObject);
                break;
            default:
                Debug.Log(other.gameObject.name);
                break;
        }
    }
}
