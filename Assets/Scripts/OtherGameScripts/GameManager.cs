using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public delegate void ToPlayerControlDelegate();

    public static event ToPlayerControlDelegate toPlayerControl;
    
    public delegate void ToHoleControlDelegate();

    public static event ToHoleControlDelegate toHoleControl;
    
    [SerializeField] private GameObject holeCam;
    [SerializeField] private GameObject characterCam;
    [SerializeField] private GameObject holeShop;
    [SerializeField] private GameObject timerCanvas;
    [SerializeField] private Transform controlPC;//-3.486

    public Image controllerImageBG;
    public Image controllerImageHandle;
    

    private void Start()
    {
        toPlayerControl += SwitchToPlayer;
        toHoleControl += SwitchToHole;
    }

    public void ControlPlayer()
    {
        toPlayerControl();
    }
    
    public void ControlHole()
    {
        toHoleControl();
        controllerImageBG.enabled = false;
        controllerImageHandle.enabled = false;
        StartCoroutine(ActiveShop());
    }
    
    public void SwitchToPlayer()
    {
        if (this != null)
        {
            holeCam.SetActive(false);
            characterCam.SetActive(true);
            timerCanvas.SetActive(false);
            controlPC.transform.DOMoveY(-4.415f, 1f).SetDelay(1f);
        }
    }

    public void SwitchToHole()
    {
        characterCam.SetActive(false);
        holeCam.SetActive(true);
        controlPC.transform.DOMoveY(-3.486f, 1f);
    }

    public void CloseShop()
    {
        holeShop.SetActive(false);
        controllerImageBG.enabled = true;
        controllerImageHandle.enabled = true;
    }
    
    IEnumerator ActiveShop()
    {
        yield return new WaitForSeconds(1f);
        holeShop.SetActive(true);
    }
}
