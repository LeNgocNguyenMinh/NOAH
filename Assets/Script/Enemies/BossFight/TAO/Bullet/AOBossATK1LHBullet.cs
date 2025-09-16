using UnityEngine;

public class AOBossATK1LHBullet : MonoBehaviour
{
    private Vector3 direct;
    private float speed;
    private Animator animator;
    private Rigidbody2D rb;
    private float flyTime;
    private bool startFly = false;
    private bool breaking = false;
    

    public void SetValue(float speed, float flyTime)
    {
        this.speed = speed;
        this.flyTime = flyTime;
        Shoot();
    }
    public void Update()
    {
        if(startFly)
        {
            flyTime -= Time.deltaTime;
            direct = (Player.Instance.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(direct.y, direct.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle - 180f);
            rb.velocity = direct * speed;
        } 
        if(flyTime <= 0f && !breaking)
        {
            breaking = true;
            startFly = false;
            flyTime = 0f;
            rb.velocity = Vector2.zero;
            animator.SetTrigger("Break");  
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
