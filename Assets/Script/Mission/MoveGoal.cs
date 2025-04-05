using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGoal : MonoBehaviour
{
    public bool reachGoal = false;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            reachGoal = true;
            Debug.Log("Goal Reached!");
        }
    }
}
