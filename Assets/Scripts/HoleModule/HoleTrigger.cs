using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class HoleTrigger : MonoBehaviour
{
    [Header("Switch Layer")]
    [SerializeField] private string entryLayer;
    [SerializeField] private string defaultLayer;

    private void OnTriggerEnter(Collider other)
    {
        other.attachedRigidbody.isKinematic = false;
        other.gameObject.layer = LayerMask.NameToLayer(entryLayer);
        other.gameObject.GetComponent<PropAnimTrigger>().ScaleAnim();
    }

    private void OnTriggerExit(Collider other)
    {
        other.gameObject.layer = LayerMask.NameToLayer(defaultLayer);
    }
}
