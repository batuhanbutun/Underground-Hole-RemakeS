using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform obj;
    public Vector3 offSet;

    
    void Update()
    {
        transform.position = obj.position + offSet;
    }
}
