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
        Destroy(gameObject, 4f);
    }


}
