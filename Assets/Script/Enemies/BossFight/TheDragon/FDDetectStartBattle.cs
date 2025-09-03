
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDDetectStartBattle : MonoBehaviour
{
    [SerializeField]private Transform moveTo;
    [SerializeField]private GameObject bossPrefab;
    [SerializeField]private Transform bossSummonPoint;
    [SerializeField]private Transform[] gateSummonPointTD;
    [SerializeField]private Transform[] gateSummonPointLR;
    [SerializeField]private GameObject gateTDPrefab;
    [SerializeField]private GameObject gateLRPrefab;
    [SerializeField]private GameObject fdBossTimeLine;
    [SerializeField]private BoxCollider2D boxCollider2D;
    private Player playerControl;
    private List<GameObject> listOfGate = new List<GameObject>();
    private GameObject bossObject;
    private bool isSummon = false;
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player" && !isSummon)
        {
            isSummon = true;
            playerControl = FindObjectOfType<Player>();
            playerControl.transform.position = moveTo.position;
            fdBossTimeLine.SetActive(true);
            boxCollider2D.enabled = false;
        }
    }
    public void SummonFDBoss()
    {
        isSummon = true;
        for(int i = 0; i < gateSummonPointTD.Length; i++)
        {
            GameObject gate = Instantiate(gateTDPrefab, gateSummonPointTD[i].position, Quaternion.identity);
            listOfGate.Add(gate);
        }
        for(int i = 0; i < gateSummonPointLR.Length; i++)
        {
            GameObject gate = Instantiate(gateLRPrefab, gateSummonPointLR[i].position, Quaternion.identity);
            listOfGate.Add(gate);
        }
        bossObject = Instantiate(bossPrefab, bossSummonPoint.position, Quaternion.identity);
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
