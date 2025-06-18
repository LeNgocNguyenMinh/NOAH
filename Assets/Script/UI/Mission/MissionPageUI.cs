using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionPageUI : MonoBehaviour
{
    public static MissionPageUI Instance;
    [SerializeField]private MissionUIPrefab missionUIPrefab;
    [SerializeField]private RectTransform activeMissContent;
    private List<MissionUIPrefab> listOfMissionPrefab = new List<MissionUIPrefab>();
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    public void InitializeActiveMission(List<MissionStatus> activeMissions)
    {
        for (int i = 0; i < activeMissions.Count; i++)
        {
            MissionUIPrefab misssionPrefab = Instantiate(missionUIPrefab, Vector3.zero, Quaternion.identity);
            misssionPrefab.transform.SetParent(activeMissContent);
            missionUIPrefab.AddMission(activeMissions[i]);
            listOfMissionPrefab.Add(misssionPrefab);
        }
    }
    
}
