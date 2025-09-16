using UnityEngine;

public class AOBossATK2LHBullet : MonoBehaviour
{
    private Vector3 direct;
    private float speed;
    private Animator animator;
    private Rigidbody2D rb;
    private int flyTime;
    public void SetValue(Vector3 direct, float speed, int flyTime)
    {
        this.direct = direct;
        this.speed = speed;
        this.flyTime = flyTime;
        rb = GetComponent<Rigidbody2D>();
        Shoot();
    }
    public void Shoot()
    {
        rb.velocity = direct * speed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        flyTime--;
        if(flyTime < 0)
        {
            Destroy(gameObject);
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
