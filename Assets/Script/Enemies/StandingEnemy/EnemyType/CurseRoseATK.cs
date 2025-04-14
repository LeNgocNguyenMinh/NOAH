using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurseRoseATK : MonoBehaviour
{
    [SerializeField]private Transform[] bulletSpawnList;
    [SerializeField]private GameObject[] bulletPrefab;
    public void Attack()
    {
        for(int i = 0; i < bulletSpawnList.Length; i++)
        {
            Instantiate(bulletPrefab[i], bulletSpawnList[i].position, Quaternion.identity);
        }
    }
}
