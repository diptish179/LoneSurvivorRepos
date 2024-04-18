using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 5f; // platform speed
    public float lowerLimit = -5f; 
    public float upperLimit = 5f; 

    void Update()
    {
     
        float newPositionY = Mathf.PingPong(Time.time * speed, upperLimit - lowerLimit) + lowerLimit;

        // Update the platform's position
        transform.position = new Vector3(transform.position.x, newPositionY, transform.position.z);
    }
}