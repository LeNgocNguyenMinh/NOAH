using UnityEngine;

public class AOBossATK1RHBullet : MonoBehaviour
{
    private Vector3 direct;
    private float speed;
    private Animator animator;
    private Rigidbody2D rb;
    private float flyTime;
    private bool startFly = false;
    private bool breaking = false;
    public void SetValue(Vector3 direct, float speed)
    {
        this.direct = direct;
        this.speed = speed;
        Shoot();
    }
    public void Update()
    {
        if(startFly)
        {
            rb.velocity = direct * speed;
        } 
        if(breaking)
        {
            rb.velocity = Vector2.zero;
        }
    }
    public void Shoot()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        float angle = Mathf.Atan2(direct.y, direct.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 180f);
        animator.SetTrigger("Fly");
        startFly = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        animator = GetComponent<Animator>();
        if(collision.CompareTag("PlayerHitCollider") && !breaking)
        {
            breaking = true;
            animator.SetTrigger("Break");
        }
        if(collision.CompareTag("ForeGround") && !breaking)
        {
            breaking = true;
            animator.SetTrigger("Break");
        }
    }
    public void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
