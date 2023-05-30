using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class MoneyAnim : MonoBehaviour
{
    [SerializeField] private DOTweenAnimation myAnim;
    [SerializeField] private TextMeshProUGUI myText;
    
    private void OnEnable()
    {
        myAnim.DOPlay();
        transform.DOLocalMoveY(transform.localPosition.y + 1f, 1f);
    }

    public void SetText(int amount)
    {
        myText.text = amount + "$";
    }
}
