using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    AudioSource ambientSound;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource[] audios = GetComponents<AudioSource>();
        ambientSound = audios[0];
    }

    // Update is called once per frame
    void Update()
    {
        //
    }
}
