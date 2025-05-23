using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteControl : MonoBehaviour
{
    private Vector3 mousePos;
    void Update()
    {
        if(!UIMouseAndPriority.Instance.CanOpenThisUI())return;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        FacingDirection();
    }
    private void FacingDirection()
    {
        
        if (mousePos.x < transform.position.x && transform.localScale.x > 0) {
            transform.localScale = new Vector3(transform.localScale.x *-1, transform.localScale.y, transform.localScale.z);
        } else if (mousePos.x > transform.position.x && transform.localScale.x < 0) 
        {
            transform.localScale = new Vector3(transform.localScale.x *-1, transform.localScale.y, transform.localScale.z);
        }
    }
}
