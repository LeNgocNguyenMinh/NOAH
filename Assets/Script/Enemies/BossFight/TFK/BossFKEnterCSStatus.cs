using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFKEnterCSStatus : MonoBehaviour
{
    [SerializeField]private GameObject cmCam;
    public void ActiveBossFKFight()
    {
        cmCam.SetActive(true);
        BossFKSummon bossFKSummon = FindObjectOfType<BossFKSummon>().GetComponent<BossFKSummon>();
        bossFKSummon.SummonFKBoss();
    }
}
