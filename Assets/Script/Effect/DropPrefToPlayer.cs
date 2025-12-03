using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
//This class control each EXP point
public class DropPrefToPlayer : MonoBehaviour
{   
    [SerializeField]private float delayBeforeMoving = 2f; //Delay time after dead and before go to player
    [SerializeField]private ProjectileVisual projectileVisual;
    private float moveSpeed = 20f; //Speed of exp point        
    [SerializeField]private Rigidbody2D rb;             
    private bool isCollected = false; 
    [SerializeField]private DropType dropType;
    public enum DropType { EXP, Coin } 

    void Update()
    {
        if(delayBeforeMoving > 0f)
        {
            delayBeforeMoving -= Time.deltaTime;
            return;
        }
        if (!isCollected)
        {
            projectileVisual.enabled = false;
            Vector2 direction = (Player.Instance.transform.position - transform.position).normalized;//Find Vector from expSpawn Point to Player

            rb.linearVelocity = direction * moveSpeed; //Start to move to player
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("PlayerHitCollider"))
        {
            if(dropType == DropType.EXP)
            {
                SoundControl.Instance.EXPCollectPlay();
                EXPControl.Instance.AddEXP(2f);
                Destroy(gameObject);
            }   
            else if(dropType == DropType.Coin)
            {
                SoundControl.Instance.CoinCollectPlay();
                CoinControl.Instance.AddCoin(2);
                Destroy(gameObject);
            }
        }
    }

}
