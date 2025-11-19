using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeHit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if(hitInfo.tag == "Enemy")
        {
            EnemyHurt enemy = hitInfo.GetComponent<EnemyHurt>();
            PlayerMagazine.Instance.HitCountIncrease();
            enemy.DamageReceive(1);//Enemy hurt
        }
        else if(hitInfo.tag == "Boss")
        {
            BossHurt bossHurt = hitInfo.GetComponent<BossHurt>();
            PlayerMagazine.Instance.HitCountIncrease();
            bossHurt.DamageReceive(1);
        }
    }
}
