using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGateHitByBullet : MonoBehaviour
{
    private bool isBurn = false;
    private Animator animator;
    public void BurnToDust()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("FireBullet") && !isBurn)
        {
            isBurn = true;
            animator = GetComponent<Animator>();
            animator.SetTrigger("isBurn");
        }
    }
}
