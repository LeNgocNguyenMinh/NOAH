using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle01Interact : MonoBehaviour
{
    private bool chestIsSpawn;
    private string passWord = "";
    [SerializeField]private Transform chestSpawn;
    [SerializeField]private GameObject chestPrefab;

    [SerializeField]private LightOnTopInteractive[] listLightOnTop;
    private string rightPassWord = "22134";
    private void Start()
    {
        chestPrefab.SetActive(false);
        chestIsSpawn = false;
    }

    public void AddToPassWord(string text)
    {
        if(passWord.Length < rightPassWord.Length)
        {
            if(text == "")
            {
                passWord = "";
                return;
            }
            passWord += text;
        }
        else if(passWord.Length == 5)
        {
            if(passWord != rightPassWord)
            {
                passWord = "";
                foreach(LightOnTopInteractive x in listLightOnTop)
                {
                    x.LightOnTopDeactive();
                }
            }
            else 
            {
                if(!chestIsSpawn)
                {
                    chestIsSpawn = true;
                    chestPrefab.SetActive(true);
                }           
            }
        }
        Debug.Log("Pass word: " + passWord);
    }
}
