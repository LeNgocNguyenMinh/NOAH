using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    public float rotationSpeed;
    public float distance;
    private void Start()
    {
        Physics2D.queriesStartInColliders = false;
    }
    void Update()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, distance);    
        if(hitInfo.collider!= null)
        {
            Debug.Log("2" + hitInfo);
            Debug.DrawRay(transform.position, transform.right*distance, Color.red);
        }
        else
        {
            Debug.Log("1" + hitInfo);
            Debug.DrawRay(transform.position, transform.position + transform.right*distance, Color.green);
        }
    }
}
