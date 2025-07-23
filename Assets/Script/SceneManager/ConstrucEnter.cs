using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConstrucEnter : MonoBehaviour
{
    private TmpDataManager tmpDataManager;
    [SerializeField]private string sceneBuildIndex;
    [SerializeField]private Transform moveTo;
    private void OnTriggerEnter2D(Collider2D other)
    {
            if(other.tag == "Player")
            {
                /* tmpDataManager = FindObjectOfType<TmpDataManager>()?.GetComponent<TmpDataManager>();
                if(tmpDataManager != null)
                {
                    tmpDataManager.SetAllTMPData();
                }
                StartCoroutine(LoadSceneAsync(sceneBuildIndex)); */
                
            }
    }
  /*   private IEnumerator LoadSceneAsync(string sceneName)
    {
        SceneTransition.Instance.SceneOut();
        yield return new WaitForSeconds(2f);
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
        if(sceneBuildIndex == "Level1")
        {
            Vector3 pos = new Vector3(-24.21f, 10.07f, 0f);
            SaveController.Instance.LoadSave(pos, TmpDataManager.tmpInventory, TmpDataManager.tmpHotBar, TmpDataManager.tmpListItemsInGround, TmpDataManager.tmpTime, TmpDataManager.tmpPlayer, TmpDataManager.tmpMission, false);
        }  
        if(sceneBuildIndex == "Level2")
        {
            Vector3 pos = new Vector3(-6.4f, -5.55f, 0f);
            SaveController.Instance.LoadSave(pos, TmpDataManager.tmpInventory, TmpDataManager.tmpHotBar, null, TmpDataManager.tmpTime, TmpDataManager.tmpPlayer, TmpDataManager.tmpMission, false);
        } 
        SceneTransition.Instance.SceneIn();   
    } */
}
