using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    public Transform level1;           

    private Vector3 startPos;
    public float parallaxFactor = 0.3f; 
    public Transform camera;        

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
