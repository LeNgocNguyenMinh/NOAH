using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDSummon : MonoBehaviour
{
    [SerializeField]private GameObject bossPrefab;
    [SerializeField]private Transform bossSummonPoint;
    [SerializeField]private Transform[] gateSummonPointTD;
    [SerializeField]private Transform[] gateSummonPointLR;
    [SerializeField]private GameObject gateTDPrefab;
    [SerializeField]private GameObject gateLRPrefab;
    [SerializeField]private GameObject fdBossTimeLine;
       
    public void SummonFDBoss()
    {
        fdBossTimeLine.SetActive(false);
        Instantiate(bossPrefab, bossSummonPoint.position, Quaternion.identity);
        for(int i = 0; i < gateSummonPointTD.Length; i++)
        {
            Instantiate(gateTDPrefab, gateSummonPointTD[i].position, Quaternion.identity);
        }
        for(int i = 0; i < gateSummonPointLR.Length; i++)
        {
            Instantiate(gateLRPrefab, gateSummonPointLR[i].position, Quaternion.identity);
        }
    }
}
