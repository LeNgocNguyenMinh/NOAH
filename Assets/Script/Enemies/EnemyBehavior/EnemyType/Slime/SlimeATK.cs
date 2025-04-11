using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeATK : MonoBehaviour
{
    [SerializeField]private GameObject slimeBullet;
    [SerializeField]private Transform bulletSpawnPoint;
    public void Attack()
    {
        Instantiate(slimeBullet, bulletSpawnPoint.position, Quaternion.identity);
    }
}
