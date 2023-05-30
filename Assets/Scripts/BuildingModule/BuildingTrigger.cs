using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTrigger : MonoBehaviour
{
    [SerializeField] private GameObject constructionObj;
    [SerializeField] private GameObject ironImage;
    [SerializeField] private GameObject plasticImage;
    [SerializeField] private GameObject woodImage;
    [SerializeField] public Transform myCanvas;
    [SerializeField] public Transform woodImageTransform;
    [SerializeField] public Transform plasticImageTransform;
    [SerializeField] public Transform ironImageTransform;

    [SerializeField] private TextMeshProUGUI ironText, plasticText, woodText;
    [SerializeField] private Image fillableImage;
    private int totalMaterialCount = 0;
    public int wood;
    public int plastic;
    public int iron;
    public CharacterMovement characterMovement;
    private bool isOpened;
    private void Start()
    {
        if (wood > 0)
        {
            woodImage.SetActive(true);
            woodText.text = wood.ToString();
            totalMaterialCount += wood;
        }
        if (plastic > 0)
        {
            plasticImage.SetActive(true);
            plasticText.text = plastic.ToString();
            totalMaterialCount += plastic;
        }
        if (iron > 0)
        {
            ironImage.SetActive(true);
            ironText.text = iron.ToString();
            totalMaterialCount += iron;
        }
    }

    private void OnBecameInvisible()
    {
        Debug.Log(gameObject);
    }

    public void GettingIron()
    {
        ironText.text = iron.ToString();
        FillImageSlider();
        if (MaterialControl(iron))
            ironImage.SetActive(false);
    }

    public void GettingWood()
    {
        woodText.text = wood.ToString();
        FillImageSlider();
        if (MaterialControl(wood))
            woodImage.SetActive(false);
    }

    public void GettingPlastic()
    {
        plasticText.text = plastic.ToString();
        FillImageSlider();
        if (MaterialControl(plastic))
            plasticImage.SetActive(false);
    }

    private void FillImageSlider()
    {
        float newAmount = ((float)(totalMaterialCount - (iron + wood + plastic)) / totalMaterialCount);
        DOTween.To( () => fillableImage.fillAmount, ( newValue ) => fillableImage.fillAmount = newValue, newAmount,0.5f).OnComplete(() => ConstructionControl());
    }

    private bool MaterialControl(int materialCount)
    {
        if (materialCount == 0)
            return true;
        return false;
    }

    private void ConstructionControl()
    {
        if (wood <= 0 && plastic <= 0 && iron <= 0 && !isOpened)
        {
            isOpened = true;
            constructionObj.SetActive(true);
            myCanvas.gameObject.SetActive(false);
            characterMovement.Cheer();
            Destroy(gameObject);
        }
    }

    public void GetCharacterAnim(CharacterMovement temp)
    {
        if(characterMovement == null)
            characterMovement = temp;
    }
}
