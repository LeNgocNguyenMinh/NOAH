using UnityEngine;

public class AOBossATK1LHBullet : MonoBehaviour
{
    private Vector3 direct;
    private float speed;
    private Animator animator;
    private Rigidbody2D rb;
    private float flyTime;
    private bool breaking = false;
    private float damage;
    

    public void SetValue(float speed, float flyTime, float damage)
    {
        this.speed = speed;
        this.flyTime = flyTime;
        this.damage = damage;
        breaking = false;
        Shoot();
    }
    public void Update()
    {
        flyTime -= Time.deltaTime;
        if(!breaking)
        {
            direct = (Player.Instance.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(direct.y, direct.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle - 180f);
            rb.linearVelocity = direct * speed;
        } 
        else{
            rb.linearVelocity = Vector2.zero;
        }
        if(flyTime <= 0f && !breaking)
        {
            breaking = true;
            flyTime = 0f;
            rb.linearVelocity = Vector2.zero;
            animator.SetTrigger("Break");  
        }
    }
    public void Shoot()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        animator.SetTrigger("Fly");
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        animator = GetComponent<Animator>();
        if(collider.CompareTag("PlayerHitCollider") && !breaking)
        {
            breaking = true;
            HealthControl.Instance.PlayerHurt(damage);
            PlayerEffect.Instance.PushBack(direct);
            PlayerEffect.Instance.HitFlash();
            animator.SetTrigger("Break");
        }
    }
    public void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
