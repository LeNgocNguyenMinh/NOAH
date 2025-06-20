using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class MissionUIPrefab : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI missionName;
    [SerializeField]private TextMeshProUGUI missionDescription;
    [SerializeField]private TextMeshProUGUI missionProgress;
    [SerializeField]private Toggle missionToggle;
    public string missionID;
    public void SetMissionInfo(MissionStatus missionStatus)
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
    public void UpdateMission(MissionStatus missionStatus)
    {
        Mission currentMiss = MissionManager.Instance.GetMissionByID(missionStatus.missionID);
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
            MissionPageUI.Instance.UnCheckOther(missionID);
            MissionManager.Instance.SetCurrentMission(missionID);
            transform.SetAsFirstSibling();
        }
        else
        {
            MissionManager.Instance.SetCurrentMission("");
        }
    }
    public void ToggleUncheck()
    {
        missionToggle.isOn = false;
    }
    public void Togglecheck()
    {
        missionToggle.isOn = true;
        transform.SetAsFirstSibling();
    }
    public void HideToggle()
    {
        missionToggle.gameObject.SetActive(false);
    }
}
