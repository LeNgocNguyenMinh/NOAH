using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCControl : MonoBehaviour
{
    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer mySpriteRender;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        mySpriteRender = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<PlayerControl>().transform;
    }  
    public void OnLoadGameRunTime()
    {
        player = FindObjectOfType<PlayerControl>().transform;
    }
    private void Update()
    {
        if(player == null) return;
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if(distanceToPlayer<3)
        {
            FacingDirection();
        }
    }    
    private void FacingDirection()
    {
        if (transform.position.x < player.transform.position.x && transform.localScale.x < 0)
        {
            Flip();
        } 
        else if (transform.position.x > player.transform.position.x && transform.localScale.x > 0)
        {
            Flip();
        }
    }
    private void Flip()
    {
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
