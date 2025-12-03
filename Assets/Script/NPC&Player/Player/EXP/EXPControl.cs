using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


// This class control each EXP point come to player
public class EXPControl : MonoBehaviour
{
    public static EXPControl Instance;
    private EXPBar expBar;// EXP bar
    private float expCurrentValue;// EXP current value
    private float expMaxValue; // EXP max value need to achive for level up
    private int currentLevel = 1; //start level
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        expBar = GetComponent<EXPBar>();

        currentLevel = PlayerStatus.Instance.playerLevel;
        expCurrentValue = PlayerStatus.Instance.currentExp;
        expMaxValue = PlayerStatus.Instance.maxExp;

        expBar.SetMaxEXP(expMaxValue); //Important, alway set SetMaxEXP before setEXP, cause the default value Unity use is 1, mean may cause the Wrong logic
        expBar.SetEXP(expCurrentValue); 

        expBar.UpdateLevelText(currentLevel);
        expBar.UpdateEXPText();
    }
    public void AddEXP(float expValue)
    {
        if(expCurrentValue + expValue >= expMaxValue)//If enough exp or more than for level up
        {
            float expExtend = expCurrentValue + expValue - expMaxValue;//calculate the extend exp Point 
            expCurrentValue = expExtend;//Set the current exp = expExtend
            
            PlayerStatus.Instance.SetCurrentEXP(expCurrentValue);//Update data
            expBar.SetEXP(expCurrentValue);

            currentLevel++;//Level Up
            expBar.UpdateLevelText(currentLevel);//Update text box in level box
            PlayerStatus.Instance.SetLevel(currentLevel);//Set new level in player Status (scriptObjectable)

            if(currentLevel % 2 == 0) //only level 2,4,6, ...
            {
                PlayerStatus.Instance.SetAvailablePoint(1);//add 1 point
            }
            expMaxValue += (expMaxValue*0.5f);//Set max exp = current Max + (current Max / 2)

            PlayerStatus.Instance.SetMaxEXP(expMaxValue);//Update
            expBar.SetMaxEXP(expMaxValue);
        }
        else{//Else, just add exp value like normal 
            expCurrentValue += expValue;

            PlayerStatus.Instance.SetCurrentEXP(expCurrentValue);
            expBar.SetEXP(expCurrentValue);
        }
        expBar.UpdateEXPText();
    }
}
