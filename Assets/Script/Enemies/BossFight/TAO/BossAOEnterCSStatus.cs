using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAOEnterCSStatus : MonoBehaviour
{
    [SerializeField]private GameObject cmCam;
    public void ActiveBossAOFight()
    {
        cmCam.SetActive(true);
        BossAOSummon bossAOSummon = FindObjectOfType<BossAOSummon>().GetComponent<BossAOSummon>();
        bossAOSummon.SummonAOBoss();
    }
}
