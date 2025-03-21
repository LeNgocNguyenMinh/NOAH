using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDManager : MonoBehaviour
{
    [SerializeField]private Transform moveTo;
    [SerializeField]private GameObject atk;
    [SerializeField]private GameObject death;
    [SerializeField]private Animator fdAnimator;
    private PlayerControl playerControl;
    public void DeathTrigger()
    {
        PlayerSpawnAfterWin();
        atk.SetActive(false);
        death.SetActive(true);
        fdAnimator.SetTrigger("headAppear");
    }
    private void PlayerSpawnAfterWin()
    {
        playerControl = FindObjectOfType<PlayerControl>();
        playerControl.transform.position = moveTo.position;
    }    
}
