using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectMission : MonoBehaviour
{
    [SerializeField]private Item item;
    [SerializeField]private int collectAmount;
    private int currentAmount = 0;
    private bool isComplete = false;
    public bool IsComplete()
    {
        return isComplete;
    }
    public void CheckGoal()
    {
        if(currentAmount >= collectAmount)
        {
            isComplete = true;
            Debug.Log("Mission Complete!");
        }
    }
}
