using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HoleShop : MonoBehaviour
{
    private MoneyManager moneyManager;
    public DOTweenAnimation holeSpeedAnim, holeRadiusAnim, holeTimerAnim;
    
    public HoleMovement holeMovement;
    private int holeSpeedLevel = 1;
    private int holeSpeedPrice = 100;
    private int holeSpeedPriceIncreaseAmount = 150;
    public TextMeshProUGUI holeSpeedLevelText, holeSpeedPriceText;

    public Transform HoleTransform;
    private int holeRadiusLevel = 1;
    private int holeRadiusPrice = 100;
    private int holeRadiusPriceIncreaseAmount = 150;
    public TextMeshProUGUI holeRadiusLevelText, holeRadiusPriceText;

    public Timer timer;
    private int holeTimerLevel = 1;
    private int holeTimerPrice = 100;
    private int holeTimerPriceIncreaseAmount = 150;
    public TextMeshProUGUI holeTimerLevelText, holeTimerPriceText;

    public Image holeSpeedImage;
    public Image holeRadiusImage;
    public Image holeTimerImage;

    public Sprite holeSpeedActiveSprite, holeSpeedCloseSprite;
    public Sprite holeRadiusActiveSprite, holeRadiusCloseSprite;
    public Sprite holeTimerActiveSprite, holeTimerCloseSprite;

    private bool holeSpeedMax, holeRadiusMax, holeTimerMax;
    private void Start()
    {
        moneyManager = MoneyManager.Instance;
        ShopControl();
    }

    private void OnEnable()
    {
        MoneyManager.earnMoneyEvent += ShopControl;
        ShopControl();
    }

    private void OnDisable()
    {
        MoneyManager.earnMoneyEvent -= ShopControl;
    }

    public void BuyHoleSpeed()
    {
        if (moneyManager.money >= holeSpeedPrice && !holeSpeedMax)
        {
            moneyManager.PayMoney(holeSpeedPrice);
            holeSpeedAnim.DOPlay();
            holeMovement.movementSpeed += 0.25f;
            holeSpeedLevel++;
            holeSpeedPrice += holeSpeedPriceIncreaseAmount;
            holeSpeedLevelText.text = "Lv." + holeSpeedLevel;
            holeSpeedPriceText.text = "$" + holeSpeedPrice;
            ShopControl();
            MaxLevelControl();
        }
    }

    public void BuyHoleRadius()
    {
        if (moneyManager.money >= holeRadiusPrice && !holeRadiusMax)
        {
            moneyManager.PayMoney(holeRadiusPrice);
            holeRadiusAnim.DOPlay();
            HoleTransform.DOScale(
                new Vector3(HoleTransform.localScale.x + 0.1f, HoleTransform.localScale.y,
                    HoleTransform.localScale.z + 0.1f),0.1f);
            holeRadiusLevel++;
            holeRadiusPrice += holeRadiusPriceIncreaseAmount;
            holeRadiusLevelText.text = "Lv." + holeRadiusLevel;
            holeRadiusPriceText.text = "$" + holeRadiusPrice;
            ShopControl();
            MaxLevelControl();
        } 
    }

    public void BuyHoleTimer()
    {
        if (moneyManager.money >= holeTimerPrice && !holeTimerMax)
        {
            moneyManager.PayMoney(holeTimerPrice);
            holeTimerAnim.DOPlay();
            timer.AddingTime(2);
            holeTimerLevel++;
            holeTimerPrice += holeTimerPriceIncreaseAmount;
            holeTimerLevelText.text = "Lv." + holeTimerLevel;
            holeTimerPriceText.text = "$" + holeTimerPrice;
            ShopControl();
            MaxLevelControl();
        }
    }

    public void ShopControl()
    {
        if (!holeSpeedMax)
        {
            holeSpeedImage.sprite = moneyManager.money >= holeSpeedPrice ? holeSpeedActiveSprite : holeSpeedCloseSprite;
        }

        if (!holeRadiusMax)
        {
            holeRadiusImage.sprite = moneyManager.money >= holeRadiusPrice ? holeRadiusActiveSprite : holeRadiusCloseSprite;
        }

        if (!holeTimerMax)
        {
            holeTimerImage.sprite = moneyManager.money >= holeTimerPrice ? holeTimerActiveSprite : holeTimerCloseSprite;
        }
    }

    private void MaxLevelControl()
    {
        if (holeSpeedLevel == 6 && !holeSpeedMax)
        {
            holeSpeedImage.sprite = holeSpeedCloseSprite;
            holeSpeedLevelText.text = "";
            holeSpeedPriceText.text = "Max.";
            holeSpeedMax = true;
        }
        if (holeRadiusLevel == 6 && !holeRadiusMax)
        {
            holeRadiusImage.sprite = holeRadiusCloseSprite;
            holeRadiusLevelText.text = "";
            holeRadiusPriceText.text = "Max.";
            holeRadiusMax = true;
        }
        if (holeTimerLevel == 6 && !holeTimerMax)
        {
            holeTimerImage.sprite = holeTimerCloseSprite;
            holeTimerLevelText.text = "";
            holeTimerPriceText.text = "Max.";
            holeTimerMax = true;
        }
    }
    

    
}
