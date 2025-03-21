using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This class control each EXP point
public class EXPDrop : MonoBehaviour
{   
    private Transform player;//Player object
    private EXPControl expControl; // 
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
        expControl = player.GetComponent<EXPControl>();
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
            playerPosition = player.transform.position;//Find A point

            Vector2 direction = (playerPosition - transform.position).normalized;//Find Vector from expSpawn Point to Player

            rb.velocity = direction * moveSpeed; //Start to move to player
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag=="PlayerHitCollider")
        {
            soundControl.EXPCollectPlay();
            expControl.AddEXP(2f);
            Destroy(gameObject);
        }
    }

}
