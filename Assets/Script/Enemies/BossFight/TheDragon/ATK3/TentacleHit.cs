using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleHit : MonoBehaviour
{
    [SerializeField] private BossStatus bossStatus;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("PlayerHitCollider"))
        {
            HealthControl.Instance.PlayerHurt(bossStatus.bossDamage);
        }
    }
}
