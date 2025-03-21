using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOnTopInteractive : MonoBehaviour
{
    private Animator animator;
    private PillarInteract pillarInteract;
    private int lightStatus; //1 is Idle, 2 is Light Up
    private float lightCountDown;
    private void Start()
    {
        animator = GetComponent<Animator>();
        pillarInteract = GetComponentInParent<PillarInteract>();
        lightStatus = 1;
    }
    private void Update()
    {
        if(lightStatus == 2)
        {
            lightCountDown -= Time.deltaTime;
            if(lightCountDown <= 0)
            {
                LightOnTopDeactive();
            }
        }
    }
    public void LightOnTopActive()
    {
        lightCountDown = 5f;
        if(lightStatus != 2)
        {
            lightStatus = 2;
            animator.SetTrigger("LightUp");
        }
    }
    public void LightOnTopDeactive()
    {
        if(lightStatus != 1)
        {
            lightStatus = 1;
            pillarInteract.ResetEnterPass();
            animator.SetTrigger("LightDown");
        }
    }
}
