using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBossStatus", menuName = "BossStatus")]
public class BossStatus : ScriptableObject
{
    public string bossName;
    public string bossID;
    public float bossBaseDamage;
    public float bossMaxHealth;
    public GameObject bossPrefab;
  
    public void SetName(string newValue)
    {
        this.bossName = newValue;
    }
}
