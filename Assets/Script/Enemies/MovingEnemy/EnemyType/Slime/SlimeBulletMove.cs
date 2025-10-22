using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlimeBulletMove : MonoBehaviour
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
        if(timeCount <= 0 && !bulletBreak)
        {
            rb.linearVelocity = Vector2.zero;
            bulletBreak = true;
            animator.SetTrigger("break");
            return;
        }
        if(bulletBreak)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }
        direction = (Player.Instance.transform.position - transform.position).normalized;
        rb.linearVelocity = direction * speed;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("PlayerHitCollider"))
        {
            collider.GetComponent<PlayerEffect>().PushBack(direction);
            collider.GetComponent<PlayerEffect>().HitFlash();   
            HealthControl.Instance.PlayerHurt(enemyStatus.enemyDamage);
            rb.linearVelocity = Vector2.zero;
            bulletBreak = true;
            animator.SetTrigger("break");
        }
    }
    public void BulletBreak()
    {
        Debug.Log("Break");
        bulletBreak = true;
        Destroy(gameObject);
    }
}
