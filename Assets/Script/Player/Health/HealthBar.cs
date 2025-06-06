using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    
    [SerializeField]private Image healthBarFrontImage;
    [SerializeField]private Image healthBarBackImage;
    [SerializeField]private TextMeshProUGUI healthText;
    private float currentHealth;
    private float maxHealth;
    public void SetMaxHealth(float health)
    {
        healthBarFrontImage.fillAmount = 1f;
        healthBarBackImage.fillAmount = 1f;
        currentHealth = health;
        maxHealth = health;
    }

    public void SetHealth(float health)
    {
        currentHealth = health;
        float target = health / maxHealth;
        if(healthBarFrontImage.fillAmount > healthBarBackImage.fillAmount)
        {
            healthBarBackImage.fillAmount = healthBarFrontImage.fillAmount;
        }
        healthBarFrontImage.DOFillAmount(target, .3f).SetEase(Ease.Linear);
        healthBarBackImage.DOFillAmount(target, .5f).SetEase(Ease.Linear);
    }
    public void UpdateHealthText()
    {
        healthText.text = $"-{(int)currentHealth}/{(int)maxHealth}-";
    }
}