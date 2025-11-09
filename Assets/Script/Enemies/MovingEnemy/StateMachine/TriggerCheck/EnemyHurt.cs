using UnityEngine;

public class EnemyHurt : MonoBehaviour
{
    [SerializeField] private EnemyHealthControl healthControl;
    [SerializeField] private Transform dmgPopUpTrans;
    [SerializeField] private GameObject damagePopUpPref;
    [SerializeField]private EnemyHitEffect enemyHitEffect;
    public void DamageReceive(float damage, Vector2 direction = default)
    {
        if(enemyHitEffect != null && direction != default)
        {
            Debug.Log("dfsfnsjdkhfsdbjfsdgfsdgfkjsdfkjsdh");
            enemyHitEffect.Flash();
            enemyHitEffect.Splash( transform.position, direction);
        }
        int offSet = Random.Range(-1, 1);
        Vector3 spawnPos = dmgPopUpTrans.position + new Vector3(offSet, 0, 0);
        DamagePopUp damagePopUp = Instantiate(damagePopUpPref, spawnPos, Quaternion.identity).GetComponent<DamagePopUp>();
        damagePopUp.ShowDamage(damage);
        healthControl.EnemyHurt(damage);
    }
}
