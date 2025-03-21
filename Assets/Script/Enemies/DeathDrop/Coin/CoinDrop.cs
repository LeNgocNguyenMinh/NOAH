using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDrop : MonoBehaviour
{
    private Transform player;//Player object
    private CoinControl coinControl; // 
    private float delayBeforeMoving = 2f; //Delay time after dead and before go to player
    private float moveSpeed = 20f; //Speed of exp point        

    private Vector3 playerPosition;     //For player position
    private Rigidbody2D rb;             
    private bool hasStartedMoving = false; 
    private SoundControl soundControl;
    void Start()
    {
        soundControl = FindObjectOfType<SoundControl>().GetComponent<SoundControl>();
        rb = GetComponent<Rigidbody2D>(); 
        player = FindObjectOfType<PlayerControl>().transform;
        coinControl = player.GetComponent<CoinControl>();
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
            playerPosition = player.transform.position;

            Vector2 direction = (playerPosition - transform.position).normalized;//Find Vector from expSpawn Point to Player

            rb.velocity = direction * moveSpeed; //Start to move to player
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag=="PlayerHitCollider")
        {
            soundControl.CoinCollectPlay();
            coinControl.AddCoin(2);
            Destroy(gameObject);
        }
    }
}
