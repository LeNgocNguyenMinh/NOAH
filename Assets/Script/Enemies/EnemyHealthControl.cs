using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class EnemyHealthControl : MonoBehaviour
{
    [SerializeField]private MovingEnemy moveEnemy;
    [SerializeField]private StandingEnemy standEnemy;
    [Header("UI Elements")]
    [SerializeField]private TextMeshProUGUI levelText;
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
        UpdateHealthText();
    }

    public void SetHealth()
    {
        float target = currentHealth / maxHealth;
        if(healthBarFrontImage.fillAmount > healthBarBackImage.fillAmount)
        {
            healthBarBackImage.fillAmount = healthBarFrontImage.fillAmount;
        }
        healthBarFrontImage.DOFillAmount(target, .1f).SetEase(Ease.Linear).SetUpdate(true);
        healthBarBackImage.DOFillAmount(target, .5f).SetEase(Ease.Linear).SetUpdate(true);
    }
    public void UpdateHealthText() //Update Health Text only when something change
    {
        healthText.text = $"{currentHealth} / {maxHealth}";
    }
    public void EnemyHurt(float damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            if(moveEnemy != null)
            {
                moveEnemy.Die();
            }
            if(standEnemy != null)
            {
                standEnemy.Die();
            }
        }
        UpdateHealthText();
        SetHealth();
    }
}
