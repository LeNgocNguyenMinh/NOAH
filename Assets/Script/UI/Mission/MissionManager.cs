using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class MissionManager : MonoBehaviour
{
    public static MissionManager Instance;
    [SerializeField]private MissionScriptable missionScriptObject;
    [SerializeField]private TMP_Text missionName;
    [SerializeField]private TMP_Text missionDescription;
    [SerializeField]private TMP_Text missionProgress;
    private int currentMissionIndex = 0;
    [SerializeField]private List<Mission> activeMissions = new List<Mission>();
    [SerializeField]private List<Mission> finishMissions = new List<Mission>();
    [SerializeField]private Transform playerTransform;
    private Mission currentMission;
    private int currentAmount = 0;
    private bool inLineMission = false;
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    // Thêm nhiệm vụ mới
    public void AddMission(Mission newMission)
    {
        activeMissions.Add(newMission);
    }
    
    public void UpdateNewMission(Mission mission)
    {
        if(mission ==null)return;
        currentMission = mission;
        currentAmount = 0;
        missionName.text = currentMission.missionName;
        missionDescription.text = currentMission.missionDes;
        missionProgress.text = $"{currentAmount}/{currentMission.requiredAmount}";
    }
    // Update Collect Mission
    public void UpdateCollectMission(string itemID, int amount)
    {
        if(currentMission==null)return;
        if (currentMission.missionType == MissionType.CollectMission && 
            currentMission.item.itemID == itemID)
        {
            currentAmount += amount;
            CheckMissionProgress();
        }
    }
    public void UpdatePressBtnMission(KeyCode btn)
    {
        if (currentMission.missionType == MissionType.ButtonMission && 
            currentMission.keyCode == btn)
        {
            
        }
    }
    public void SetLineMission(Mission mission)
    {
        inLineMission = true;
        currentMission = mission;
        currentAmount = 0;
        CheckMissionProgress();
    }

    private void CheckMissionProgress()
    {
        if(currentMission.missionType == MissionType.CollectMission)
        {
            if (currentAmount >= currentMission.requiredAmount)
            {
                PopUp.Instance.ShowNotification("Finish mission: " + currentMission.missionName);
                MissionFinish();
                return;
            }
        }
        missionName.text = currentMission.missionName;
        missionDescription.text = currentMission.missionDes;
        missionProgress.text = $"{currentAmount}/{currentMission.requiredAmount}";
    }
    private void MissionFinish()
    {
        ClaimReward();
        finishMissions.Add(currentMission);
        if(!inLineMission)
        {
            currentMissionIndex++;
            if(missionScriptObject.missionList.Count < currentMissionIndex)
            {
                return;
            }
        }
        else
        {
            inLineMission = false;
            currentMission.isFinish = true;
        }
        currentMission = missionScriptObject.GetMissionByIndex(currentMissionIndex);
        UpdateNewMission(currentMission);
    }
    
    // Nhận thưởng
    public void ClaimReward()
    {
        if(currentMission.missionReward.coin > 0)
        {
            CoinControl.Instance.AddCoin(currentMission.missionReward.coin);
            PopUp.Instance.ShowNotification("Add " + currentMission.missionReward.coin + " coins.");
        }
        foreach(ItemAmount itemAmount in currentMission.missionReward.items)
        {
            if(itemAmount != null)
            {
                UIInventoryPage.Instance.AddItem(itemAmount.item, itemAmount.itemQuantity);
                PopUp.Instance.ShowNotification("Add " + itemAmount.itemQuantity + " " + itemAmount.item.itemName + ".");
            }
        }
    }
    public MissionSaveData GetCurrentMission()
    {
        return new MissionSaveData
        {
            currentAmount = this.currentAmount,
            missionIndex = this.currentMissionIndex
        };
    }
    public void SetCurrentMission(MissionSaveData saveData)
    {
        currentAmount = saveData.currentAmount;
        currentMissionIndex = saveData.missionIndex;
        currentMission = missionScriptObject.GetMissionByIndex(currentMissionIndex);
        CheckMissionProgress();
    }
}
