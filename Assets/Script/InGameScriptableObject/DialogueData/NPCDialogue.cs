using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewNPCDialogue", menuName = "NPCDialogue")]
public class NPCDialogue : ScriptableObject
{
    public string npcName;
    public Sprite npcPortrait;
    public string[] dialogueLine;
    public bool[] endDialogueLine;
    public float typingSpeed;
    public DialogueChoice[] choice;
    public MissionLine[] missionLine;
}
[System.Serializable]
public class DialogueChoice
{
    public int dialogueIndex;
    public string[] choice;
    public string[] respond;
}
[System.Serializable]
public class MissionLine
{
    public int dialogueIndex;
    public Mission mission;
    public string inQuestDialogue;
    public string finishQuestDialogue;
}