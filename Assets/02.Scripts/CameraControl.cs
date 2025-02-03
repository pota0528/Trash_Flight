using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    void Start()
    {
        var camera = GetComponent<Camera>();
        camera.orthographicSize = 3f / camera.aspect;
    }

    void Update()
    {
        
    }
}
