using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EXPBar : MonoBehaviour
{
    [SerializeField]private Slider slider;
    public void SetMaxEXP(float exp)//Set max value need for level up
    {
        slider.maxValue = exp;
    }
    public void SetEXP(float exp)//Set current value 
    {
        slider.value = exp;
    }
}
