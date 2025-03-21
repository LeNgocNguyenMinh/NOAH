using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]private Slider slider;
    [SerializeField] private Slider backSlider;
    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
        backSlider.maxValue = health;
        backSlider.value = health;
    }

    public void SetHealth(float health)
    {
        slider.value = health;
        SmoothSlider(slider.value);
    }

    public void SmoothSlider(float value)
    {
        backSlider.DOValue(value, 2f, false);
    }
}