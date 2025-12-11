using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOBossAttackPoint : MonoBehaviour
{
    [SerializeField]private BossStatusController bossStatusController;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("PlayerHitCollider"))
        {
            PlayerHealthControl.Instance.PlayerHurt(bossStatusController.GetBossDamage());
            PlayerEffect.Instance.PushBack(new Vector2(-1,0));
            PlayerEffect.Instance.HitFlash();
        }
    }
}
