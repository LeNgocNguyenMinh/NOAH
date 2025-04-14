using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBulletMove : MonoBehaviour
{
    [SerializeField]private EnemyStatus enemyStatus;
    private Transform playerTransform;
    private HealthControl playerHealth;
    private Vector3 direction;
    private bool bulletBreak = false;
    [SerializeField]private float chaseTime = 0.0f;
    [SerializeField]private float speed = 5.0f;
    private float timeCount;
    private Rigidbody2D rb;
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerTransform = FindObjectOfType<HealthControl>().transform;
        timeCount = chaseTime;
        playerHealth = FindObjectOfType<HealthControl>().GetComponent<HealthControl>();
    }
    public void FixedUpdate()
    {
        timeCount -= Time.deltaTime;
        if(timeCount <= 0 && !bulletBreak)
        {
            rb.velocity = Vector2.zero;
            bulletBreak = true;
            animator.SetTrigger("break");
            return;
        }
        if(bulletBreak)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        direction = (playerTransform.position - transform.position).normalized;
        rb.velocity = direction * speed;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("PlayerHitCollider"))
        {
            playerHealth.PlayerHurt(enemyStatus.enemyDamage);
            rb.velocity = Vector2.zero;
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
