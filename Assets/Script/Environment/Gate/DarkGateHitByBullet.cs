using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkGateHitByBullet : MonoBehaviour
{
    private Animator animator;
    private bool isHit = false;
    public void GateDestroy()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("LightBullet") && !isHit)
        {
            isHit = true;
            animator = GetComponent<Animator>();
            animator.SetTrigger("isHit");
        }
    }
}
