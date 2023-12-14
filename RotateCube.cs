// This line is equivalent to #include part in C++, it is importing necessary namespaces
// to gain access to their classes and functions, so as to allow us to interact with
// the UnityEngine's features and C# collections
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Define a new class "RotateCube" that inherits from MonoBehaviour
// MonoBehaviour is a base class every script derives from when you are using Unity.
public class RotateCube : MonoBehaviour
{
    // Define two float variables to handle rotation speed and the amount of rotation.
    float rotationSpeed;
    float rotation;

    // Start is called before the first frame update.
    // The Start method is used to initialize variables and or game state before the game starts.
    // It is called only once during the lifetime of the script instance.
    void Start()
    {
        // Initialize the rotationSpeed to 50f, which will define how fast the cube rotates.
        rotationSpeed = 50f;

        // Initialize the rotation to 0f, meaning there's no rotation applied initially.
        rotation = 0f;
    }

    // Update is called once per frame.
    // It is the main workhorse method for frame updates which runs every frame.
    // All the game logic needed to be updated every frame are usually kept here.
    void Update()
    {
        // Calculate the amount of rotation needed at this frame.
        // Time.deltaTime gives the time (in seconds) since the last frame, 
        // hence multiplying it with rotationSpeed gives rotation per frame.
        // It makes sure that the rotation is frame-rate independent and smooth.
        rotation = rotationSpeed * Time.deltaTime;

        // Apply the calculated rotation to the cube on its x-axis.
        // The Rotate method is modifying the rotation property of the cube’s transform.
        // The transformation is what allows the actual rotation of the cube in the game.
        transform.Rotate(rotation, 0f, 0f);
    }
}