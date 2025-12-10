using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;

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
            UpdateMissionProgress();
        }
    }
    //Add mission from NPC
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
        UpdateMissionProgress();
    }
    // Update the current mission progress
    private void UpdateMissionProgress()
    {
        // Check if the current mission is finish
        if(currentMission.missionType == MissionType.CollectMission && currentMissionStatus.isFinish == false)
        {
            if (currentMissionStatus.currentAmount >= currentMission.requiredAmount)
            {
                NotifPopUp.Instance.ShowNotification("Finish mission: " + currentMission.missionName);
                MissionFinish();
                return;
            }
        }
        //else update the mission progress text
        missionName.text = currentMission.missionName;
        missionDescription.text = currentMission.missionDes;
        missionProgress.text = $"{currentMissionStatus.currentAmount}/{currentMission.requiredAmount}";
    }
    private void MissionFinish()
    {
        //TakeReward
        ClaimReward();
        //Set "Mission Status" is Finish = true
        if(!inLineMission)
        {
            currentMissionStatus.isFinish = true;
        }
        else
        {
            inLineMission = false;
            currentMissionStatus.isFinish = true;
        }
        //Reload all mission in mission board
        MissionPageUI.Instance.InitializeMissionBoard(listOfMission);
        //Set current mission to null
        SetCurrentMission("");
    }
    
    // Nhận thưởng
    public void ClaimReward()
    {
        if(currentMission.missionReward.coin > 0)
        {
            PlayerCoinControl.Instance.AddCoin(currentMission.missionReward.coin);
            NotifPopUp.Instance.ShowNotification("Add " + currentMission.missionReward.coin + " coins.");
        }
        foreach(ItemAmount itemAmount in currentMission.missionReward.items)
        {
            if(itemAmount != null)
            {
                UIInventoryPage.Instance.AddItem(itemAmount.item, itemAmount.itemQuantity);
                NotifPopUp.Instance.ShowNotification("Add " + itemAmount.itemQuantity + " " + itemAmount.item.itemName + ".");
            }
        }
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
        UpdateMissionProgress();
    }
    // Get Mission by ID
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
    public bool IsMissionFinish(string missionID)
    {
        foreach(MissionStatus missionStatus in listOfMission)
        {
            if (missionStatus.missionID == missionID && missionStatus.isFinish)
            {
                return true;
            }
        }
        return false;
    }
    // Get Mission Status from List
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
    // Set Mission List from Save Data
    public void SetMissionList(MissionSaveData missionSaveData)
    {
        missionPageUI = GetComponent<MissionPageUI>();
        listOfMission.Clear();
        listOfMission = missionSaveData.missionList;
        SetCurrentMission(missionSaveData.currentMissionID);
        missionPageUI.InitializeMissionBoard(listOfMission);
    }
    // Get Mission List for Save Data
    public MissionSaveData GetMissionList()
    {
        MissionSaveData missionSaveData = new MissionSaveData();
        missionSaveData.missionList = listOfMission;
        missionSaveData.currentMissionID = currentMissionID;
        return missionSaveData;
    }
    // Check if the current mission ID is the same as the given mission ID
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
