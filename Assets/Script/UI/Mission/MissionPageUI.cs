using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MissionPageUI : MonoBehaviour
{
    public static MissionPageUI Instance;
    [SerializeField]private MissionUIPrefab missionUIPrefab;
    [SerializeField]private RectTransform currentMissionContent;
    [SerializeField]private RectTransform activeMissContent;
    [SerializeField]private RectTransform finishMissContent;
    [SerializeField]private RectTransform missionNamePanel;
    [SerializeField]private RectTransform missionDesPanel;
    [SerializeField]private RectTransform missionProgPanel;
    [SerializeField]private TextMeshProUGUI missionName;
    [SerializeField]private TextMeshProUGUI missionDescription;
    [SerializeField]private TextMeshProUGUI missionProgress;
    [SerializeField]private float delay1;
    [SerializeField]private float delay2;
    [SerializeField]private float delay3;
    [SerializeField]private Vector2 visiblePos1;
    [SerializeField]private Vector2 visiblePos2;
    [SerializeField]private Vector2 visiblePos3;
    [SerializeField] private Vector2 hiddenPos1;
    [SerializeField] private Vector2 hiddenPos2;
    [SerializeField] private Vector2 hiddenPos3;
    [SerializeField] float moveDuration;
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
    public void ShowMissionInfo(MissionStatus missionStatus)
    {
        Mission currentMiss = MissionManager.Instance.GetMissionByID(missionStatus.missionID);
        missionName.text = currentMiss.missionName;
        missionDescription.text = currentMiss.missionDes;
        missionProgress.text = $"{missionStatus.currentAmount}/{currentMiss.requiredAmount}";
    }
    public void HideMissionInfo()
    {
        missionName.text = "-NaN-";
        missionDescription.text = "-Select a mission-";
        missionProgress.text = "-NaN-";
    }
    
}
