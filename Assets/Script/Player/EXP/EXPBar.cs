using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class EXPBar : MonoBehaviour
{
    [SerializeField]private Image expBarImage;
    [SerializeField]private TextMeshProUGUI expText;
    [SerializeField]private TextMeshProUGUI levelText;
    private float maxEXP;
    private float currentEXP;
    private float target;
    public void SetMaxEXP(float exp)//Set max value need for level up
    {
       maxEXP = exp;
    }
    public void SetEXP(float exp)//Set current value 
    {
        currentEXP = exp;
        target = currentEXP / maxEXP;
        expBarImage.DOFillAmount(target, .3f).SetEase(Ease.Linear);
    }
    public void UpdateEXPText()
    {
        expText.text = $"-{(int)currentEXP}/{(int)maxEXP}-";
    }
    public void UpdateLevelText(int level)
    {
        levelText.text = $"{level}";
    }
}
