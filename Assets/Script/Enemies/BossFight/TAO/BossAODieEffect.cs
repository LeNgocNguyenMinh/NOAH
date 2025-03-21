using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAODieEffect : MonoBehaviour
{
    [SerializeField]private GameObject summonCircle;
    public void Die()
    {
        Debug.Log("Dead");
        Destroy(gameObject);
    }
    public void DestroySummonCircle()
    {
        Destroy(summonCircle);
    }
}
