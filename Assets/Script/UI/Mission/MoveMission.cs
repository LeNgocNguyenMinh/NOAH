using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMission : MonoBehaviour
{
    [SerializeField]private GameObject target;
    private bool isComplete = false;
    private void CheckGoal()
    {
        if(target.GetComponent<MoveGoal>().reachGoal && !isComplete)
        {
            isComplete = true;
            Debug.Log("Mission Complete!");
        }
    }
}
