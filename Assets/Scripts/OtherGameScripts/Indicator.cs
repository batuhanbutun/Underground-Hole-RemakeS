using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    public Transform objTransform;
    public Transform originTransform;
    public Vector3 offset;
    void Update()
    {
        transform.position = originTransform.position + offset;
        transform.LookAt(objTransform);
    }
}
