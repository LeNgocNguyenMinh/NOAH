using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurt : MonoBehaviour
{
    [SerializeField] private EnemyHealthControl healthControl;
    public void DamageReceive(float damage)
    {
        healthControl.EnemyHurt(damage);
    }
}
