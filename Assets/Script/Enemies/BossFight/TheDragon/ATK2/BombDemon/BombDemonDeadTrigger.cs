using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDemonDeadTrigger : MonoBehaviour
{
    private HealthControl playerHealthControl;
    [SerializeField] private BossStatus bossStatus;
    [SerializeField]private Animator animator;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == ("PlayerHitCollider") || collider.tag == ("ForeGround") || collider.tag == ("BombDemonMini"))
        {
            if(collider.tag == "PlayerHitCollider")
            {
                playerHealthControl = GameObject.FindObjectOfType<PlayerControl>().GetComponent<HealthControl>();
                playerHealthControl.PlayerHurt(bossStatus.bossDamage);
            }
            animator.SetTrigger("isDead");
        }
    }
}
