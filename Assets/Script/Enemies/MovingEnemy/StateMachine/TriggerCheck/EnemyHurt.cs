using UnityEngine;

public class EnemyHurt : MonoBehaviour
{
    [SerializeField] private EnemyHealthControl healthControl;
    [SerializeField] private Color color;
    [SerializeField] private Transform dmgPopUpTrans;
    [SerializeField] private GameObject damagePopUpPref;
    public void DamageReceive(float damage, Vector3 direction = default)
    {
        EnemyHitEffect enemyHitEffect = GetComponentInParent<EnemyHitEffect>();
        if(enemyHitEffect != null && direction != default)
        {
            Debug.Log("dfsfnsjdkhfsdbjfsdgfsdgfkjsdfkjsdh");
            enemyHitEffect.Flash(color);
            enemyHitEffect.SplashAfterEffectInit( transform.position, (Vector2) direction);
        }
        DamagePopUp damagePopUp = Instantiate(damagePopUpPref, dmgPopUpTrans.position, Quaternion.identity).GetComponent<DamagePopUp>();
        damagePopUp.ShowDamage(damage);
        damagePopUp.ShowDamage(damage);
        healthControl.EnemyHurt(damage);
    }
}
