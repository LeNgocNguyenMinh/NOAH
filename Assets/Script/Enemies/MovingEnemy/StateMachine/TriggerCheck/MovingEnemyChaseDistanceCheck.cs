using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemyChaseDistanceCheck : MonoBehaviour
{
    [SerializeField] private MovingEnemy enemy;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            enemy.SetIsInChaseRange(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            enemy.SetIsInChaseRange(false);
        }
    }
}
