using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MissionType
{
    CollectMission,
    MoveMission,
    KillMonsterMission
}

[System.Serializable]
public class Mission
{
    public string missionID;
    public string missionName;
    public MissionType missionType;
    public string targetID; // ID vật phẩm hoặc quái
    public Item item;
    public int requiredAmount;
    public Vector2 targetPosition;
    public float requiredRadius = 1f; // Bán kính hợp lệ
    
    // Phần thưởng
    public int rewardCoins;
}
