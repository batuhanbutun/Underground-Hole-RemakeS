using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchToHole : MonoBehaviour
{
    [SerializeField] private Image circularImage;
    private bool isPlayerTop,isHoleActivated;
    private float circleTime = 0f;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (isPlayerTop && circleTime <= 2.1f)
        {
            circleTime += Time.deltaTime;
            circularImage.fillAmount = circleTime / 2f;
        }

        if (circleTime >= 2f && !isHoleActivated)
        {
            isHoleActivated = true;
            gameManager.ControlHole();
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
        if (isHoleActivated)
            isHoleActivated = false;
    }
}
