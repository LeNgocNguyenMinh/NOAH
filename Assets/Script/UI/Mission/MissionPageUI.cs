using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionPageUI : MonoBehaviour
{
    public static MissionPageUI Instance;
    [SerializeField]private MissionUIPrefab missionUIPrefab;
    [SerializeField]private RectTransform currentMissionContent;
    [SerializeField]private RectTransform activeMissContent;
    [SerializeField]private RectTransform finishMissContent;
    private List<MissionUIPrefab> listOfMissionPrefab = new List<MissionUIPrefab>();
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    public void InitializeMissionBoard(List<MissionStatus> listOfMission)
    {
        ClearMissionBoard();
        for (int i = 0; i < listOfMission.Count; i++)
        {
            MissionUIPrefab missionPrefab = Instantiate(missionUIPrefab, Vector3.zero, Quaternion.identity);
            if(!listOfMission[i].isFinish)
            {
                missionPrefab.transform.SetParent(activeMissContent);
            }
            else{
                missionPrefab.transform.SetParent(finishMissContent);
                missionPrefab.HideToggle();
            }
            missionPrefab.SetMissionInfo(listOfMission[i]);
            listOfMissionPrefab.Add(missionPrefab);
        }
    }
    public void UpdateMission(MissionStatus missionStatus)
    {
        foreach (MissionUIPrefab missionPrefab in listOfMissionPrefab)
        {
            if (missionPrefab.missionID == missionStatus.missionID)
            {
                missionPrefab.UpdateMission(missionStatus);
                return;
            }
        }
    }
    public void UnCheckOther(string missionID)
    {
        foreach (MissionUIPrefab missionPrefab in listOfMissionPrefab)
        {
            if (missionPrefab.missionID != missionID)
            {
                missionPrefab.ToggleUncheck();
            }
        }
    }

    private void ClearMissionBoard()
    {
        foreach (Transform child in activeMissContent)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in finishMissContent)
        {
            Destroy(child.gameObject);
        }
        listOfMissionPrefab.Clear();
    }
    
}
