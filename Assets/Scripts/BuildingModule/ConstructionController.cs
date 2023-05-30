using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ConstructionController : MonoBehaviour
{
    [SerializeField] private Transform moneyPos;
    [SerializeField] private float moneyEarnDuration;
    [SerializeField] private int howMuchCashEarning;
    [SerializeField] private ParticleSystem confetiParticle;
    private MoneyManager moneyManager;

    public bool isEarningCash = true;
    void Start()
    {
        moneyManager = MoneyManager.Instance;
        StartCoroutine(GainMoney());
        confetiParticle.transform.position = transform.position;
        confetiParticle.Play();
    }

    IEnumerator GainMoney()
    {
        while (isEarningCash)
        {
            yield return new WaitForSeconds(moneyEarnDuration);
            moneyManager.EarnMoney(howMuchCashEarning,moneyPos.position);
            transform.DOPunchScale(Vector3.one * 0.2f, 0.2f, 1, 0.33f);
        }
    }
    
}
