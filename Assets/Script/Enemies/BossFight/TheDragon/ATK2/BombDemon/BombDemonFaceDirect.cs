using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDemonFaceDirect : MonoBehaviour
{
    private Transform playerTransform;
    void Update()
    {
        FaceDirect();
    }
    private void FaceDirect()
    {
        playerTransform = FindObjectOfType<PlayerControl>().transform;
        if(playerTransform.position.x > transform.position.x && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(transform.localScale.x *-1, transform.localScale.y, transform.localScale.z);
        }
        else if(playerTransform.position.x < transform.position.x && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(transform.localScale.x *-1, transform.localScale.y, transform.localScale.z);
        }
    }
}
