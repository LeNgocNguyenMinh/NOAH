using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;
    public List<PuzzleSaveData> puzzleDataList = new List<PuzzleSaveData>();
    public List<GameObject> puzzlePrefabList;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SetPuzzleSolve(string ID)
    {
        foreach (PuzzleSaveData puzzle in puzzleDataList)
        {
            if (puzzle.puzzleID == ID)
            {
                puzzle.puzzleSolve = true;
                return;
            }
        }
    }
    public List<PuzzleSaveData> GetPuzzleData()
    {
        List<PuzzleSaveData> puzzleSaveList = new List<PuzzleSaveData>();
        foreach (PuzzleSaveData puzzle in puzzleDataList)
        {
            puzzleSaveList.Add(new PuzzleSaveData{puzzleID = puzzle.puzzleID, puzzleSolve = puzzle.puzzleSolve});
        }
        return puzzleSaveList;
    }
    public void SetPuzzleData(List<PuzzleSaveData> puzzleSaveList)
    {
        foreach (GameObject puzzle in puzzlePrefabList)
        {
            Destroy(puzzle);
        }
        puzzlePrefabList.Clear();
        puzzleDataList.Clear();
        foreach (PuzzleSaveData puzzleSave in puzzleSaveList)
        {
            if(!puzzleSave.puzzleSolve)
            {
                PuzzleStatus puzzleStatus = ItemDictionary.Instance.GetPuzzleInfo(puzzleSave.puzzleID);
                puzzlePrefabList.Add(Instantiate(puzzleStatus.puzzlePrefab, puzzleStatus.puzzlePos, Quaternion.identity));
                puzzleDataList.Add(new PuzzleSaveData{puzzleID = puzzleSave.puzzleID, puzzleSolve = puzzleSave.puzzleSolve});
            }
        }
    }
}
[System.Serializable]
public class PuzzleSaveData
{
    public string puzzleID;
    public bool puzzleSolve;
}