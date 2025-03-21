using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


// This class control each EXP point come to player
public class EXPControl : MonoBehaviour
{
    private LevelControl levelControl; //Script for level
    [SerializeField]private TextMeshProUGUI EXPText;
    [SerializeField]private PlayerStatus playerStatus;
    [SerializeField]private EXPBar expBar;// EXP bar
    private float expCurrentValue;// EXP current value
    private float expMaxValue; // EXP max value need to achive for level up
    private int currentLevel = 1; //start level
    
    void Start()
    {
        levelControl = GetComponentInChildren<LevelControl>();
        
        //Set Start Value 
        currentLevel = playerStatus.playerLevel;
        expCurrentValue = playerStatus.currentExp;
        expMaxValue = playerStatus.maxExp;

        expBar.SetMaxEXP(expMaxValue); //Important, alway set SetMaxEXP before setEXP, cause the default value Unity use is 1, mean may cause the Wrong logic
        expBar.SetEXP(expCurrentValue); 

        levelControl.UpdateLevelText(currentLevel);
        UpdateEXPText();
    }
   

    private void UpdateEXPText()
    {
        EXPText.text = $"{(int)expCurrentValue} / {(int)expMaxValue}";
    }
    public void AddEXP(float expValue)
    {
        if(expCurrentValue + expValue >= expMaxValue)//If enough exp or more than for level up
        {
            float expExtend = expCurrentValue + expValue - expMaxValue;//calculate the extend exp Point 
            expCurrentValue = expExtend;//Set the current exp = expExtend
            
            playerStatus.SetCurrentEXP(expCurrentValue);//Update data
            expBar.SetEXP(expCurrentValue);

            currentLevel++;//Level Up
            levelControl.UpdateLevelText(currentLevel);//Update text box in level box
            playerStatus.SetLevel(currentLevel);//Set new level in player Status (scriptObjectable)

            if(currentLevel % 2 == 0) //only level 2,4,6, ...
            {
                playerStatus.SetAvailablePoint(1);//add 1 point
            }
            expMaxValue += (expMaxValue*0.5f);//Set max exp = current Max + (current Max / 2)

            playerStatus.SetMaxEXP(expMaxValue);//Update
            expBar.SetMaxEXP(expMaxValue);
        }
        else{//Else, just add exp value like normal 
            expCurrentValue += expValue;

            playerStatus.SetCurrentEXP(expCurrentValue);
            expBar.SetEXP(expCurrentValue);
        }
        UpdateEXPText();
    }
}
