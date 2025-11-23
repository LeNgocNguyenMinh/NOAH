using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class SpawnControl : MonoBehaviour
{
    [SerializeField]private PlayerStatus playerStatus;
    public static SpawnControl Instance;
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
    public void RespawnAfterDead()
    {
        SaveController.Instance.LoadSave();
        PlayerStatusAfterRespawn();
    }
   
    private void PlayerStatusAfterRespawn()
    {
        PlayerHealthControl.Instance.PlayerHeatlthAfterRespawn(); //Player Health UI
        Player.Instance.PlayerRespawn();
    }
}
