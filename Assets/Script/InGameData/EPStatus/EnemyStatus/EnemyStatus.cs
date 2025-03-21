using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyStatus", menuName = "EnemyStatus")]
public class EnemyStatus : ScriptableObject
{
    public int enemyLevel;
    public string enemyName;
    public float enemyBaseDamage;
    public float enemyDamage;
    public float enemyBaseHealth;
    public float enemyMaxHealth;
    public void SetLevel(int enemyLevel)
    {
        this.enemyLevel = enemyLevel;
    }
    public void SetName(string enemyName)
    {
        this.enemyName = enemyName;
    }
    public void SetDamage(float enemyDamage)
    {
        this.enemyDamage = enemyDamage;
    }
    public void SetMaxHealth(float enemyMaxHealth)
    {
        this.enemyMaxHealth = enemyMaxHealth;
    }
}
