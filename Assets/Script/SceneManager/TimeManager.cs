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
    [SerializeField]private TextMeshProUGUI amAndPmBox;//Show AM or PM
    public TextMeshProUGUI timeDisplay;//Display Minute to check or for who want easier clock
    public TextMeshProUGUI dayDisplay;//Display the date
    public Gradient sunLightGradient;//Create the change of the gradient for sun light
    [SerializeField]private GameObject lightSource = null;//Create the list of the light such as light from house

    public Light2D[] sunLight; //List of Sunlight 
    private string[] seasons = {"Spring", "Summer", "Autumn", "Winter"};
    private string[] dateList = {"Mon", "Tue", "Wed","Thu", "Fri", "Sat", "Sun"};
    public float tick; 
    private float min;
    private float hour;
    private int dateIndex;
    private float percentage;
    private ShopInteractive shopInteractive;
    private ShopController shopController;

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
            if(FindObjectOfType<ShopController>()!=null)
            {
                shopController = FindObjectOfType<ShopController>().GetComponent<ShopController>();
                shopController.AddItemToShop();
            }
        }

        if(hour > 12) // Mean finish a first circle 
        {
            amAndPmBox.text = "PM";
        }
        else{
            amAndPmBox.text = "AM";
        }

        if(FindObjectOfType<ShopInteractive>()!=null)
        {
            shopInteractive = FindObjectOfType<ShopInteractive>().GetComponent<ShopInteractive>();
            if(10 <= hour && hour <= 22)
            {
                shopInteractive.canOpenShop = true;
            }
            else{
                shopInteractive.canOpenShop = false;
            }
        }
        
        percentage = hour/24*1f;
        if(sunLight.Length > 0)
        {
            foreach(Light2D light in sunLight)
            {
                if(light != null)
                {
                    light.color = sunLightGradient.Evaluate(percentage);
                } 
            }
        }

        if(lightSource != null)
        {
            if((19 <= hour && hour <= 24)||(0<=hour && hour <=3))
            {
                lightSource.SetActive(true);
            }
            else{
                lightSource.SetActive(false);
            }
        } 
        float hourAngle = (hour + min / 60f) * (360f / 12f); // Calculate rotate angle for hour stick
        clockHourStick.localEulerAngles = new Vector3(0, 0, -hourAngle);//Rrotate clockwise
        float minuteAngle = min * 6f; // Each minute is 360/60 = 6 degrees
        clockMinuteStick.localEulerAngles = new Vector3(0, 0, -minuteAngle);       
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
    public TimeSaveData GetTimeSkip()
    {
        dateIndex +=1;
        if(dateIndex == 7)
        {
            dateIndex = 0;
        }
        return new TimeSaveData
        {
            minData = 0,
            hourData = 0,
            dateData = dateIndex,
        };
    }
    public void SetTime(TimeSaveData timeSaveData)
    {
        this.min = timeSaveData.minData;
        this.hour = timeSaveData.hourData;
        this.dateIndex = timeSaveData.dateData;
    }
}
