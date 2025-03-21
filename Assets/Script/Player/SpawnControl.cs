using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnControl : MonoBehaviour
{
    [SerializeField]private PlayerStatus playerStatus;
    private Vector3 reSpawnPoint;
    private PlayerControl playerControl;
    private HealthControl playerHealthControl;
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
        playerControl = GetComponent<PlayerControl>();
        playerHealthControl = GetComponent<HealthControl>();
    }
    public void RespawnAfterDead()
    {
      /*   playerControl.SetIsAlive(true);// do this so player can move
        animator.SetTrigger("isRespawn");
        playerStatus.RespawnPlayerAfterDead();//Player Status after dead and was respawn
        playerHealthControl.PlayerHeatlthAfterRespawn(); //Player Health UI
        gameObject.transform.position = reSpawnPoint; //Transform Point. If Vinh finish the save, 
        //chance the code here */
    }
}
