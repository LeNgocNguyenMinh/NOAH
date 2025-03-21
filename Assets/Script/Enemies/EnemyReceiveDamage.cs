using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReceiveDamage : MonoBehaviour
{
    [SerializeField]private BossHealthControl bossHealthControl;
    public void ReceiveDamage(float newValue)
    {
        bossHealthControl.BossHurt(newValue);
    }
}
