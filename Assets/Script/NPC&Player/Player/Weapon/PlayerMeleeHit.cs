using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeHit : MonoBehaviour
{
    private Vector2 direct;
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if(hitInfo.tag == "Enemy")
        {
            EnemyHurt enemy = hitInfo.GetComponent<EnemyHurt>();
            PlayerMagazine.Instance.HitCountIncrease();
            direct = (hitInfo.transform.position - Player.Instance.transform.position).normalized;
            enemy.DamageReceive(PlayerStatus.Instance.playerCurrentDamage/2, direct);//Enemy hurt
        }
        else if(hitInfo.tag == "Boss")
        {
            BossHurt bossHurt = hitInfo.GetComponent<BossHurt>();
            PlayerMagazine.Instance.HitCountIncrease();
            direct = (hitInfo.transform.position - Player.Instance.transform.position).normalized;
            bossHurt.DamageReceive(PlayerStatus.Instance.playerCurrentDamage/2, direct);
        }
    }
}
