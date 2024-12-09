using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    public Transform level1;           // Record the object's initial position

    private Vector3 startPos;
    public float parallaxFactor = 0.3f; // Factor controlling the depth appearance
    public Transform camera;            // Reference to the main camera

    // Start is called before the first frame update
    void Start()
    {
        startPos = level1.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraDelta = camera.position - startPos;
        level1.position = startPos + cameraDelta * parallaxFactor;
    }
}
