using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemyAttackDistanceCheck : MonoBehaviour
{
    [SerializeField] private MovingEnemy enemy;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            enemy.SetIsInAttackRange(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            enemy.SetIsInAttackRange(false);
        }
    }
}
