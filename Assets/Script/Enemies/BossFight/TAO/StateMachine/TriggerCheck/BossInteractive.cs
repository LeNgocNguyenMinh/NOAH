using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossInteractive : MonoBehaviour
{
    [SerializeField] private AOBoss aoBoss;
    public void WakeBoss()
    {
        if(aoBoss != null)
        {
            aoBoss.BossAwake();
        }
    }
}
