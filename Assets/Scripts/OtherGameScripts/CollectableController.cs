using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class CollectableController : MonoBehaviour
{
    [SerializeField] private DOTweenAnimation myScaleAnim;

    public Transform toFollow;
    public float duration;
    public float posY;
    public Vector3 offSet;

    public bool isTriggerOpen = true;
    public bool isCollected;
    public ImageLookHolder imageLH;

    private Vector3 startScale;
    private int collectedIndex = 0;
    private void Start()
    {
        GameManager.toHoleControl += DropDown;
        startScale = transform.localScale;
    }

    private void OnEnable()
    {
        isTriggerOpen = true;
    }

    private void Update()
    {
        if (isCollected)
        {
            Vector3 followPos = new Vector3(toFollow.position.x, posY, toFollow.position.z) + offSet;
            transform.position = Vector3.Lerp(transform.position,followPos,duration);
        }
    }

    public void Collect(Transform stackPos,int collectIndex,int listCount)
    {
        toFollow = stackPos;
        transform.DOMove(stackPos.position + new Vector3(0f,posY,0f) + (Vector3.up * 0.3f), 0.3f).OnComplete(() => isCollected = true);
        collectedIndex = collectIndex;
        duration = 0.22f - listCount * 0.0035f;
    }

    public void ReList(int listCount)
    {
        duration = 0.22f - listCount * 0.0035f;
    }
    
    public void UnCollect()
    {
        isCollected = false;
        imageLH.enabled = false;
        transform.DOLocalRotate(new Vector3(90f,0f,135f), 1.5f, RotateMode.LocalAxisAdd);
    }

    public void Recycle()
    {
        imageLH.enabled = true;
        transform.localScale = startScale;
    }

    public void DropDown()
    {
        if (this != null)
        {
            if (isCollected)
            {
                isCollected = false;
                isTriggerOpen = true;
                if (collectedIndex % 2 == 0)
                {
                    transform.DOMove(new Vector3(transform.position.x + Random.Range(0.75f,1f), -3f, transform.position.z + Random.Range(0.5f,1.5f)), 0.5f);
                }
                else
                {
                    transform.DOMove(new Vector3(transform.position.x - Random.Range(0.75f,1f), -3f, transform.position.z + Random.Range(0.5f,1.5f)), 0.5f);
                }
            } 
        }
    }
    
    public void PlayCollectableAnim()
    {
        myScaleAnim.DOPlay();
    }
}
