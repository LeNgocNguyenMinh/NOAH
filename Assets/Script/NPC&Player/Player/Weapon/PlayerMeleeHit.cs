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
            PlayerWeaponParent.Instance.HitCountIncrease();
            enemy.DamageReceive(1);//Enemy hurt
        }
    }
}
