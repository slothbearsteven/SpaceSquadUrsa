using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatWidth;
    private float speed = 10;

    private void Start()
    {
        startPos = transform.position; // Establish the default starting position 
        repeatWidth = 50; // Set repeat width to half of the background
    }

    private void Update()
    {
        MoveBackground();
    }

    void MoveBackground()
    { //Moves the background as game is active. Once the player dies, the background should stop.
        if (MainManager.gameActive)
        {
            transform.Translate(Vector3.back * Time.deltaTime * speed);
            // If background moves left by its repeat width, move it back to start position
            if (transform.position.z < startPos.z - repeatWidth)
            {
                transform.position = startPos;
            }
        }
    }

}


