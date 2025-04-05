using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMission", menuName = "Mission")]
public class MissionScriptable : ScriptableObject
{
    public string missionName;
    public string missionDescription;

}
