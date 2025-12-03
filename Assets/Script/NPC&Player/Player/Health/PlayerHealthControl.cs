using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealthControl : MonoBehaviour
{
    public static PlayerHealthControl Instance;
    private PlayerHealthBar healthBar;// Health bar
    public float healthCurrentValue;// Health current value
    public float healthMaxValue; // Health max value need to achive for level up
    private Animator animator;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        healthBar = GetComponent<PlayerHealthBar>();
        //Set Start Value same with data (player status)
        healthCurrentValue = PlayerStatus.Instance.currentHealth;
        healthMaxValue = PlayerStatus.Instance.maxHealth;

        healthBar.SetMaxHealth(healthMaxValue);
        healthBar.SetHealth(healthCurrentValue); 
        
        healthBar.UpdateHealthText();
    }
    public void UpdateMaxHealth() //Update max Health when player Add point to Health 
    {
        healthMaxValue = PlayerStatus.Instance.maxHealth;
        healthBar.SetMaxHealth(healthMaxValue);
        healthBar.UpdateHealthText();
    }
    public void PlayerHurt(float damageAmount) //Player hurt by enemy
    {
        healthCurrentValue -= damageAmount;
        if(healthCurrentValue <= 0)
        {
            healthCurrentValue = 0;
            Player.Instance.PlayerDead();// Mean player Dead
        }
        //Update Current Health and Health Text
        healthBar.SetHealth(healthCurrentValue);
        PlayerStatus.Instance.SetCurrentHealth(healthCurrentValue);
        healthBar.UpdateHealthText();
    }
    public bool HealthRecover(float health)
    {
        if(healthCurrentValue == healthMaxValue)
        {
            return false;
        }
        healthCurrentValue += health;

        if(healthCurrentValue >= healthMaxValue)
        {
            healthCurrentValue = healthMaxValue;
        }
        healthBar.SetHealth(healthCurrentValue);
        PlayerStatus.Instance.SetCurrentHealth(healthCurrentValue);
        healthBar.UpdateHealthText();
        return true;
    }
    public void PlayerHeatlthAfterRespawn()
    {
        healthCurrentValue = PlayerStatus.Instance.currentHealth;
        healthMaxValue =   PlayerStatus.Instance.maxHealth;
        healthBar.SetHealth(healthCurrentValue);
        healthBar.UpdateHealthText();
    }
}
