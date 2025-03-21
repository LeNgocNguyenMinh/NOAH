using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallShotATK1 : MonoBehaviour
{
    private HealthControl playerHealthControl;
    [SerializeField] private BossStatus bossStatus;
    private float speed; 
    private Transform playerTransform;
    private Rigidbody2D rb;
    private Vector3 direct;
    private Animator animator;
    private bool isHit = false;
    private void Update()
    {
        if(isHit)return;
        if(speed != 0f)
        {
            rb = GetComponent<Rigidbody2D>();
            rb.velocity = direct * speed;
            Debug.Log("Shoot");
        } 
    }
    public void SetShootSpeed(float newValue)
    {
        playerTransform = FindObjectOfType<PlayerControl>().transform;
        direct = (playerTransform.position - transform.position).normalized;
        speed = newValue;
    }
    public void SmallShotDestroy()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "PlayerHitCollider" || collider.tag == "ForeGround")
        {
            if(!isHit)
            {
                rb = GetComponent<Rigidbody2D>();
                rb.velocity = Vector3.zero; 

                if(collider.tag == "PlayerHitCollider")
                {
                    playerHealthControl = GameObject.FindObjectOfType<PlayerControl>().GetComponent<HealthControl>();
                    playerHealthControl.PlayerHurt(bossStatus.bossDamage/4f);
                }

                animator = GetComponent<Animator>();
                animator.SetTrigger("smallShotBreak");
                isHit = true;
            }
        }
    }
}
