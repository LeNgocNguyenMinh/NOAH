using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class BossHealthControl : MonoBehaviour
{
    [SerializeField]private BossStatus bossStatus;
    private float currentHealth;
    private float maxHealth;
    [SerializeField]private TextMeshProUGUI levelText;
    // Start is called before the first frame update
    [SerializeField]private Image healthBarFrontImage;
    [SerializeField]private Image healthBarBackImage;
    [SerializeField]private TextMeshProUGUI healthText;

    public void SetMaxHealth(float health)
    {
        healthBarFrontImage.fillAmount = 1f;
        healthBarBackImage.fillAmount = 1f;
        currentHealth = health;
        maxHealth = health;
        UpdateHealthText();
    }

    public void SetHealth(float health)
    {
        currentHealth = health;
        float target = health / maxHealth;
        if(healthBarFrontImage.fillAmount > healthBarBackImage.fillAmount)
        {
            healthBarBackImage.fillAmount = healthBarFrontImage.fillAmount;
        }
        healthBarFrontImage.DOFillAmount(target, .1f).SetEase(Ease.Linear).SetUpdate(true);
        healthBarBackImage.DOFillAmount(target, .5f).SetEase(Ease.Linear).SetUpdate(true);
    }
    public void UpdateHealthText()
    {
        Debug.Log(currentHealth + "/" + maxHealth);
        healthText.text = $"{(int)currentHealth}/{(int)maxHealth}";
    }
    public void BossHurt(float damage)
    {
        currentHealth -= damage;
        if(currentHealth <=0)
        {
            currentHealth = 0;
            UpdateHealthText();
            SetHealth(currentHealth);    
            if(bossStatus.bossID == "B_01")//The Ancient One
            {
                AOBoss.Instance.BossDeath();
            }  
            if(bossStatus.bossName == "B_03")//FruitKing
            {
                FKBoss.Instance.BossDeath();
            }       
        }
        SetHealth(currentHealth);
        UpdateHealthText();
    }
    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}
