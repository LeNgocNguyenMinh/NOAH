using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    private TimeManager timeManager; 
    private TimeSaveData tmpTime = new TimeSaveData();
    private TmpDataManager tmpDataManager;
    [SerializeField]private string sceneBuildIndex;
    private void OnTriggerEnter2D(Collider2D other)
    {
            if(other.tag == "Player")
            {
                tmpDataManager = FindObjectOfType<TmpDataManager>()?.GetComponent<TmpDataManager>();
                if(tmpDataManager != null)
                {
                    tmpDataManager.SetAllTMPData();
                }
                StartCoroutine(LoadSceneAsync(sceneBuildIndex));
            }
    }
    private IEnumerator LoadSceneAsync(string sceneName)
    {
        yield return null;
        // Bắt đầu load scene nhưng không active ngay lập tức
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;
        timeManager = FindObjectOfType<TimeManager>().GetComponent<TimeManager>();
        tmpTime = timeManager.GetTime();
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
        if(sceneBuildIndex == "Level1")
        {
            Vector3 pos = new Vector3(-25.75f, 10.07f, 0f);
            SaveController.Instance.LoadSave(pos, TmpDataManager.tmpInventory, TmpDataManager.tmpHotBar, TmpDataManager.tmpListItemsInGround, TmpDataManager.tmpTime, TmpDataManager.tmpPlayer);
        }  
        if(sceneBuildIndex == "Level2")
        {
            Vector3 pos = new Vector3(-6.4f, -5.55f, 0f);
            SaveController.Instance.LoadSave(pos, TmpDataManager.tmpInventory, TmpDataManager.tmpHotBar, null, TmpDataManager.tmpTime, TmpDataManager.tmpPlayer);
        }    
    }
}
