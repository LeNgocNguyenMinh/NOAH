using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMission", menuName = "ListOfMission")]
public class MissionScriptable : ScriptableObject
{
    public List<Mission> missionList;
    public List<Mission> npcMissionList;
}
