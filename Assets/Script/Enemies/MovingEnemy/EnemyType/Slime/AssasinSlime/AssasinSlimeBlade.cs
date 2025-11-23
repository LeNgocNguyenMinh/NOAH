using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AssasinSlimeBlade : MonoBehaviour
{
    private Vector2 direction = new(1, 0) ;
    [SerializeField]private Transform bladeScale;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("PlayerHitCollider"))
        {
            if(bladeScale.localScale.x < 0)
            {
                direction = new(-1, 0);
            }
            collider.GetComponent<PlayerEffect>().PushBack(direction);
            collider.GetComponent<PlayerEffect>().HitFlash();   
            PlayerHealthControl.Instance.PlayerHurt(1);
        }
    }
}
    