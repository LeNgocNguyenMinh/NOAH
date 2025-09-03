using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class SpawnControl : MonoBehaviour
{
    [SerializeField]private PlayerStatus playerStatus;
    private string saveLocation;
    private Player playerControl;
    private Animator animator;
    void Awake()
    {
        saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
    }
    public void RespawnAfterDead()
    {
        SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));
        if(SceneManager.GetActiveScene().name != saveData.saveScene)
        {
            StartCoroutine(LoadSceneAsync(saveData.saveScene));
        } 
        else{
            PlayerStatusAfterRespawn();
            FindObjectOfType<Player>().transform.position = saveData.playerPosition;
        }
    }
    private IEnumerator LoadSceneAsync(string sceneName)
    {
        yield return null;
        // Bắt đầu load scene nhưng không active ngay lập tức
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        // Chờ scene load xong
        while (!operation.isDone)
        {
            // Khi progress đạt 0.9 có nghĩa là scene đã load xong, chỉ còn chờ active
            if (operation.progress >= 0.9f)
            {
                SceneManager.sceneLoaded += OnSceneLoaded;
                operation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SaveController.Instance.LoadSave();
    }
    private void PlayerStatusAfterRespawn()
    {
        animator = GetComponent<Animator>();
        playerControl = GetComponent<Player>();

       /*  playerControl.SetIsAlive(true);// do this so player can move */
        animator.SetTrigger("isRespawn");
        playerStatus.RespawnPlayerAfterDead();//Player Status after dead and was respawn
        HealthControl.Instance.PlayerHeatlthAfterRespawn(); //Player Health UI
    }
}
