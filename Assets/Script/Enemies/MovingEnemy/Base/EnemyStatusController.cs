using UnityEngine;
using TMPro;


public class EnemyStatusInfo : MonoBehaviour
{
    private int enemyLevel;
    private float enemyMaxHealth;
    private float enemyDamage;
    [SerializeField]private EnemyBaseStatus enemyBaseStatus;
    [SerializeField]private EnemyHealthControl enemyHealthControl;
    [SerializeField]private TextMeshProUGUI levelText;
    private void Start()
    {
        enemyLevel = 1;
        enemyMaxHealth = enemyBaseStatus.enemyBaseMaxHealth;
        enemyDamage = enemyBaseStatus.enemyBaseDamage;
        if(PlayerStatus.Instance.playerLevel % 3 == 0)//Mean only when player level is 3, 6, 9
        {
            enemyLevel = PlayerStatus.Instance.playerLevel / 2;//then enemy level is 1, 3, 4, ...
            //then damage, maxHealth calculate to fit with the level 
            enemyMaxHealth = enemyBaseStatus.enemyBaseMaxHealth * (1 + 0.4f * enemyLevel);
        }
        enemyDamage = enemyBaseStatus.enemyBaseDamage + enemyLevel;
        levelText.text = enemyLevel + "";
        enemyHealthControl.SetMaxHealth(enemyMaxHealth);
    }
    public float GetEnemyDamage()
    {
        return enemyDamage;
    }
}
