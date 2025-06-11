using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMission", menuName = "ListOfMission")]
public class MissionScriptable : ScriptableObject
{
    public List<Mission> missionList;
    public List<Mission> npcMissionList;
    public Mission GetMissionByIndex(int index)
    {
        if (index >= 0 && index < missionList.Count)
        {
            return missionList[index];
        }
        else
        {
            Debug.LogError("Index out of range");
            return null;
        }
    }
    public Mission GetNPCMissionBuyIndex(int index)
    {
        if (index >= 0 && index < npcMissionList.Count)
        {
            return npcMissionList[index];
        }
        else
        {
            Debug.LogError("Index out of range");
            return null;
        }
    }
}
