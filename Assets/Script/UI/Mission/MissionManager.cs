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
    public List<MissionStatus> listOfMission = new List<MissionStatus>();
    [SerializeField]private Transform playerTransform;
    private string currentMissionID;
    private MissionStatus currentMissionStatus;
    private Mission currentMission;
    private int currentAmount = 0;
    private bool inLineMission = false;
    private MissionPageUI missionPageUI;
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    // Update Collect Mission
    public void UpdateCollectMission(string itemID, int amount)
    {
        if(currentMission==null)return;
        if (currentMission.missionType == MissionType.CollectMission && 
            currentMission.item.itemID == itemID)
        {
            currentMissionStatus.currentAmount += amount;
            CheckMissionProgress();
        }
    }

    public void SetLineMission(string missionID)
    {
        currentMissionID = missionID;
        inLineMission = true;
        currentMission = GetMissionByID(missionID);
        currentMissionStatus = GetMissionStatusFromList(missionID);
        if(currentMissionStatus == null)
        {
            currentMissionStatus = new MissionStatus
            {
                missionID = currentMissionID,
                currentAmount = 0,
                isFinish = false
            };
            listOfMission.Add(currentMissionStatus);
            MissionPageUI.Instance.InitializeMissionBoard(listOfMission);
        }
        CheckMissionProgress();
    }

    private void CheckMissionProgress()
    {
        if(currentMission.missionType == MissionType.CollectMission && currentMissionStatus.isFinish == false)
        {
            if (currentMissionStatus.currentAmount >= currentMission.requiredAmount)
            {
                PopUp.Instance.ShowNotification("Finish mission: " + currentMission.missionName);
                MissionFinish();
                return;
            }
        }
        missionName.text = currentMission.missionName;
        missionDescription.text = currentMission.missionDes;
        missionProgress.text = $"{currentMissionStatus.currentAmount}/{currentMission.requiredAmount}";
        MissionPageUI.Instance.UpdateMission(currentMissionStatus);
    }
    private void MissionFinish()
    {
        ClaimReward();
        if(!inLineMission)
        {
            currentMissionStatus.isFinish = true;
        }
        else
        {
            inLineMission = false;
            currentMissionStatus.isFinish = true;
        }
        MissionPageUI.Instance.InitializeMissionBoard(listOfMission);
        SetCurrentMission("");
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
    public List<MissionStatus> GetCurrentMission()
    {
        List<MissionStatus> missionSaveDataList = new List<MissionStatus>();
        foreach(MissionStatus mission in listOfMission)
        {
            MissionStatus saveData = new MissionStatus
            {
                currentAmount = mission.currentAmount, // Reset current amount for each mission
                missionID = mission.missionID,
                isFinish = mission.isFinish
            };
            missionSaveDataList.Add(saveData);
        }
        return missionSaveDataList;
    }
    public void SetCurrentMission(string missionID)
    {
        if(missionID == "")
        {
            currentMissionID = null;
            currentMission = null;
            currentMissionStatus = null;
            missionName.text = "-No mission is selected-";
            missionDescription.text = "Please open Mission board(J) and select a mission";
            missionProgress.text = "--------";
            return;
        }
        currentMissionID = missionID;
        currentMission = GetMissionByID(missionID);
        currentMissionStatus = GetMissionStatusFromList(missionID);
        
        currentAmount = currentMissionStatus.currentAmount;
        CheckMissionProgress();
    }
    public Mission GetMissionByID(string missionID)
    {
        foreach(Mission mission in missionScriptObject.missionList)
        {
            if (mission.missionID == missionID)
            {
                return mission;
            }
        }
        foreach(Mission mission in missionScriptObject.npcMissionList)
        {
            if (mission.missionID == missionID)
            {
                return mission;
            }
        }
        return null;
    }
    public MissionStatus GetMissionStatusFromList(string missionID)
    {
        foreach (MissionStatus missionSaveData in listOfMission)
        {
            if (missionSaveData.missionID == missionID)
            {
                return missionSaveData;
            }
        }
        return null;
    }
    public void SetMissionList(MissionSaveData missionSaveData)
    {
        missionPageUI = GetComponent<MissionPageUI>();
        listOfMission.Clear();
        listOfMission = missionSaveData.missionList;
        missionPageUI.InitializeMissionBoard(listOfMission);
        SetCurrentMission(missionSaveData.currentMissionID);
    }
    public MissionSaveData GetMissionList()
    {
        MissionSaveData missionSaveData = new MissionSaveData();
        missionSaveData.missionList = listOfMission;
        missionSaveData.currentMissionID = currentMissionID;
        return missionSaveData;
    }
    public bool IsCurrentMissionID(string missionID)
    {
        if(missionID != currentMissionID || currentMissionID == null)
        {
            return false;
        }
        return true;
    }
}
[System.Serializable]
public class MissionStatus
{
    public int currentAmount = 0;
    public bool isFinish = false;
    public string missionID;
}
[System.Serializable]
public class MissionSaveData
{
    public List<MissionStatus> missionList;
    public string currentMissionID;
}
