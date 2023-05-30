using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class StackManager : MonoBehaviour
{
    private Vector3 collectPoint = new Vector3(0f,0.6f,-0.3f);
    private Vector3 collectableAnimPoint = new Vector3(0.08f, 1.2f, -0.5f);
    private float collectPointY = 0.6f;
    public int ironCount = 0, woodCount = 0, plasticCount = 0;
    public List<GameObject> collectableList;
    public List<GameObject> ironList;
    public List<GameObject> woodList;
    public List<GameObject> plasticList;

    public Transform stackPos;
    private int collectIndex = 1;

    public int stackCapacity;
    public GameObject fullCapacityText;
    private void Start()
    {
        GameManager.toHoleControl += EmptyCollection;
        stackCapacity = 20;
    }
    

    IEnumerator CollectableListAnim()
    {
        int i = collectableList.Count - 1;
        while (i >= 0)
        {
            yield return new WaitForSeconds(0.04f);
            if(i < collectableList.Count - 1)
                collectableList[i].GetComponent<CollectableController>().PlayCollectableAnim();
            i--;
        }
    }

    public void ReOrderMaterials()
    {
        collectPointY = 0.6f;
        for (int i = 0; i < collectableList.Count; i++)
        {
            collectableList[i].GetComponent<CollectableController>().posY = i * 0.05f;
            collectableList[i].GetComponent<CollectableController>().ReList(i);
            //collectableList[i].transform.localPosition = new Vector3(collectPoint.x,collectPointY , collectPoint.z);
            //collectPointY += 0.1f;
        }
        CapacityControl();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("iron") && stackCapacity > collectableList.Count)
        {
            if (other.gameObject.GetComponent<CollectableController>().isTriggerOpen)
            {
                CollectMaterials(other.gameObject);
                ironList.Add(other.gameObject);
                ironCount++;
            }
        }
        else if (other.CompareTag("wood") && stackCapacity > collectableList.Count)
        {
            if (other.gameObject.GetComponent<CollectableController>().isTriggerOpen)
            {
                CollectMaterials(other.gameObject);
                woodList.Add(other.gameObject);
                woodCount++;
            }
        }
        else if (other.CompareTag("plastic") && stackCapacity > collectableList.Count)
        {
            if (other.gameObject.GetComponent<CollectableController>().isTriggerOpen)
            {
                CollectMaterials(other.gameObject);
                plasticList.Add(other.gameObject);
                plasticCount++;
            }
        }
    }

    private void CollectMaterials(GameObject material)
    {
        collectableList.Add(material.gameObject);
            material.gameObject.GetComponent<CollectableController>().isTriggerOpen = false;
            material.gameObject.GetComponent<CollectableController>().posY = (collectableList.Count * 0.05f);
            material.gameObject.GetComponent<CollectableController>().PlayCollectableAnim();
            material.gameObject.GetComponent<CollectableController>()
                .Collect(stackPos, collectIndex, collectableList.Count);
            collectIndex++;
            StartCoroutine(CollectableListAnim());
            CapacityControl();
    }

    private void EmptyCollection()
    {
        if (this != null)
        {
            collectableList.Clear();
            ironList.Clear();
            woodList.Clear();
            plasticList.Clear();
            woodCount = 0;
            plasticCount = 0;
            ironCount = 0;
            CapacityControl();
        }
    }

    public void CapacityControl()
    {
        fullCapacityText.SetActive(stackCapacity <= collectableList.Count);
    }

}
