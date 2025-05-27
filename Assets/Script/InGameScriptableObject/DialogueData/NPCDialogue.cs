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
}
[System.Serializable]
public class DialogueChoice
{
    public int dialogueIndex;
    public string[] choice;
    public string[] respond;
}