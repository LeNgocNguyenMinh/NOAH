using UnityEngine;

public class AOBossATK1RHShock : MonoBehaviour
{
    private Vector3 direct;
    private float damage;
    private bool isHit;
    

    public void SetValue(float damage)
    {
        this.damage = damage;
        isHit = false;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("PlayerHitCollider") && !isHit)
        {
            isHit = true;
            direct = (Player.Instance.transform.position - transform.position).normalized;
            PlayerHealthControl.Instance.PlayerHurt(damage);
            PlayerEffect.Instance.PushBack(direct);
            PlayerEffect.Instance.HitFlash();
        }
    }
    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
