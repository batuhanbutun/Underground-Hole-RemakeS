using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class CollectablePool : MonoBehaviour
{
    public static CollectablePool Instance { get; private set; }

    [SerializeField] private List<GameObject> ironPool;
    [SerializeField] private List<GameObject> woodPool;
    [SerializeField] private List<GameObject> plasticPool;

    public GameObject iron, wood, plastic;

    public Transform poolParent;

    private void Awake()
    {
        Instance = this;
    }

    public void AddPoolToMaterial(GameObject obj, int materialNo)//1 for iron, 2 for wood, 3 for plastic
    {
        obj.GetComponent<CollectableController>().Recycle();
        obj.SetActive(false);
        switch (materialNo)
        {
            case 1:
                ironPool.Add(obj);
                break;
            case 2:
                woodPool.Add(obj);
                break;
            case 3:
                plasticPool.Add(obj);
                break;
        }
    }


    
    public void GetIron(Vector3 spawnPos)
    {
        if (ironPool.Count > 0)
        {
            ironPool[0].transform.position = spawnPos;
            ironPool[0].SetActive(true);
            ironPool[0].transform.DOMove(new Vector3(spawnPos.x + Random.Range(-1f,1f),-3f,spawnPos.z + Random.Range(-1f,1f)),0.6f);
            ironPool.Remove(ironPool[0]);
        }
        else
        {
            GameObject ironObj = Instantiate(iron, spawnPos, iron.transform.rotation,poolParent);
            ironObj.transform.DOMove(new Vector3(spawnPos.x + Random.Range(-1f,1f),-3f,spawnPos.z + Random.Range(-1f,1f)),0.6f);
        }
    }
    
    public void GetWood(Vector3 spawnPos)
    {
        if (woodPool.Count > 0)
        {
            woodPool[0].transform.position = spawnPos;
            woodPool[0].SetActive(true);
            woodPool[0].transform.DOMove(new Vector3(spawnPos.x + Random.Range(-1f,1f),-3f,spawnPos.z + Random.Range(-1f,1f)),0.6f);
            woodPool.Remove(woodPool[0]);
        }
        else
        {
            GameObject woodObj = Instantiate(wood, spawnPos, wood.transform.rotation,poolParent);
            woodObj.transform.DOMove(new Vector3(spawnPos.x + Random.Range(-1f,1f),-3f,spawnPos.z + Random.Range(-1f,1f)),0.6f);
        }
    }
    
    public void GetPlastic(Vector3 spawnPos)
    {
        if (plasticPool.Count > 0)
        {
            plasticPool[0].transform.position = spawnPos;
            plasticPool[0].SetActive(true);
            plasticPool[0].transform.DOMove(new Vector3(spawnPos.x + Random.Range(-1f,1f),-3f,spawnPos.z + Random.Range(-1f,1f)),0.6f);
            plasticPool.Remove(plasticPool[0]);
        }
        else
        {
            GameObject plasticObj = Instantiate(plastic, spawnPos, plastic.transform.rotation,poolParent);
            plasticObj.transform.DOMove(new Vector3(spawnPos.x + Random.Range(-1f,1f),-3f,spawnPos.z + Random.Range(-1f,1f)),0.6f);
        }
    }
   
}
