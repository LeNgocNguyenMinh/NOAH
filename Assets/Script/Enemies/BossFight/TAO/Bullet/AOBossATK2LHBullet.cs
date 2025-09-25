using UnityEngine;

public class AOBossATK2LHBullet : MonoBehaviour
{
    private Vector3 direct;
    private float speed;
    private Animator animator;
    private Rigidbody2D rb;
    private int flyTime;
    private bool animTrigger = false;
    private float damage;
    public void SetValue(Vector3 direct, float speed, int flyTime, float damage)
    {
        this.direct = direct;
        this.speed = speed;
        this.flyTime = flyTime;
        this.damage = damage;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Shoot();
    }
    public void Shoot()
    {
        rb.velocity = direct * speed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        flyTime--;
        if(collision.gameObject.tag == "PlayerHitCollider" && !animTrigger)
        {
            PlayerEffect.Instance.PushBack(direct);
            PlayerEffect.Instance.HitFlash();
            animTrigger = true;
            rb.velocity = Vector2.zero;
            HealthControl.Instance.PlayerHurt(damage);
            animator.SetTrigger("Break");
            return;
        }
        if(flyTime < 0 && !animTrigger)
        {
            rb.velocity = Vector2.zero;
            animTrigger = true;
            animator.SetTrigger("Break");
            return;
        }
        var firstContact = collision.contacts[0];
        Vector2 newVelocity = Vector2.Reflect(direct, firstContact.normal);
        direct = newVelocity.normalized;
        Shoot();
    }
    public void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
