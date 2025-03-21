using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDemonSpawn : MonoBehaviour
{
    [SerializeField]private GameObject bombDemonPrefab;
    [SerializeField]private Transform[] spawnPointTransform;

    public void BombDemonSummon()
    {
        for(int i = 0; i < spawnPointTransform.Length; i++)
        {
            Instantiate(bombDemonPrefab, spawnPointTransform[i].position, Quaternion.identity);
        }
    } 
}
