using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackStyle2 : MonoBehaviour
{
    [SerializeField]private float timeBetweenShoot;
    [SerializeField]private GameObject bulletPrefab;
    [SerializeField] private float maxBulletDistance;
    [SerializeField] private int maxBullet;
    private int bulletCount = 0;
    [SerializeField]private Transform leftHand;
    [SerializeField]private Transform rightHand;

    public void ShootBulletDown()
    {
        if(bulletCount > maxBullet)return;
        BB02Control bullet1 = Instantiate(bulletPrefab, leftHand.position, Quaternion.identity).GetComponent<BB02Control>();
        bullet1.SetMaxDistance(maxBulletDistance);
        BB02Control bullet2 = Instantiate(bulletPrefab, rightHand.position, Quaternion.identity).GetComponent<BB02Control>();
        bullet2.SetMaxDistance(maxBulletDistance);
        bulletCount++;
    }
    public bool CheckATKFinish()
    {
        if(bulletCount < maxBullet)
        {
            return false;
        }
        bulletCount = 0;
        return true;
    }
}
