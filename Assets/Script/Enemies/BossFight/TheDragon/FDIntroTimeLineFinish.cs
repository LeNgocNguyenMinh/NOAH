using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDIntroTimeLineFinish : MonoBehaviour
{
    [SerializeField]private FDDetectStartBattle fdDetectStartBattle;
    [SerializeField]private GameObject fdBossTimeLine;
    public void FinishTimeLine()
    {
        fdDetectStartBattle.SummonFDBoss();
        fdBossTimeLine.SetActive(false);
    }
}
