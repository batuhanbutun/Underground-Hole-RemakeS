using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenShop : MonoBehaviour
{
    
    [SerializeField] private Image circularImage;
    [SerializeField] private GameObject upgradePanel;
    private bool isPlayerTop,isShopActivated;
    private float circleTime = 0f;
    
    

    private void Update()
    {
        if (isPlayerTop && circleTime <= 2.1f)
        {
            circleTime += Time.deltaTime;
            circularImage.fillAmount = circleTime / 2f;
        }

        if (circleTime >= 2f && !isShopActivated)
        {
            isShopActivated = true;
            upgradePanel.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        isPlayerTop = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isPlayerTop = false;
        circleTime = 0f;
        circularImage.fillAmount = 0f;
        if (isShopActivated)
            isShopActivated = false;
    }
}
