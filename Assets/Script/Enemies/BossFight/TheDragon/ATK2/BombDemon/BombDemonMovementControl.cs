using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDemonMovementControl : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool startRun = false;
    private Transform playerTransform;
    private Vector3 direct;
    private bool runStraight = false;
    [SerializeField]private float maxDistance;
    [SerializeField]private float speed;
    void Update()
    {
        if(startRun)
        {
            if(Vector3.Distance(playerTransform.position, transform.position) > maxDistance && !runStraight)
            {
                Debug.Log("Distance: " + Vector3.Distance(playerTransform.position, transform.position));
                direct = (playerTransform.position - transform.position).normalized;
            }
            else
            {
                Debug.Log("reach limit");
                runStraight = true;
            } 
            rb.velocity = direct * speed;
        }
    }
    public void StartToRun()
    {
        playerTransform = FindObjectOfType<Player>().transform;
        rb = GetComponent<Rigidbody2D>();
        direct = (playerTransform.position - transform.position).normalized;
        startRun = true;
    }
    public void StopRun()
    {
        startRun = false;
        rb.velocity = Vector3.zero;
    }
    public void Dead()
    {
        Destroy(gameObject);
    }
}
