using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackStyle1 : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab; 
    [SerializeField] private float maxBulletDistance;
    [SerializeField] private int maxBullet;
    private int bulletCount = 0;
    [SerializeField] private Transform atk1ShootTransform;  
    private Transform playerTransform;
    private void Start()
    {
        playerTransform = FindObjectOfType<Player>().transform;
    }
    public void ShootBulletTo()
    {
        if(bulletCount > maxBullet)return;
        Vector3 direction = (playerTransform.transform.position - atk1ShootTransform.position).normalized;
        BB01Control bullet = Instantiate(bulletPrefab, atk1ShootTransform.position, Quaternion.identity).GetComponent<BB01Control>();
        bullet.SetMaxDistance(maxBulletDistance);
        bullet.SetTarget(direction);
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
