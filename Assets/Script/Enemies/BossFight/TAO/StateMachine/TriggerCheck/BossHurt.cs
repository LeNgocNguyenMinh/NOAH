using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHurt : MonoBehaviour
{
    [SerializeField] private BossHealthControl healthControl;
    private enum CurrentBoss{
        AOBoss,
        FKBoss
    }
    [SerializeField] private CurrentBoss currentBoss;
    [SerializeField] private Color color;
    [SerializeField] private Transform dmgPopUpTrans;
    [SerializeField] private GameObject damagePopUpPref;
    [SerializeField] private EnemyHitEffect enemyHitEffect;
    public void DamageReceive(float damage, Vector3 direction = default)
    {
        Debug.Log("Receive: " + damage);
        int offSet = Random.Range(-1, 1);
        Vector3 spawnPos = dmgPopUpTrans.position + new Vector3(offSet, 0, 0);
        DamagePopUp damagePopUp = Instantiate(damagePopUpPref, spawnPos, Quaternion.identity).GetComponent<DamagePopUp>();
        if((currentBoss == CurrentBoss.AOBoss && AOBoss.Instance.BossIsAwake)||(currentBoss == CurrentBoss.FKBoss && FKBoss.Instance.BossIsAwake))
        {
            if(enemyHitEffect != null && direction != default)
            {
                enemyHitEffect.Flash();
                enemyHitEffect.Splash(transform.position, (Vector2) direction);
            }
            damagePopUp.ShowDamage(damage);
            healthControl.BossHurt(damage);
            return;
        }
        damagePopUp.ShowDamage(0);
    }
    public void BossDeath()
    {
        if(currentBoss == CurrentBoss.AOBoss)
            AOBoss.Instance.BossDeath();
        else if(currentBoss == CurrentBoss.FKBoss)
            FKBoss.Instance.BossDeath();
    }
}
