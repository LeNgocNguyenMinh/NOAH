using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOnTopInteractive : MonoBehaviour
{
    [SerializeField]private Animator animator;
    [SerializeField]private PillarInteract pillarInteract;
    private bool lightIsOn;
    private bool lightIsIdle;
    [SerializeField]private float lightIdleTime;
    private float lightCount;

    void Start()
    {
        lightIsOn = false;
        lightIsIdle = false;
    }
    public void Update()
    {
        if(lightIsIdle)
        {
            if(lightCount <=0 )
            {
                lightCount = 0;
                TurnOffLight();
            }
            else
            {
                lightCount -= Time.deltaTime;
            }
        }
    }
    public void TurnOnLight()
    {
        if (!lightIsOn)
        {
            lightIsIdle = false;
            lightIsOn = true;
            animator.SetTrigger("LightOn");
        }
    }
    public void LightIdle()
    {
        lightIsIdle = true;
        lightCount = lightIdleTime;
        animator.SetTrigger("LightIdle");
    }
    public void TurnOffLight()
    {
        if (lightIsOn)
        {
            lightIsIdle = false;
            lightIsOn = false;
            pillarInteract.ResetEnterPass();
            animator.SetTrigger("LightOut");
        }
    }

}
