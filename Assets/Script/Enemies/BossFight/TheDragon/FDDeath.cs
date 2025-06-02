using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDDeath : MonoBehaviour
{
    [SerializeField]private Transform weaponInsTrans;
    [SerializeField]private GameObject weapon;
    [SerializeField]private GameObject fallenDragon;
    [SerializeField]private GameObject gemDrop;
    [SerializeField]private GameObject expPrefab;
    [SerializeField]private GameObject coinPrefab;
    public void WPInstantiate()
    {
        Instantiate(weapon, weaponInsTrans.position, Quaternion.identity);
    }
    public void DropOEC()
    {
        WPInstantiate();
        Vector3 gemOffset = new Vector3(2, 0, 2);
        Instantiate(gemDrop, weaponInsTrans.position + gemOffset, Quaternion.identity);
        int expPoint = 15;
        int coinPoint = 30;
        for (int i =0; i < expPoint; i++)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
            Vector3 spawnPosition = weaponInsTrans.position + randomOffset;
            Instantiate(expPrefab, spawnPosition, Quaternion.identity);
        }
        for (int i =0; i < coinPoint; i++)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-2f, 2f));
            Vector3 spawnPosition = weaponInsTrans.position + randomOffset;
            Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
        }
    }
    public void DestroyAfterDeath()
    {
        Destroy(fallenDragon);
    }
   
}
