using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFKSummon : MonoBehaviour
{
    [SerializeField]private GameObject bossPrefab;
    [SerializeField]private Transform bossSummonPoint;
    [SerializeField]private Transform[] gateSummonPointTB;
    [SerializeField]private Transform[] gateSummonPointLR;
    [SerializeField]private GameObject gateTBPrefab;
    [SerializeField]private GameObject gateLRPrefab;
    [SerializeField]private GameObject cmCamera;
    [SerializeField]private GameObject bossTimeLine;
    [SerializeField]private BoxCollider2D boxCollider2D; 
    private List<GameObject> listOfGate = new List<GameObject>();
    private GameObject bossObject;
    private bool isSummon = false;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player") && !isSummon)
        {
            cmCamera.SetActive(false);
            bossTimeLine.SetActive(true);
            boxCollider2D.enabled = false;
            isSummon = true;
        }
    }
    public void SummonFKBoss()
    {
        bossTimeLine.SetActive(false);    
        for(int i = 0; i < gateSummonPointTB.Length; i++)
        {
            GameObject gate = Instantiate(gateTBPrefab, gateSummonPointTB[i].position, Quaternion.identity);
            listOfGate.Add(gate);
        }
        for(int i = 0; i < gateSummonPointLR.Length; i++)
        {
            GameObject gate = Instantiate(gateLRPrefab, gateSummonPointLR[i].position, Quaternion.identity);
            listOfGate.Add(gate);
        }
        
        cmCamera.SetActive(true);
    }
    public void PlayerDeadInBossBattle()
    {
        foreach(GameObject gate in listOfGate)
        {
            if(gate != null)
            {
                Destroy(gate);
            }
        }
        listOfGate.Clear();
        Destroy(bossObject);
        boxCollider2D.enabled = true;
        isSummon = false;
    }
}
