using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Puzzle01Interact : MonoBehaviour
{
    private bool puzzleSolve;
    [SerializeField]private string puzzleID;
    private string passWord;
    [SerializeField]private Transform itemSpawn;
    [SerializeField]private GameObject itemPrefab;    
    private string rightPassWord = "2213";
    private void Start()
    {
        puzzleSolve = false;
        passWord = "";
    }
    public void AddToPassWord(string text)
    {
        if(text == "")
        {
            passWord = "";
            return;
        }
        passWord += text;
        Debug.Log(passWord);
        if(passWord.Length == rightPassWord.Length)
        {
            if(passWord == rightPassWord)
            {
                if(!puzzleSolve)
                {
                    CollectableItems item = Instantiate(itemPrefab, itemSpawn.position, Quaternion.identity).GetComponentInChildren<CollectableItems>();
                    Debug.Log("Spawned Item ID: " + item.GetItemID());
                    ItemInGroundController.Instance.AddNewItemInGround(item.GetItemID(), itemSpawn.position, 1);
                    puzzleSolve = true;
                    PuzzleManager.Instance.SetPuzzleSolve(puzzleID);
                    Destroy(gameObject);
                }
            }
        }
    }
}
