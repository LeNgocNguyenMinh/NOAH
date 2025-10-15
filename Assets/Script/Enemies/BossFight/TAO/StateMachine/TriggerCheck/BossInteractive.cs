using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossInteractive : MonoBehaviour
{
    private enum LinkToBoss{
        AOBoss,
        FKBoss
    }
    [SerializeField]private LinkToBoss linkToBoss;
    public void WakeBoss()
    {
        if(linkToBoss == LinkToBoss.AOBoss)
            AOBoss.Instance.BossAwake();
        else if(linkToBoss == LinkToBoss.FKBoss)
            FKBoss.Instance.BossAwake();
    }
}
