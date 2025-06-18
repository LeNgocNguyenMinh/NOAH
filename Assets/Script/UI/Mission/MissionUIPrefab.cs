using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Newtonsoft.Json;

public class MissionUIPrefab : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI missionName;
    [SerializeField]private TextMeshProUGUI missionDescription;
    [SerializeField]private TextMeshProUGUI missionProgress;
    [SerializeField]private Toggle missionToggle;
    private string missionID;
    public void AddMission(MissionStatus missionStatus)
    {
        missionID = missionStatus.missionID;
        if(MissionManager.Instance.IsCurrentMissionID(missionID))
        {
            missionToggle.isOn = true;
        }
        else
        {
            missionToggle.isOn = false;
        }
        Mission currentMiss = MissionManager.Instance.GetMissionByID(missionStatus.missionID);
        missionName.text = currentMiss.missionName;
        missionDescription.text = currentMiss.missionDes;
        missionProgress.text = $"{missionStatus.currentAmount}/{currentMiss.requiredAmount}";
    }
    void OnEnable()
    {
        missionToggle.onValueChanged.AddListener(OnToggleChanged);
    }
    void OnToggleChanged(bool isOn)
    {
        if (isOn)
        {
            MissionManager.Instance.SetCurrentMission(missionID);
        }
        else
        {
            MissionManager.Instance.SetCurrentMission(null);
        }
    }
}
