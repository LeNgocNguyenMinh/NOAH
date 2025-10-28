using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonStone : MonoBehaviour
{
    [SerializeField]private Animator animator;
    private enum LinkToBoss{
        AOBoss,
        FKBoss
    }
    [SerializeField]private LinkToBoss linkToBoss;
    private bool isActive = false;
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag.Contains("Bullet") && !isActive) 
        {
            isActive = true;
            animator.SetTrigger("Active");
            WakeBoss();
        }
    }
    
    public void WakeBoss()
    {
        if(linkToBoss == LinkToBoss.AOBoss)
            AOBoss.Instance.BossAwake();
        else if(linkToBoss == LinkToBoss.FKBoss)
            FKBoss.Instance.BossAwake();
    }
}
