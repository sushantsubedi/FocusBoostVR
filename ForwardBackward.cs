using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine;

public class ForwardBackward : MonoBehaviour
{
    public float moveSpeed = 2.0f; // Speed at which the object will move with
    public float moveDistance = 2.0f; // Distance the object will move in the z-axis

    private Vector3 initialPosition; // Store the initial position of the object


    // Start is called before the first frame update
    void Start()
    {
        // Store the initial position of the object
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the new position based on the sine wave to
        float offset = Mathf.Sin(Time.time * moveSpeed) * moveDistance;
        transform.position = initialPosition + new Vector3(0, 0, offset); //  the offset apolies to the z-axis  only
    }
}
