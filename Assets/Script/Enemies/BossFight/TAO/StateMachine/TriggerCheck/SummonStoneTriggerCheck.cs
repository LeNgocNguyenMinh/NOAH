using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonStoneTriggerCheck : MonoBehaviour
{
    [SerializeField]private BossInteractive bossInteractive;
    [SerializeField]private Animator animator;
    private bool isActive = false;
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag.Contains("Bullet") && !isActive) 
        {
            isActive = true;
            animator.SetTrigger("Active");
            bossInteractive.WakeBoss();
        }
    }
}
