using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStyle2 : MonoBehaviour
{
    [SerializeField]private BossStatus bossStatus;
    [SerializeField] float moveSpeed = 5f; // Tốc độ di chuyển
    private Rigidbody2D rb;
    private BossAOShoot bossShoot;
    private int direction = 1; // 1: phải, -1: trái
    private void Update()
    {
        rb = GetComponent<Rigidbody2D>();
        if(bossStatus.bossCurrentHealth <=0)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("ForeGround"))
        {
            Debug.Log("có");
            direction *= -1; // Đảo ngược hướng
        }
    }
}
