using System.Collections;
using UnityEngine;

public class FKBossATK2LHBullet : MonoBehaviour
{
    private Vector3 direct;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("PlayerHitCollider"))
        {
            HealthControl.Instance.PlayerHurt(1f);
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
