using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHurt : MonoBehaviour
{
    [SerializeField] private BossHealthControl healthControl;
    [SerializeField] private AOBoss aoBoss;
    [SerializeField] private Color color;
    [SerializeField] private Transform dmgPopUpTrans;
    [SerializeField] private GameObject damagePopUpPref;
    [SerializeField] private EnemyHitEffect enemyHitEffect;
    public void DamageReceive(float damage, Vector3 direction = default)
    {
        int offSet = Random.Range(-1, 1);
        Vector3 spawnPos = dmgPopUpTrans.position + new Vector3(offSet, 0, 0);
        DamagePopUp damagePopUp = Instantiate(damagePopUpPref, spawnPos, Quaternion.identity).GetComponent<DamagePopUp>();
        if(aoBoss != null && aoBoss.BossIsAwake)
        {
            if(enemyHitEffect != null && direction != default)
            {
                enemyHitEffect.Flash(color);
                enemyHitEffect.SplashAfterEffectInit( transform.position, (Vector2) direction);
            }
            damagePopUp.ShowDamage(damage);
            healthControl.BossHurt(damage);
        }
        damagePopUp.ShowDamage(0);
    }
}
