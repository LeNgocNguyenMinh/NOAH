using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlueSlimeBullet : MonoBehaviour
{
    [SerializeField]private EnemyStatus enemyStatus;
    [SerializeField]private Rigidbody2D rb;
    [SerializeField]private Animator animator;
    private Vector2 direction;
    private bool bulletBreak = false;
    private float speed = 5.0f;
    private float timeCount;
    
    public void SetInitValue(float speed, float chaseTime)
    {
        this.speed = speed;
        timeCount = chaseTime;
        bulletBreak = false;
    }
    public void FixedUpdate()
    {
        timeCount -= Time.deltaTime;
        if(bulletBreak)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }
        if(timeCount <= 0 && !bulletBreak)
        {
            bulletBreak = true;
            animator.SetTrigger("break");
            return;
        }
        direction = (Player.Instance.transform.position - transform.position).normalized;
        rb.linearVelocity = direction * speed;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("PlayerHitCollider") && !bulletBreak)
        {
            collider.GetComponent<PlayerEffect>().PushBack(direction);
            collider.GetComponent<PlayerEffect>().HitFlash();   
            HealthControl.Instance.PlayerHurt(1);
            bulletBreak = true;
            animator.SetTrigger("break");
        }
    }
    public void BulletDestroy()
    {
        Destroy(gameObject);
    }
}
