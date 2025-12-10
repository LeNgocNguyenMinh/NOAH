using UnityEngine;

public class YSlimeATKHit : MonoBehaviour
{
    [SerializeField]private Transform enemyTrans;
    private Vector2 direct;
    [SerializeField]private EnemyStatusInfo enemyStatus;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("PlayerHitCollider"))
        {
            direct = (Player.Instance.transform.position - enemyTrans.position).normalized;
            PlayerHealthControl.Instance.PlayerHurt(enemyStatus.GetEnemyDamage());
            PlayerEffect.Instance.PushBack(direct);
            PlayerEffect.Instance.HitFlash();
        }
    }
}
