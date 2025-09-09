using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.Unity.VisualStudio.Editor;

public class HealthControl : MonoBehaviour
{
    public static HealthControl Instance;
    private PauseMenu pauseMenu;
    [SerializeField]private PlayerStatus playerStatus;
    [SerializeField]private Player playerControl;
    private HealthBar healthBar;// Health bar
    private float healthCurrentValue;// Health current value
    private float healthMaxValue; // Health max value need to achive for level up
    private Animator animator;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        healthBar = GetComponent<HealthBar>();
        //Set Start Value same with data (player status)
        healthCurrentValue = playerStatus.currentHealth;
        healthMaxValue = playerStatus.maxHealth;

        healthBar.SetMaxHealth(healthMaxValue);
        healthBar.SetHealth(healthCurrentValue); 
        
        healthBar.UpdateHealthText();
    }
    public void UpdateMaxHealth() //Update max Health when player Add point to Health 
    {
        healthMaxValue = playerStatus.maxHealth;
        healthBar.SetMaxHealth(healthMaxValue);
        healthBar.UpdateHealthText();
    }
    public void PlayerHurt(float damageAmount) //Player hurt by enemy
    {
        healthCurrentValue -= damageAmount;
        if(healthCurrentValue <= 0)
        {
            healthCurrentValue = 0;
            /* BossAOShoot ancientBoss = FindObjectOfType<BossAOShoot>(); */
            /* FDManager dragonBoss = FindObjectOfType<FDManager>();

            if (ancientBoss != null)
            {
                BossAOSummon bossAOSummon = FindObjectOfType<BossAOSummon>().GetComponent<BossAOSummon>();
                bossAOSummon.PlayerDeadInBossBattle();
            }

            if (dragonBoss != null)
            {
                FDDetectStartBattle fdDetectStartBattle = FindObjectOfType<FDDetectStartBattle>().GetComponent<FDDetectStartBattle>();
                fdDetectStartBattle.PlayerDeadInBossBattle();
            } */
            playerControl.PlayerDead();// Mean player Dead
        }
        //Update Current Health and Health Text
        healthBar.SetHealth(healthCurrentValue);
        playerStatus.SetCurrentHealth(healthCurrentValue);
        healthBar.UpdateHealthText();
    }
    public void GameOverMenuActive()
    {
        pauseMenu = FindObjectOfType<PauseMenu>().GetComponent<PauseMenu>();
        pauseMenu.GameOverMenuPanelShow();
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
        playerStatus.SetCurrentHealth(healthCurrentValue);
        healthBar.UpdateHealthText();
        return true;
    }
    public void PlayerHeatlthAfterRespawn()
    {
        healthCurrentValue = healthMaxValue;
        healthBar.SetHealth(healthCurrentValue);
        healthBar.UpdateHealthText();
        animator = GetComponent<Animator>();
        animator.SetTrigger("isRespawn");
        /* playerControl.SetIsAlive(true); */
    }
}
