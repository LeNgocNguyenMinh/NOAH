using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossHealthControl : MonoBehaviour
{
    [SerializeField]private BossStatus bossStatus;
    [SerializeField]private PlayerStatus playerStatus;
    [SerializeField]private BossHealthBar bossHealthBar;
    private float currentHealth;
    private float maxHealth;
    [SerializeField]private TextMeshProUGUI healthText;
    [SerializeField]private TextMeshProUGUI levelText;
    // Start is called before the first frame update
    void Start()
    {
        if(playerStatus.playerLevel >= 4)
        {
            bossStatus.SetLevel(playerStatus.playerLevel - 2);
        }
        else{
            bossStatus.SetLevel(playerStatus.playerLevel);
        }
        bossStatus.SetMaxHealth(bossStatus.bossBaseHealth * (1 + 0.4f * bossStatus.bossLevel));
        bossStatus.SetDamage(bossStatus.bossBaseDamage + bossStatus.bossLevel);
        levelText.text = bossStatus.bossLevel + "";

        maxHealth = bossStatus.bossMaxHealth;
        currentHealth = maxHealth;

        bossHealthBar.SetMaxHealth(maxHealth);
        bossHealthBar.SetHealth(currentHealth);
        bossStatus.SetCurrentHealth(maxHealth);
        UpdateHealthText();
        UpdateLevelText();
    }
    private void UpdateHealthText() //Update Health Text only when something change
    {
        healthText.text = $"{currentHealth} / {maxHealth}";
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
            if(bossStatus.bossName == "The Ancient One")
            {
                BossAOShoot bossShoot = GetComponentInParent<BossAOShoot>();
                bossShoot.BossDeath();
            }
            if(bossStatus.bossName == "The Fallen Dragon")
            {
                FDManager fdManager = GetComponent<FDManager>();
                fdManager.DeathTrigger(); 
            }
            currentHealth = 0;
        }
        bossStatus.SetCurrentHealth(currentHealth);
        UpdateHealthText();
        bossHealthBar.SetHealth(currentHealth);
    }
    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}
