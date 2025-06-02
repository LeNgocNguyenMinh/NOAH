using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MissionType
{
    CollectMission,
    KillMonsterMission,
    ButtonMission
}

[System.Serializable]
public class Mission
{
    public string missionName;
    public MissionType missionType;
    public string targetID; 
    public string missionDes;
     [Header("-------For Item collect mission------")]
    public Item item;
    public int requiredAmount;
    [Header("-------For Btn mission------")]
    public KeyCode keyCode;

    
    // Phần thưởng
    public int rewardCoins;
}
