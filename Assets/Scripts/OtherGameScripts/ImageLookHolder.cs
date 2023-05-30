using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageLookHolder : MonoBehaviour
{
    public Camera mainCam;
    private Vector3 offSet = new Vector3(0f, 360f, 0f);

    private void Start()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        transform.LookAt(mainCam.transform);
        transform.localEulerAngles += offSet;
    }
    
}
