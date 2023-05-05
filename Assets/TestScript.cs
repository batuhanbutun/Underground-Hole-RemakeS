using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public Transform toFollow;
    public float duration;
    public float posY;
    public Vector3 offSet;

    public bool isCollected;
    private void Update()
    {
        if (isCollected)
        {
            Vector3 followPos = new Vector3(toFollow.position.x, posY, toFollow.position.z) + offSet;
            transform.position = Vector3.Lerp(transform.position,followPos,duration);
        }
    }
}
