using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealthControl : MonoBehaviour
{
    public static PlayerHealthControl Instance;
    public float healthCurrentValue;// Health current value
    public float healthMaxValue; // Health max value need to achive for level up

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PlayerHurt(float damageAmount) //Player hurt by enemy
    {
        healthCurrentValue -= damageAmount;
        if(healthCurrentValue <= 0)
        {
            healthCurrentValue = 0;
            PlayerHealthBar.Instance.UpdateHealthText();
            Player.Instance.PlayerDead();// Mean player Dead
            return;
        }
        //Update Current Health and Health Text
        PlayerStatus.Instance.SetCurrentHealth(healthCurrentValue);
        SetCurrentHealthStatus();
    }
    public bool HealthCanRecover(float health)
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
        PlayerStatus.Instance.SetCurrentHealth(healthCurrentValue);
        SetCurrentHealthStatus();
        return true;
    }
    public void PlayerHeatlthAfterRespawn()
    {
        SetCurrentHealthStatus();
    }
    public void SetCurrentHealthStatus()
    {
        healthMaxValue = PlayerStatus.Instance.maxHealth;
        healthCurrentValue = PlayerStatus.Instance.currentHealth;
        PlayerHealthBar.Instance.SetCurrentHealth(healthCurrentValue);
        PlayerHealthBar.Instance.UpdateHealthText();
    }
}
