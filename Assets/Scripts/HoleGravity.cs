using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleGravity : MonoBehaviour
{
    [SerializeField] private float GRAVITY_PULL = .78f;
    public static float GravityRadius = 1f;

    private Vector3 posOfset = new Vector3(0f,-2f,0f);
    private void Awake()
    {
        GravityRadius = GetComponent<CapsuleCollider>().radius;
    }

    private void OnTriggerStay(Collider other)
    {
            float gravityIntensity = Vector3.Distance(transform.position, other.transform.position) / GravityRadius;
            other.attachedRigidbody.AddForce(((transform.position + posOfset) - other.transform.position) * gravityIntensity * other.attachedRigidbody.mass * GRAVITY_PULL * Time.smoothDeltaTime,ForceMode.Acceleration);
            //Debug.Log(other.gameObject);
    }
}
