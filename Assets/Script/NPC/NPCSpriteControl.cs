using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpriteControl : MonoBehaviour
{
    private Transform player;
    private Animator animator;
    private SpriteRenderer mySpriteRender;

    private void Update()
    {
        player = FindObjectOfType<PlayerControl>()?.transform;
        if(player == null) return;
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if(distanceToPlayer<3)
        {
            FacingDirection();
        }
    }    
    private void FacingDirection()
    {
        player = FindObjectOfType<PlayerControl>().transform;
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
