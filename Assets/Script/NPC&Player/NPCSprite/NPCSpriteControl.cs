using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpriteControl : MonoBehaviour
{
    [SerializeField]private ObjectInteraction objectInteraction;
    private void Update()
    {
        if(objectInteraction.GetCanInteract())
        {
            FacingDirection();
        }
    }
    private void FacingDirection()
    {
        if (transform.position.x < Player.Instance.transform.position.x && transform.localScale.x < 0)
        {
            Flip();
        } 
        else if (transform.position.x > Player.Instance.transform.position.x && transform.localScale.x > 0)
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
