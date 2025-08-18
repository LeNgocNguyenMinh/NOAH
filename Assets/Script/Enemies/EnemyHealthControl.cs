using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyHealthControl : MonoBehaviour
{
    [SerializeField]private EnemyStatus enemyStatus;
    [SerializeField]private PlayerStatus playerStatus;
    [SerializeField]private MovingEnemy moveEnemy;
    [SerializeField]private StandingEnemy standEnemy;
    private float enemyMaxHealth;
    private float enemyCurrentHealth;
    private EnemyHealthBar enemyHealthBar;
    [SerializeField]private TextMeshProUGUI levelText;
    private void Start()
    {
        //In here, we control both Health, damage and level of enemy.
        if(playerStatus.playerLevel % 3 == 0)//Mean only when player level is 3, 6, 9
        {
            enemyStatus.SetLevel(playerStatus.playerLevel / 2);//then enemy level is 1, 3, 4, ...
            //then damage, maxHealth calculate to fit with the level 
            enemyStatus.SetMaxHealth(enemyStatus.enemyBaseHealth * (1 + 0.4f * enemyStatus.enemyLevel));
        }
        enemyStatus.SetDamage(enemyStatus.enemyBaseDamage + enemyStatus.enemyLevel);
        levelText.text = enemyStatus.enemyLevel + "";
        enemyHealthBar = GetComponentInChildren<EnemyHealthBar>();

        enemyMaxHealth = enemyStatus.enemyMaxHealth;
        enemyCurrentHealth = enemyMaxHealth;

        enemyHealthBar.SetMaxHealth(enemyMaxHealth);
        /* enemyHealthBar.UpdateHealthText(); */
    }
    
    public void EnemyHurt(float damage)
    {
        enemyCurrentHealth -= damage;
        if(enemyCurrentHealth <= 0)
        {
            enemyCurrentHealth = 0;
            if(moveEnemy != null)
            {
                moveEnemy.Die();
            }
            if(standEnemy != null)
            {
                standEnemy.Die();
            }
        }
        enemyHealthBar.UpdateHealthText();
        enemyHealthBar.SetHealth(enemyCurrentHealth);
    }
}
