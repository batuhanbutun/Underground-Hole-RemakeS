using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class PropAnimTrigger : MonoBehaviour
{
    [SerializeField] private float toScaleValue;
    [SerializeField] private float startScaleValue;
    [SerializeField] private float duration;
    private int randomMaterialNumber;
    public void ScaleAnim()
    {
        transform.DOScaleX(toScaleValue, duration).OnComplete(()=> transform.DOScaleX(startScaleValue,duration));
    }

    IEnumerator ConvertToMaterials()
    {
        yield return new WaitForSeconds(0.2f);
        CollectablePool collectPool = CollectablePool.Instance;
        randomMaterialNumber = Random.Range(0, 3);
        switch (randomMaterialNumber)
        {
            case 0:
                collectPool.GetIron(transform.position);
                break;
            case 1:
                collectPool.GetWood(transform.position);
                break;
            case 2:
                collectPool.GetPlastic(transform.position);
                break;
        }
        Vector3 position = transform.position;
        MoneyManager.Instance.EarnMoney(2,position);
        ParticlePool.Instance.GetMoneyParticle(position);
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("propconverter"))
        {
            StartCoroutine(ConvertToMaterials());
        }
    }
}
