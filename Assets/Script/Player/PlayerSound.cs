using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    AudioSource stepSound;
    AudioSource healSound;

    // Start is called before the first frame update
    void Start()
    {
        Botiquin.playerHealed += OnPlayerHeal;
        
        AudioSource[] audios = GetComponents<AudioSource>();
        stepSound = audios[0];
        healSound = audios[1];
        //damageSound = audios[2];
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            stepSound.enabled = true;
        }

        else
        {
            stepSound.enabled = false;
        }
    }

    private void OnDisable()
    {
        //UIManager.Pause -= OnPause;
    }

    private void OnPlayerHeal()
    {
        healSound.Play();
    }
}
