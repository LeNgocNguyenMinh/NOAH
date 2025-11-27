using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDrop : MonoBehaviour
{
    private float delayBeforeMoving = 2f; //Delay time after dead and before go to player
    private float moveSpeed = 20f; //Speed of exp point   
    private Rigidbody2D rb;             
    private bool hasStartedMoving = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        Invoke("StartMoving", delayBeforeMoving); 
    }
    void StartMoving()
    {
        hasStartedMoving = true;
    }

    void Update()
    {
        if (hasStartedMoving)
        {

            Vector2 direction = (Player.Instance.transform.position - transform.position).normalized;//Find Vector from expSpawn Point to Player

            rb.linearVelocity = direction * moveSpeed; //Start to move to player
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag=="PlayerHitCollider")
        {
            SoundControl.Instance.CoinCollectPlay();
            CoinControl.Instance.AddCoin(2);
            Destroy(gameObject);
        }
    }
}
