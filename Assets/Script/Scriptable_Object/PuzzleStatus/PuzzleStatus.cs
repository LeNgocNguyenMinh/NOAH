using UnityEngine;

[CreateAssetMenu(fileName = "NewPuzzle", menuName = "Puzzle")]
public class PuzzleStatus : ScriptableObject
{
    public string puzzleID;
    public Vector3 puzzlePos;
    public GameObject puzzlePrefab;
}
