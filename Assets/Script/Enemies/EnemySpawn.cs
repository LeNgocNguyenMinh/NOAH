using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]private GameObject[] enemyPrefab;
    [SerializeField]private Transform[] spawnPoints;
 
    [SerializeField]private float spawnTime;
    public int enemyNumberOnField = 0;
    public int maxEnemyOnField;
    private float timeUntilSpawn = 0f;

    void Update()
    {
        if(enemyNumberOnField >= maxEnemyOnField)return;
        timeUntilSpawn -= Time.deltaTime;
        if(timeUntilSpawn <= 0)
        {
            int randomIndex = Random.Range(0, enemyPrefab.Length);
            int randomPlace = Random.Range(0, spawnPoints.Length);
            Instantiate(enemyPrefab[randomIndex], spawnPoints[randomPlace].position, Quaternion.identity);
            enemyNumberOnField++;
            SetTimeUntilSpawn();
        }
    }
    private void SetTimeUntilSpawn()
    {
        timeUntilSpawn = spawnTime;
    }
    
}
