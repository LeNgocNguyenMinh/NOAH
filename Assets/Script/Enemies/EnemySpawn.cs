using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]private GameObject[] enemyPrefab;
    [SerializeField]private Transform[] spawnPoints;
    public List<GameObject> listEnemyOnField;
 
    [SerializeField]private float spawnTime;
    public int maxEnemyOnField;
    private float timeUntilSpawn = 0f;

    void Update()
    {
        listEnemyOnField.RemoveAll(item => item == null);
        if(listEnemyOnField.Count >= maxEnemyOnField)return;
        timeUntilSpawn -= Time.deltaTime;
        if(timeUntilSpawn <= 0)
        {
            int randomIndex = Random.Range(0, enemyPrefab.Length);
            int randomPlace = Random.Range(0, spawnPoints.Length);
            GameObject tmp = Instantiate(enemyPrefab[randomIndex], spawnPoints[randomPlace].position, Quaternion.identity);
            listEnemyOnField.Add(tmp);
            timeUntilSpawn = spawnTime;
        }
    }
   
}
