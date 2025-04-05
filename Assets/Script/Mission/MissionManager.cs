using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissionManager : MonoBehaviour
{
    public static MissionManager Instance;
    public TMP_Text missionName;
    public TMP_Text missionDescription;
    public TMP_Text missionProgress;
    private int currentMissionIndex = 0;
    [SerializeField]private List<Mission> activeMissions = new List<Mission>();
    [SerializeField]private List<Mission> finishMissions = new List<Mission>();
    [SerializeField]private Transform playerTransform;
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    public void Update()
    {
        if (activeMissions.Count > 0)
        {
            missionName.text = activeMissions[currentMissionIndex].missionName;
            missionDescription.text = "Collect " + activeMissions[currentMissionIndex].requiredAmount + " " + activeMissions[currentMissionIndex].item.itemName;
            missionProgress.text = $"{activeMissions[currentMissionIndex].currentAmount}/{activeMissions[currentMissionIndex].requiredAmount}";
        }
    }
    // Thêm nhiệm vụ mới
    public void AddMission(Mission newMission)
    {
        activeMissions.Add(newMission);
    }
    
    // Cập nhật nhiệm vụ thu thập
    public void UpdateCollectMission(string itemID, int amount)
    {
        if (activeMissions[currentMissionIndex].missionType == MissionType.CollectMission && 
            activeMissions[currentMissionIndex].item.itemID == itemID && 
            !activeMissions[currentMissionIndex].isCompleted)
        {
            activeMissions[currentMissionIndex].currentAmount += amount;
            CheckMissionProgress(activeMissions[currentMissionIndex]);
        }
    }
    
    // Cập nhật nhiệm vụ tiêu diệt
    public void UpdateDefeatMission(string enemyID, int amount)
    {
        foreach (Mission mission in activeMissions)
        {
            if (mission.missionType == MissionType.KillMonsterMission && 
                mission.targetID == enemyID && 
                !mission.isCompleted)
            {
                mission.currentAmount += amount;
                CheckMissionProgress(mission);
            }
        }
    }
    void CheckMovementMissions()
    {
        playerTransform = FindObjectOfType<PlayerControl>().transform;
        foreach (Mission mission in activeMissions)
        {
            if (mission.missionType == MissionType.MoveMission && 
                !mission.isCompleted)
            {
                float distance = Vector2.Distance(
                    playerTransform.position, 
                    mission.targetPosition
                );

                mission.currentAmount = distance <= mission.requiredRadius ? 1 : 0;
                
                if (distance <= mission.requiredRadius)
                {
                    mission.isCompleted = true;
                    /* UIManager.Instance.UpdateMissionUI(); */
                }
            }
        }
    }
    private void CheckMissionProgress(Mission mission)
    {
        if (mission.currentAmount >= mission.requiredAmount)
        {
            mission.isCompleted = true;
            Debug.Log("Hoàn thành");
            // Kích hoạt sự kiện cập nhật UI
            /* UIManager.Instance.UpdateMissionUI(); */
            finishMissions.Add(mission);
            currentMissionIndex++;
        }
    }
    
    // Nhận thưởng
    public void ClaimReward(Mission mission)
    {
        if (mission.isCompleted && !mission.isClaimed)
        {
            // Thêm phần thưởng
            /* InventoryManager.Instance.AddCoins(mission.rewardCoins);
            InventoryManager.Instance.AddItem(mission.rewardItemID, mission.rewardItemAmount); */
            
            mission.isClaimed = true;
            activeMissions.Remove(mission);
        }
    }
}
