using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAODropAfterDead : MonoBehaviour
{
    [SerializeField]private GameObject weaponDrop;
    [SerializeField]private GameObject gemDrop;
    [SerializeField]private GameObject expPrefab;
    [SerializeField]private GameObject coinPrefab;
    public void DropOEC(Vector3 pos)
    {
        Vector3 gemOffset = new Vector3(2, 0, 2);
        Instantiate(gemDrop, transform.position + gemOffset, Quaternion.identity);
        Instantiate(weaponDrop, transform.position, Quaternion.identity);
        int expPoint = 15;
        int coinPoint = 30;
        for (int i =0; i < expPoint; i++)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
            Vector3 spawnPosition = pos + randomOffset;
            Instantiate(expPrefab, spawnPosition, Quaternion.identity);
        }
        for (int i =0; i < coinPoint; i++)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-2f, 2f));
            Vector3 spawnPosition = pos + randomOffset;
            Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
