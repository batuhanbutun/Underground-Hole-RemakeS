using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : MonoBehaviour
{
    public static ParticlePool Instance { get; private set; }
    
    [SerializeField] private List<ParticleSystem> particlePool;
    private int poolIndex = 0;
    private int poolCount;
    private void Awake()
    {
        Instance = this;
        poolCount = particlePool.Count;
    }

    public void GetMoneyParticle(Vector3 pos)
    {
        particlePool[poolIndex].transform.position = pos;
        particlePool[poolIndex].Play();
        poolIndex++;
        if (poolIndex == poolCount)
            poolIndex = 0;
    }
}
