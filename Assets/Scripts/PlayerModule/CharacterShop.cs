using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterShop : MonoBehaviour
{
    private MoneyManager moneyManager;
    public DOTweenAnimation cSpeedAnim, cRadiusAnim, cStackAnim;
    
    public CharacterMovement characterMovement;
    private int cSpeedLevel = 1;
    private int cSpeedPrice = 100;
    private int cSpeedPriceIncreaseAmount = 150;
    public TextMeshProUGUI cSpeedLevelText, cSpeedPriceText;

    public StackManager stackManager;
    public Transform collectMagnet;
    private int cRadiusLevel = 1;
    private int cRadiusPrice = 100;
    private int cRadiusPriceIncreaseAmount = 150;
    public TextMeshProUGUI cRadiusLevelText, cRadiusPriceText;
    
    private int cStackLevel = 1;
    private int cStackPrice = 100;
    private int cStackPriceIncreaseAmount = 150;
    public TextMeshProUGUI cStackLevelText, cStackPriceText;

    public Image cSpeedImage;
    public Image cRadiusImage;
    public Image cStackImage;

    public Sprite cSpeedActiveSprite, cSpeedCloseSprite;
    public Sprite cRadiusActiveSprite, cRadiusCloseSprite;
    public Sprite cStackActiveSprite, cStackCloseSprite;

    private bool cSpeedMax, cRadiusMax, cStackMax;

    public DOTweenAnimation charAnim;
    
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
    
    public void BuyCSpeed()
    {
        if (moneyManager.money >= cSpeedPrice && !cSpeedMax)
        {
            charAnim.DOPlay();
            moneyManager.PayMoney(cSpeedPrice);
            cSpeedAnim.DOPlay();
            characterMovement.IncreaseSpeed();
            cSpeedLevel++;
            cSpeedPrice += cSpeedPriceIncreaseAmount;
            cSpeedLevelText.text = "Lv." + cSpeedLevel;
            cSpeedPriceText.text = "$" + cSpeedPrice;
            ShopControl();
            MaxLevelControl();
        }
    }
    
    public void BuyCRadius()
    {
        if (moneyManager.money >= cRadiusPrice && !cRadiusMax)
        {
            charAnim.DOPlay();
            moneyManager.PayMoney(cRadiusPrice);
            cRadiusAnim.DOPlay();
            collectMagnet.DOScale(collectMagnet.localScale + Vector3.one * 0.2f, 0.1f);
            cRadiusLevel++;
            cRadiusPrice += cRadiusPriceIncreaseAmount;
            cRadiusLevelText.text = "Lv." + cRadiusLevel;
            cRadiusPriceText.text = "$" + cRadiusPrice;
            ShopControl();
            MaxLevelControl();
        } 
    }
    
    public void BuyCStack()
    {
        if (moneyManager.money >= cStackPrice && !cStackMax)
        {
            charAnim.DOPlay();
            moneyManager.PayMoney(cStackPrice);
            cStackAnim.DOPlay();
            stackManager.stackCapacity += 8;
            stackManager.CapacityControl();
            cStackLevel++;
            cStackPrice += cStackPriceIncreaseAmount;
            cStackLevelText.text = "Lv." + cStackLevel;
            cStackPriceText.text = "$" + cStackPrice;
            ShopControl();
            MaxLevelControl();
        }
    }
    
    public void ShopControl()
    {
        if (!cSpeedMax)
        {
            cSpeedImage.sprite = moneyManager.money >= cSpeedPrice ? cSpeedActiveSprite : cSpeedCloseSprite;
        }

        if (!cRadiusMax)
        {
            cRadiusImage.sprite = moneyManager.money >= cRadiusPrice ? cRadiusActiveSprite : cRadiusCloseSprite;
        }

        if (!cStackMax)
        {
            cStackImage.sprite = moneyManager.money >= cStackPrice ? cStackActiveSprite : cStackCloseSprite;
        }
    }

    private void MaxLevelControl()
    {
        if (cSpeedLevel == 6 && !cSpeedMax)
        {
            cSpeedImage.sprite = cSpeedCloseSprite;
            cSpeedLevelText.text = "";
            cSpeedPriceText.text = "Max.";
            cSpeedMax = true;
        }
        if (cRadiusLevel == 6 && !cRadiusMax)
        {
            cRadiusImage.sprite = cRadiusCloseSprite;
            cRadiusLevelText.text = "";
            cRadiusPriceText.text = "Max.";
            cRadiusMax = true;
        }
        if (cStackLevel == 6 && !cStackMax)
        {
            cStackImage.sprite = cStackCloseSprite;
            cStackLevelText.text = "";
            cStackPriceText.text = "Max.";
            cStackMax = true;
        }
        
    }
    
}
