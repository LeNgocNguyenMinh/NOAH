using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class SpawnControl : MonoBehaviour
{
    [SerializeField]private PlayerStatus playerStatus;
    private string saveLocation;
    void Awake()
    {
        saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
    }
    public void RespawnAfterDead()
    {
        /* saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
        SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));
        Time.timeScale = 1f;
        PlayerStatusAfterRespawn();
        Player.Instance.transform.position = saveData.playerPosition; */
        SaveController.Instance.LoadSave();
    }
   
    private void PlayerStatusAfterRespawn()
    {
        Player.Instance.PlayerRespawn();
        playerStatus.RespawnPlayerAfterDead();//Player Status after dead and was respawn
        HealthControl.Instance.PlayerHeatlthAfterRespawn(); //Player Health UI
    }
}
