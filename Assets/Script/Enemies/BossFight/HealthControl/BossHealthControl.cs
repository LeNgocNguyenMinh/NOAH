using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossHealthControl : MonoBehaviour
{
    [SerializeField]private BossStatus bossStatus;
    [SerializeField]private BossHealthBar bossHealthBar;
    private float currentHealth;
    private float maxHealth;
    [SerializeField]private TextMeshProUGUI levelText;
    // Start is called before the first frame update
    void Start()
    {
        bossStatus.UpdateLevel();
        bossStatus.UpdateMaxHealth();
        bossStatus.UpdateDamage();
        UpdateLevelText();

        maxHealth = bossStatus.bossMaxHealth;
        currentHealth = maxHealth;

        bossHealthBar.SetMaxHealth(maxHealth);
        bossHealthBar.SetHealth(maxHealth);
        bossHealthBar.UpdateHealthText();
    }
    private void UpdateLevelText() //Update Health Text only when something change
    {
        levelText.text = $"LEVEL {bossStatus.bossLevel}";
    }
    public void BossHurt(float damage)
    {
        currentHealth -= damage;
        if(currentHealth <=0)
        {
            currentHealth = 0;
            bossHealthBar.UpdateHealthText();
            bossHealthBar.SetHealth(currentHealth);    
            if(bossStatus.bossID == "B_01")//The Ancient One
            {
                AOBoss.Instance.BossDeath();
            }  
            if(bossStatus.bossName == "B_03")//FruitKing
            {
                FKBoss.Instance.BossDeath();
            }       
        }
        bossHealthBar.SetHealth(currentHealth);
        bossHealthBar.UpdateHealthText();
    }
    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}
