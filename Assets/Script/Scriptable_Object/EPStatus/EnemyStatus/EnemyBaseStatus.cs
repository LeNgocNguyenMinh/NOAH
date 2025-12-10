using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyStatus", menuName = "EnemyStatus")]
public class EnemyBaseStatus : ScriptableObject
{
    public float enemyBaseDamage;
    public float enemyBaseMaxHealth;
}
