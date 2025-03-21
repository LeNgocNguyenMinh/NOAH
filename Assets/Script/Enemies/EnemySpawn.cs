using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefab;
 
    [SerializeField] private float spawnTime;
    public int enemyNumberOnField = 0;
    public int maxEnemyOnField;
    private float timeUntilSpawn;
    void Awake()
    {
        SetTimeUntilSpawn();
    }
    void Update()
    {
        if(enemyNumberOnField == maxEnemyOnField)return;
        timeUntilSpawn -= Time.deltaTime;
        if(timeUntilSpawn <= 0)
        {
            int randomIndex = Random.Range(0, enemyPrefab.Length);
            Instantiate(enemyPrefab[randomIndex], transform.position, Quaternion.identity);
            enemyNumberOnField++;
            SetTimeUntilSpawn();
        }
    }
    private void SetTimeUntilSpawn()
    {
        timeUntilSpawn = spawnTime;
    }
    
}
