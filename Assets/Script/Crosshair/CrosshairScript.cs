using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairScript : MonoBehaviour
{
    [SerializeField] private RectTransform crosshair;

    public GameObject player; // We will reference a boolean from the player's "PlayerMove" script 

    public float aimSize = 25f;
    public float idleSize = 50f;
    public float walkSize = 75f;
    public float runJumpSize = 125f;
    public float currentSize = 50f; // We set the initial currentSize float to "50" in order to prevent visible scaling at game start
    public float speed = 10f; // Crosshair scaling speed

    private void Update() // Called once per frame
    {
        if (Aiming) // If "Aiming" boolean is set to true
        {
            currentSize = Mathf.Lerp(currentSize, aimSize, Time.deltaTime * speed); // Lerp the currentSize float to aimSize float
        }
        else if (Walking) // If "Walking" boolean is set to true
        {
            currentSize = Mathf.Lerp(currentSize, walkSize, Time.deltaTime * speed); // Lerp the currentSize float to walkSize float
        }
        else if (Running || Jumping) // If "Running" or "Jumping" boolean is set to true
        {
            currentSize = Mathf.Lerp(currentSize, runJumpSize, Time.deltaTime * speed); // Lerp the currentSize float to runJumpSize float
        }
        else // if the player is idle
        {
            currentSize = Mathf.Lerp(currentSize, idleSize, Time.deltaTime * speed); // Lerp the currentSize float to idleSize float
        }

        crosshair.sizeDelta = new Vector2(currentSize, currentSize); // Resize the crosshair X and Y values to the currentSize float 
    }

    public bool Aiming // Define the "Aiming" boolean
    {
        get // Use the get method to return a value for the "Aiming" boolean
        {
            if (Input.GetMouseButton(1)) // If the right mouse button is pressed
            {
                if (!Walking && !Running && !Jumping) // If the "Walking", "Running" and "Jumping" booleans equal false
                {
                    return true; // Return a true value for the aiming boolean
                }
                else // If the "Walking", "Running" and/or "Jumping" booleans equal true
                {
                    return false; // Return a false value for the aiming boolean
                }
            }
            else // If the right mouse button is not pressed
            {
                return false; // Return a false value for the aiming boolean
            }
        }
    }

    bool Walking // Define the "Walking" boolean
    {
        get // Use the get method to return a value for the "Walking" boolean
        {
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) // If Unity's input buttons for "Horizontal" and/or "Vertical" are being pressed
            {
                if (Input.GetKey(KeyCode.LeftShift) == false && !Jumping) // If the left shift button is not being pressed and the "Jumping" boolean is equal to false
                {
                    return true; // Return a true value for the "Walking" boolean
                }
                else // If the left shift button is being pressed and/or the "Jumping" boolean is equal to true
                {
                    return false; // Return a false value for the "Walking" boolean
                }
            }
            else // If Unity's input buttons for "Horizontal" and/or "Vertical" are not being pressed
            {
                return false; // Return a false value for the "Walking" boolean
            }
        }
    }

    bool Running // Define the "Running" boolean
    {
        get // Use the get method to return a value for the "Running" boolean
        {
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) // If Unity's input buttons for "Horizontal" and/or "Vertical" are being pressed
            {
                if (Input.GetKey(KeyCode.LeftShift) == true && !Jumping) // If the left shift button is being pressed and the "Jumping" boolean is equal to false
                {
                    return true; // Return a true value for the "Running" boolean
                }
                else // If the left shift button is not being pressed and/or the "Jumping" boolean is equal to true
                {
                    return false; // Return a false value for the "Running" boolean
                }
            }
            else // If Unity's input buttons for "Horizontal" and/or "Vertical" are not being pressed
            {
                return false; // Return a false value for the "Running" boolean
            }
        }
    }

    bool Jumping // Define the "Jumping" boolean
    {
        get // Use the get method to return a value for the "Jumping" boolean
        {
            if (player.GetComponent<PlayerMove>().grounded == false) // If the "grounded" boolean of the "PlayerMove" script equals false
            {
                return true; // Return a true value for the "Jumping" boolean
            }
            else // If the "grounded" boolean of the "PlayerMove" script equals true
            {
                return false; // Return a false value for the "Jumping" boolean
            }
        }
    }
}
