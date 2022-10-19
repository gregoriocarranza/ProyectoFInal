using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BoomScript : MonoBehaviour
{
    AudioSource FinAudio;

    void Start()
    {
        FinAudio = GetComponent<AudioSource>();

        FinAudio.PlayOneShot(FinAudio.clip, 0.7F);
        Destroy(gameObject, 4f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerData>().damage(5);
        }
    }


}
