using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomScript : MonoBehaviour
{
    AudioSource FinAudio;
    void Start()
    {
        FinAudio = GetComponent<AudioSource>();

        FinAudio.PlayOneShot(FinAudio.clip, 0.7F);
        Invoke("DESTROY", 4f);

    }


    void Update()
    {

    }
    void DESTROY()
    {
        Destroy(gameObject);
    }
}
