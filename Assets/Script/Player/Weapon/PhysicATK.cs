using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PhysicATK : MonoBehaviour
{
    [SerializeField] private GameObject hitParticlePrefab;
    
    private float damageAmount = 1;
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if(hitInfo.tag == "Enemy")
        {
            EnemyHurt enemy = hitInfo.GetComponent<EnemyHurt>();
            Destroy(Instantiate(hitParticlePrefab, hitInfo.ClosestPoint(transform.position), Quaternion.identity), .35f);
            WeaponParent.Instance.PhysicHitAnim();
            WeaponParent.Instance.HitCountIncrease();
            enemy.HitByBullet(damageAmount);//Enemy hurt
        }
    }
}
