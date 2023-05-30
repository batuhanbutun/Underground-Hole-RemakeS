using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    private StackManager stackManager;
    private GameObject tempObj;
    private BuildingTrigger buildingTrigger;
    private bool movementDelay = true;

    private float delay = 0f;

    private void Start()
    {
        stackManager = GetComponent<StackManager>();
    }
    
    private void CollectableToPool(GameObject material,int materialNo)
    {
        CollectablePool.Instance.AddPoolToMaterial(material,materialNo);
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("constarea"))
        {
            buildingTrigger = other.gameObject.GetComponent<BuildingTrigger>();
            buildingTrigger.GetCharacterAnim(GetComponent<CharacterMovement>());
            Vector3 moveDir = (buildingTrigger.transform.position - transform.position) / 1.2f;
            moveDir.y = 0;
            if (buildingTrigger.iron > 0)
            {
                Vector3 pos = Camera.main.ViewportToWorldPoint(buildingTrigger.ironImageTransform.position);
                delay = 0f;
                while (stackManager.ironCount > 0)
                {
                    var sequence = DOTween.Sequence();
                    GameObject tempObj = stackManager.ironList[0];
                    stackManager.ironList.Remove(tempObj);
                    stackManager.collectableList.Remove(tempObj);
                    tempObj.GetComponent<CollectableController>().UnCollect();
                    tempObj.transform.DOScale(Vector3.one * 0.1f, 0.6f).SetDelay(delay).OnComplete(() => CollectableToPool(tempObj,1));
                    sequence.Append(tempObj.transform.DOMove(Camera.main.WorldToViewportPoint(pos) + Vector3.up * 0.5f - moveDir, 0.5f).SetEase(Ease.Linear).SetDelay(delay));
                    sequence.Append(tempObj.transform.DOMove(Camera.main.WorldToViewportPoint(pos), 0.1f).SetEase(Ease.Linear)
                        .OnComplete(() => buildingTrigger.GettingIron()));
                    buildingTrigger.iron--;
                    stackManager.ironCount--;
                    delay += 0.08f;
                    if (buildingTrigger.iron == 0)
                        break;
                }
            }
            if (buildingTrigger.wood > 0)
            {
                Vector3 pos = Camera.main.ViewportToWorldPoint(buildingTrigger.woodImageTransform.position);
                delay = 0f;
                while (stackManager.woodCount > 0)
                {
                    var sequence = DOTween.Sequence();
                    GameObject tempObj = stackManager.woodList[0];
                    stackManager.woodList.Remove(tempObj);
                    stackManager.collectableList.Remove(tempObj);
                    tempObj.GetComponent<CollectableController>().UnCollect();
                    tempObj.transform.DOScale(Vector3.one * 0.1f, 0.6f).SetDelay(delay).OnComplete(() => CollectableToPool(tempObj,2));
                    sequence.Append(tempObj.transform.DOMove(Camera.main.WorldToViewportPoint(pos) + Vector3.up * 0.5f - moveDir, 0.5f).SetEase(Ease.Linear).SetDelay(delay));
                    sequence.Append(tempObj.transform.DOMove(Camera.main.WorldToViewportPoint(pos), 0.1f).SetEase(Ease.Linear)
                        .OnComplete(() => buildingTrigger.GettingWood()));
                    stackManager.woodCount--;
                    buildingTrigger.wood--;
                    delay += 0.08f;
                    if (buildingTrigger.wood == 0)
                        break;
                }
            }
            if (buildingTrigger.plastic > 0)
            {
                Vector3 pos = Camera.main.ViewportToWorldPoint(buildingTrigger.plasticImageTransform.position);
                delay = 0f;
                while (stackManager.plasticCount > 0)
                {
                    var sequence = DOTween.Sequence();
                    GameObject tempObj = stackManager.plasticList[0];
                    stackManager.plasticList.Remove(tempObj);
                    stackManager.collectableList.Remove(tempObj);
                    tempObj.GetComponent<CollectableController>().UnCollect();
                    tempObj.transform.DOScale(Vector3.one * 0.1f, 0.6f).SetDelay(delay).OnComplete(() => CollectableToPool(tempObj,3));
                    sequence.Append(tempObj.transform.DOMove(Camera.main.WorldToViewportPoint(pos) + Vector3.up * 0.5f - moveDir, 0.5f).SetEase(Ease.Linear).SetDelay(delay));
                    sequence.Append(tempObj.transform.DOMove(Camera.main.WorldToViewportPoint(pos), 0.1f).SetEase(Ease.Linear)
                        .OnComplete(() => buildingTrigger.GettingPlastic()));
                    stackManager.plasticCount--;
                    buildingTrigger.plastic--;
                    delay += 0.08f;
                    if (buildingTrigger.plastic == 0)
                        break;
                }
            }
            StartCoroutine(ReOrderDelay());
        }
    }

    IEnumerator ReOrderDelay()
    {
        yield return new WaitForSeconds(0.75f);
        stackManager.ReOrderMaterials();
    }
}
