using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleManager : MonoBehaviour
{
    public static HoleManager Instance { get; private set; }
    [SerializeField] private Timer holeTimer;

    private void Awake()
    {
        Instance = this;
    }

    public void TimerStart()
    {
        holeTimer.StartTimer();
    }
    
}
