using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBridge : MonoBehaviour
{
    private Animator animator;
    private BoxCollider2D boxCollider;
    void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "WoodBullet")
        {
            boxCollider.isTrigger = true;
            animator.SetTrigger("isHit");
        }
    }
}
