using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOnDie : MonoBehaviour
{
    [SerializeField]private GameObject expPrefab;
    [SerializeField]private GameObject coinPrefab;
    public void DropEXP(Vector3 pos)
    {
        int expPoint = Random.Range(3,5);
        int coinPoint = Random.Range(4,7);
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
