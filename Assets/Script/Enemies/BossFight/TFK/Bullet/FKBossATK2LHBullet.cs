using System.Collections;
using UnityEngine;

public class FKBossATK2LHBullet : MonoBehaviour
{
    private Vector3 direct;
    private float damage;
    public void SetInitValue(float damage)
    {
        this.damage = damage;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("PlayerHitCollider"))
        {
            PlayerHealthControl.Instance.PlayerHurt(1f);
            Vector3 hitDirect = (Player.Instance.transform.position - transform.position).normalized;
            PlayerEffect.Instance.PushBack(hitDirect);
            PlayerEffect.Instance.HitFlash();
        }
    }
    public void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
