using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


// This class control each EXP point come to player
public class PlayerEXPControl : MonoBehaviour
{
    public static PlayerEXPControl Instance;
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
    public void AddEXP(float expValue)
    {
        if(expCurrentValue + expValue >= expMaxValue)//If enough exp or more than for level up
        {
            float expExtend = expCurrentValue + expValue - expMaxValue;//calculate the extend exp Point 
            expCurrentValue = expExtend;//Set the current exp = expExtend

            currentLevel++;//Level Up
            //Set new level in player Status (scriptObjectable)

            if(currentLevel % 2 == 0) //only level 2,4,6, ...
            {
                PlayerStatus.Instance.SetAvailablePoint(1);//add 1 point
            }
            expMaxValue += (expMaxValue*0.5f);//Set max exp = current Max + (current Max / 2)

            PlayerStatus.Instance.SetCurrentEXP(expCurrentValue);
            PlayerStatus.Instance.SetMaxEXP(expMaxValue);//Update
            PlayerStatus.Instance.SetLevel(currentLevel);
        }
        else{//Else, just add exp value like normal 
            expCurrentValue += expValue;

            PlayerStatus.Instance.SetCurrentEXP(expCurrentValue);
        }
        SetCurrentExpStatus();
    }
    public void SetCurrentExpStatus()
    {
        currentLevel = PlayerStatus.Instance.playerLevel;
        expCurrentValue = PlayerStatus.Instance.currentExp;
        expMaxValue = PlayerStatus.Instance.maxExp;

        PlayerEXPBar.Instance.SetMaxEXP(expMaxValue); //Important, alway set SetMaxEXP before setEXP, cause the default value Unity use is 1, mean may cause the Wrong logic
        PlayerEXPBar.Instance.SetCurrentEXP(expCurrentValue); 

        PlayerEXPBar.Instance.UpdateLevelText(currentLevel);
        PlayerEXPBar.Instance.UpdateEXPText();
    }
}
