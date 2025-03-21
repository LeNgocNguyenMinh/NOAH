using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTimeData", menuName = "TimeData")]
public class TimeObjectScriptable :  ScriptableObject
{
    public float minute;
    public float hour;
    public int day;
    public void SetMinutes(float newMins)
    {
        this.minute = newMins;
    }
    public void SetHours(float newHours)
    {
        this.hour = newHours;
    }
    public void SetDays(int newDays)
    {
        this.day = newDays;
    }
}
