using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public static Action shootInput;
    public static Action reloadInput;
    public static Action triggerRelease;

    [SerializeField] private KeyCode reloadKey;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)){
            shootInput?.Invoke();
        }

        if (Input.GetMouseButtonUp(0)){
            triggerRelease?.Invoke();
        }

        if (Input.GetKeyDown(reloadKey)){
            reloadInput?.Invoke();
        }
    }
}
