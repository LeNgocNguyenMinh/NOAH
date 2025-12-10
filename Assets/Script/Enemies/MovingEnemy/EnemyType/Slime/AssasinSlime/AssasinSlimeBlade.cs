using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AssasinSlimeBlade : MonoBehaviour
{
    private Vector2 direction = new(1, 0) ;
    private float enemyDamage;
    [SerializeField]private Transform bladeScale;
    public void SetInitValue(float enemyDamage)
    {
        this.enemyDamage = enemyDamage;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("PlayerHitCollider"))
        {
            if(bladeScale.localScale.x < 0)
            {
                direction = new(-1, 0);
            }
            PlayerEffect.Instance.PushBack(direction);
            PlayerEffect.Instance.HitFlash();   
            PlayerHealthControl.Instance.PlayerHurt(enemyDamage);
        }
    }
}
    