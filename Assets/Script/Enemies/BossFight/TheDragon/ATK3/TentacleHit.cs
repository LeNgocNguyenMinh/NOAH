using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleHit : MonoBehaviour
{
    private HealthControl playerHealthControl;
    [SerializeField] private BossStatus bossStatus;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("PlayerHitCollider"))
        {
            playerHealthControl = GameObject.FindObjectOfType<PlayerControl>().GetComponent<HealthControl>();
            playerHealthControl.PlayerHurt(bossStatus.bossDamage);
        }
    }
}
