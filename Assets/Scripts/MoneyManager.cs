using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI moneyText;
    public int money = 0;

    [SerializeField] private List<MoneyAnim> moneyImagePool;
    private int poolIndex = 0;
    private int poolLength;
    
    public delegate void ToEarnMoneyDelegate();

    public static event ToEarnMoneyDelegate earnMoneyEvent;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        moneyText.text = money.ToString();
        poolLength = moneyImagePool.Count;
    }

    public void MoneyHack()
    {
        money += 25000;
        moneyText.text = money.ToString();
    }

    public void EarnMoney(int amount,Vector3 position)
    {
        money += amount;
        moneyText.text = money.ToString();
        moneyImagePool[poolIndex].transform.position = position;
        moneyImagePool[poolIndex].gameObject.SetActive(true);
        moneyImagePool[poolIndex].SetText(amount);
        poolIndex++;
        if (poolIndex == poolLength - 1)
            poolIndex = 0;
        if (earnMoneyEvent != null) earnMoneyEvent();
    }

    public void PayMoney(int amount)
    {
        money -= amount;
        moneyText.text = money.ToString();
    }
}
