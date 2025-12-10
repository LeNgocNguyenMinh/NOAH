using UnityEngine;

public class FKBossATK2RHBullet : MonoBehaviour
{
    [SerializeField]private Projectile projectile;
    [SerializeField]private Animator animator;
    private float damage;
    private Vector3 target;
    private bool isFly = true;
    
    public void SetValue(float maxSpeed, float maxHeight, float damage)
    {
        target = Player.Instance.transform.position;
        this.damage = damage;
        projectile.InitializeProjectile(target, maxSpeed, maxHeight);
    }
    public void Update()
    {
        if(projectile.isStop && isFly)
        {
            isFly = false;
            animator.SetTrigger("Explode");
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("PlayerHitCollider"))
        {
            PlayerHealthControl.Instance.PlayerHurt(damage);
            Vector3 hitDirect = (Player.Instance.transform.position - transform.position).normalized;
            PlayerEffect.Instance.PushBack(hitDirect);
            PlayerEffect.Instance.HitFlash();
            animator.SetTrigger("Explode");
        }
    }
    public void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
