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
        bossStatus.UpdateLevel();
        bossStatus.UpdateMaxHealth();
        bossStatus.UpdateDamage();
        UpdateLevelText();

        maxHealth = bossStatus.bossMaxHealth;
        currentHealth = maxHealth;

        bossHealthBar.SetMaxHealth(maxHealth);
        bossHealthBar.SetHealth(maxHealth);
        bossHealthBar.UpdateHealthText();
        UpdateLevelText();
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
        }
        bossHealthBar.UpdateHealthText();
        bossHealthBar.SetHealth(currentHealth);
    }
    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}
