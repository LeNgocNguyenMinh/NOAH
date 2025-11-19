using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueSlimeATK : MonoBehaviour
{
    [SerializeField]private float bulletSpeed;
    [SerializeField]private float bulletChaseTime;
    [SerializeField]private GameObject slimeBullet;
    [SerializeField]private Transform bulletSpawnPoint;
    public void Attack()
    {
        Instantiate(slimeBullet, bulletSpawnPoint.position, Quaternion.identity).GetComponent<BlueSlimeBullet>().SetInitValue(bulletSpeed, bulletChaseTime);
    }
}
    