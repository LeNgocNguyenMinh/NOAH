using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class MissionManager : MonoBehaviour
{
    public static MissionManager Instance;
    [SerializeField]private MissionScriptable missionList;
    [SerializeField]private TMP_Text missionName;
    [SerializeField]private TMP_Text missionDescription;
    [SerializeField]private TMP_Text missionProgress;
    private int currentMissionIndex = 0;
    [SerializeField]private List<Mission> activeMissions = new List<Mission>();
    [SerializeField]private List<Mission> finishMissions = new List<Mission>();
    [SerializeField]private Transform playerTransform;
    private Mission currentMission;
    private int currentAmount = 0;
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        currentMission = missionList.GetMissionByInDex(0);
        CheckMissionProgress();
    }
    // Thêm nhiệm vụ mới
    public void AddMission(Mission newMission)
    {
        activeMissions.Add(newMission);
    }
    
    // Cập nhật nhiệm vụ thu thập
    public void UpdateCollectMission(string itemID, int amount)
    {
        if (currentMission.missionType == MissionType.CollectMission && 
            currentMission.item.itemID == itemID             )
        {
            currentAmount += amount;
            CheckMissionProgress();
        }
    }
    
    // Cập nhật nhiệm vụ tiêu diệt


    private void CheckMissionProgress()
    {
        if (currentAmount >= currentMission.requiredAmount)
        {
            finishMissions.Add(currentMission);
            currentMissionIndex++;
            PopUp.Instance.ShowNotification("Finish mission: " + currentMission.missionName);
            if(missionList.GetMissionByInDex(currentMissionIndex)!= null)
            {
                currentMission = missionList.GetMissionByInDex(currentMissionIndex);
            }
            else return;
            currentAmount = 0;
        }
        missionName.text = currentMission.missionName;
        missionDescription.text = "Collect " + currentMission.requiredAmount + " " + currentMission.item.itemName;
        missionProgress.text = $"{currentAmount}/{currentMission.requiredAmount}";
    }
    
    // Nhận thưởng
    public void ClaimReward(Mission mission)
    {
    }
    public MissionSaveData GetCurrentMission()
    {
        return new MissionSaveData
        {
            currentAmount = this.currentAmount,
            missionIndex = this.currentMissionIndex,
            currentMission = this.currentMission
        };
    }
    public void SetCurrentMission(MissionSaveData saveData)
    {
        currentMissionIndex = saveData.missionIndex;
        currentMission = saveData.currentMission;
        currentAmount = saveData.currentAmount;
        CheckMissionProgress();
    }
}
