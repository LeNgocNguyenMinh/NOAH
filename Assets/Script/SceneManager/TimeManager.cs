using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using TMPro;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    [SerializeField]private Transform clockHourStick;//Transform of the hour stick, for rotate purpose 
    [SerializeField]private Transform clockMinuteStick;//Transform of the minute stick, for rotate purpose 
    [SerializeField]private TextMeshProUGUI amAndpmBox;//Show AM or PM
    [SerializeField]private TimeObjectScriptable timeData;
    public TextMeshProUGUI timeDisplay;//Display Minute to check or for who want easier clock
    public TextMeshProUGUI dayDisplay;//Display the date
    public Gradient sunLightGradient;//Create the change of the gradient for sun light
    private GameObject lightSource;//Create the list of the light such as light from house

    public Light2D[] sunLight; //List of Sunlight 
    private string[] seasons = {"Spring", "Summer", "Autumn", "Winter"};
    private string[] dateList = {"Mon", "Tue", "Wed","Thu", "Fri", "Sat", "Sun"};
    public float tick; 
    private float min;
    private float hour;
    private int dateIndex;
    private float percentage;

    void FixedUpdate() // we used fixed update, since update is frame dependant. 
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            min = 0;
            hour = 0;
            dateIndex = 1; // Load dateIndex as well
        }
        CalcTime();
        DisplayTime();
    }
 
    public void CalcTime() // Used to calculate sec, min and hours
    {
        
        min += Time.fixedDeltaTime * tick; // multiply time between fixed update by tick
 
        if (min >= 60) //60 min = 1 hr
        {
            min = 0;
            hour += 1;
        }
 
        if (hour >= 24) //24 hr = 1 day
        {
            hour = 0;
            dateIndex += 1;
            if(dateIndex == 7)
            {
                dateIndex = 0;
            }
        }

        float hourAngle = (hour + min / 60f) * (360f / 12f); // Calculate rotate angle for hour stick
        clockHourStick.localEulerAngles = new Vector3(0, 0, -hourAngle);//Rrotate clockwise

        if(hourAngle > 360) // Mean finish a first circle 
        {
            amAndpmBox.text = "PM";
        }
        else{
            amAndpmBox.text = "AM";
        }
        float minuteAngle = min * 6f; // Each minute is 360/60 = 6 degrees
        clockMinuteStick.localEulerAngles = new Vector3(0, 0, -minuteAngle);
        
        lightSource = GameObject.FindWithTag("LightManager");
        if (lightSource == null) return;
        
        if((19 <= hour && hour <= 24)||(0<=hour && hour <=3))
        {
            lightSource.SetActive(true);
        }
        else{
            lightSource.SetActive(false);
        }
        percentage = hour/24*1f;
        foreach(Light2D light in sunLight)
        {
            light.color = sunLightGradient.Evaluate(percentage);
        }
        timeData.SetMinutes(min);
        timeData.SetHours(hour);
        timeData.SetDays(dateIndex);
    }
    public void DisplayTime() // Shows time and day in ui
    {
        timeDisplay.text = string.Format("{0:00}:{1:00}", hour, min); // The formatting ensures that there will always be 0's in empty spaces
        dayDisplay.text = dateList[dateIndex]; // display day counter
    }
    public TimeSaveData GetTime()
    {
        return new TimeSaveData
        {
            minData = min,
            hourData = hour,
            dateData = dateIndex
        };
    }
    public void SetTime(TimeSaveData timeSaveData)
    {
        this.min = timeSaveData.minData;
        this.hour = timeSaveData.hourData;
        this.dateIndex = timeSaveData.dateData;
    }
}
