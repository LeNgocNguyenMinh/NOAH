using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBossStatus", menuName = "BossStatus")]
public class BossStatus : ScriptableObject
{
    public int bossLevel;
    public string bossName;
    public float bossBaseDamage;
    public float bossDamage;
    public float bossBaseHealth;
    public float bossCurrentHealth;
    public float bossMaxHealth;
    public bool isAlive;
    public void SetLevel(int newValue)
    {
        this.bossLevel = newValue;
    }
    public void SetName(string newValue)
    {
        this.bossName = newValue;
    }
    public void SetDamage(float newValue)
    {
        this.bossDamage = newValue;
    }
    public void SetMaxHealth(float newValue)
    {
        this.bossMaxHealth = newValue;
    }
    public void SetCurrentHealth(float newValue)
    {
        this.bossCurrentHealth = newValue;
    }
    public void SetIsAlive(bool newValue)
    {
        this.isAlive = newValue;
    }
}
