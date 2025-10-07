using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBossStatus", menuName = "BossStatus")]
public class BossStatus : ScriptableObject
{
    public int bossLevel;
    public string bossName;
    public string bossID;
    public float bossBaseDamage;
    public float bossDamage;
    public float bossBaseHealth;

    public float bossMaxHealth;
    public PlayerStatus playerStatus;
    public void UpdateLevel()
    {
        this.bossLevel = playerStatus.playerLevel + 2;
    }
    public void SetName(string newValue)
    {
        this.bossName = newValue;
    }
    public void UpdateDamage()
    {
        this.bossDamage = bossBaseDamage;
    }
    public void UpdateMaxHealth()
    {
        this.bossMaxHealth = bossBaseHealth * (1 + 0.5f * (bossLevel - 1));
    }
}
