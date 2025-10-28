using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOBossAttackPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("PlayerHitCollider"))
        {
            HealthControl.Instance.PlayerHurt(10);
            PlayerEffect.Instance.PushBack(new Vector2(-1,0));
            PlayerEffect.Instance.HitFlash();
        }
    }
}
