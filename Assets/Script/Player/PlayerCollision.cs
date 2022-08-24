using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private PlayerData playerdata;

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
                playerdata.kill(1);
                break;
            default:
                Debug.Log(other.gameObject.name);
                break;
        }
    }
}
