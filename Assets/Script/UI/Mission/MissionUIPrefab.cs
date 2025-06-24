using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class MissionUIPrefab : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]private TextMeshProUGUI missionName;
    [SerializeField]private Toggle missionToggle;
    public string missionID;
    public bool isSelect = false;
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
    public void OnPointerClick(PointerEventData eventData)
    {
        isSelect = true;
        ShowMissionInfo();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowMissionInfo();
    }
    public void ShowMissionInfo()
    {
        MissionStatus missionStatus = MissionManager.Instance.GetMissionStatusFromList(missionID);
        MissionPageUI.Instance.ShowMissionInfo(missionStatus);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(isSelect) return;
        MissionPageUI.Instance.HideMissionInfo();
    }
}
