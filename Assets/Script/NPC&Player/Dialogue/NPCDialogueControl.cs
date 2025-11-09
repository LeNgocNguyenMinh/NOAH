using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening; 

public class NPCDialogueControl : MonoBehaviour
{
    [SerializeField]private NPCDialogue dialogueData;
    private int dialogueIndex;
    public static bool isDialogueActive = false;
    private ObjectInteraction objectInteraction;
    private Tween typewriterTween; 
    private DialogueChoice currentChoice;
    private string currentRespondLine;
    private bool mainLineIsTyping = false;
    private bool mainLineRespondIsTyping = false;
    private bool questLineRespondIsTyping = false;
    private bool isChoosen = false;
    private Mission currentDialogueMission = null;
    private MissionLine currentMissionLine = null;
    
    private enum DialogueMissionState
    {
        NoMission,
        InMission,
        FinishMission
    }
    private enum DialogueState
    {
        InMainLine,
        InMainLineRespond,
        InQuestLineStart,
        InQuestLineFinish
    }
    private DialogueMissionState missionState = DialogueMissionState.NoMission;
    private void Update()
    {
        objectInteraction = GetComponent<ObjectInteraction>();
        if(objectInteraction.GetCanInteract())
        {
            if(Input.GetKeyDown(KeyCode.F))
            {  
                Interact();
            }
        }
    }
    private void Interact()
    {
        //Some UI panel is active, check if other panel is active, yes -> return, no -> continue 
        if(dialogueData == null//no data  
        || (UIMouseAndPriority.Instance.OtherPanelIsActive() && isDialogueActive == false))//
        {
            return;
        }
        //if in option choice line and previous line done typing and player not choose yet, return
        if(IsOptionChoice() && !mainLineIsTyping && !mainLineRespondIsTyping && !isChoosen)
        {
            return;
        }
        //in normal line
        if(isDialogueActive)
        {
            NextLine();
        }//in first line
        else{
            StartDialogue();
        }
    }
    private void StartDialogue()
    {
        DialogueController.Instance.ShowDialogueUI();
        DialogueController.Instance.SetDialogue(dialogueData.npcName, dialogueData.npcPortrait);

        if(missionState == DialogueMissionState.InMission)
        {
            if(IsMissionFinish())
            {
                Debug.Log(0111111);
                TypeLine(DialogueState.InQuestLineFinish, currentMissionLine.finishQuestDialogue);
                return;
            }
            else 
            {
                Debug.Log(02222);
                TypeLine(DialogueState.InQuestLineStart, currentMissionLine.inQuestDialogue);
                return;
            }
        }
        else if(missionState == DialogueMissionState.FinishMission)
        {
            dialogueIndex = dialogueData.dialogueLine.Length - 1;
            TypeLine(DialogueState.InMainLine, "");
        }
        else 
        {
            dialogueIndex = 0;
            TypeLine(DialogueState.InMainLine, "");
        }
    }
    //function for write line and actione when line finish 
    private void TypeLine(DialogueState dialogueState, string fullText = "")
    {
        //Check if last line
        if(dialogueState == DialogueState.InMainLine)
        {
            if(dialogueData.dialogueLine.Length <= dialogueIndex)
            {
                return;
            }
            else{
                if(IsMissionLine())
                {
                    if(IsMissionFinish())
                    {
                        Debug.Log(11465465456);
                        dialogueIndex++;
                    }
                    else
                    {
                        missionState = DialogueMissionState.InMission;
                        currentDialogueMission = MissionManager.Instance.GetMissionByID(currentMissionLine.missionID);
                        MissionManager.Instance.SetLineMission(currentMissionLine.missionID);
                    }
                }
                mainLineIsTyping = true;
                fullText = dialogueData.dialogueLine[dialogueIndex];
            }
        }
        //Respond choice line
        else if(dialogueState == DialogueState.InMainLineRespond)
        {
            mainLineRespondIsTyping = true;
        }
        //In quest Line
        else if(dialogueState == DialogueState.InQuestLineStart)
        {
            questLineRespondIsTyping = true;
        }
        //In quest Line finish
        else if(dialogueState == DialogueState.InQuestLineFinish)
        {
            questLineRespondIsTyping = true;
        }
        
        DialogueController.Instance.SetDialogueText("");
        // Tạo hiệu ứng typewriter bằng DOTween và DOVirtual
        typewriterTween = DOTween.To(
            () => 0, // Giá trị bắt đầu
            (currentIndex) => 
            {
                DialogueController.Instance.SetDialogueText(fullText.Substring(0, currentIndex));
            },
            fullText.Length, // Giá trị kết thúc
            fullText.Length * dialogueData.typingSpeed // Thời gian dựa trên số ký tự và tốc độ
        )
        .SetEase(Ease.Linear)
        .SetUpdate(true)
        .OnComplete(() => 
        {
            if(dialogueState == DialogueState.InMainLine)
            {
                mainLineIsTyping = false;
            // check if current line is option choice line
                if(IsOptionChoice())
                {
                    DialogueController.Instance.ClearChoice();
                    DisplayChoice(currentChoice);
                }
            } 
            else if(dialogueState == DialogueState.InMainLineRespond)
            {
                mainLineRespondIsTyping = false;
            }     
            else if(dialogueState == DialogueState.InQuestLineStart)
            {
                questLineRespondIsTyping = false;
            }
            else if(dialogueState == DialogueState.InQuestLineFinish)
            {
                questLineRespondIsTyping = false;
                missionState = DialogueMissionState.FinishMission;
            }      
        });
    }

    private void NextLine()
    {
        // if typing,show the full line
        if(mainLineRespondIsTyping)
        {
            typewriterTween?.Kill();
            mainLineRespondIsTyping = false;
            DialogueController.Instance.SetDialogueText(currentRespondLine);
            return;
        }
        if(mainLineIsTyping)
        {
            typewriterTween?.Kill();
            DialogueController.Instance.SetDialogueText(dialogueData.dialogueLine[dialogueIndex]);
            mainLineIsTyping = false;
            if(IsOptionChoice())
            {
                DialogueController.Instance.ClearChoice();
                DisplayChoice(currentChoice);
            }
            return;
        }  
        if(questLineRespondIsTyping)
        {
            typewriterTween?.Kill();
            questLineRespondIsTyping = false;
            if(missionState == DialogueMissionState.InMission)
            {
                DialogueController.Instance.SetDialogueText(currentMissionLine.inQuestDialogue);
            }
            else if(missionState == DialogueMissionState.FinishMission)
            {
                DialogueController.Instance.SetDialogueText(currentMissionLine.finishQuestDialogue);
            }
        }  
        if(!questLineRespondIsTyping && missionState == DialogueMissionState.InMission)
        {
            EndDialogue();
            return;
        }
        if(!questLineRespondIsTyping && missionState == DialogueMissionState.FinishMission)
        {
            EndDialogue();
            return;
        }
        //if this is the finish line then end the dialogue 
        if(dialogueData.endDialogueLine.Length > dialogueIndex && dialogueData.endDialogueLine[dialogueIndex])
        {
            EndDialogue();
            return;
        }
        //if this this not the last line then continue
        if(++dialogueIndex < dialogueData.dialogueLine.Length)
        {
            TypeLine(DialogueState.InMainLine, "");
        }//else end dialogue
        else
        {
            EndDialogue();
        }
    }
    //check if current line is option choice line
    private bool IsOptionChoice()
    {
        foreach(DialogueChoice choice in dialogueData.choice)
        {
            if(choice.dialogueIndex == dialogueIndex)
            {
                currentChoice = choice;
                return true;
            }
        }
        return false;
    }
    private bool IsMissionLine()
    {
        foreach(MissionLine missionLine in dialogueData.missionLine)
        {
            if(missionLine.dialogueIndex == dialogueIndex)
            {
                currentMissionLine = missionLine;
                return true;
            }
        }
        return false;
    }
    private bool IsMissionFinish()
    {
        if(MissionManager.Instance.IsMissionFinish(currentMissionLine.missionID))
        {
            missionState = DialogueMissionState.InMission;
            return true;
        }
        return false;
    }
    //hide dialogue UI
    private void EndDialogue()
    {
        typewriterTween?.Kill();
        DialogueController.Instance.HideDialogueUI();
    }
    //create option choice button
    private void DisplayChoice(DialogueChoice dialogueChoice)
    {
        isChoosen = false;
        for(int i = 0; i < dialogueChoice.choice.Length; i++)
        {
            int index = i;
            DialogueController.Instance.CreateChoiceButton(dialogueChoice.choice[i], () => ChooseOption(index));
        }
    }
    //choose option choice and process next line
    private void ChooseOption(int i)
    {
        /* if(IsMissionLine())
        {
            if(i == 0)
            {
                currentDialogueMission = MissionManager.Instance.GetMissionByID(currentMissionLine.missionID);
                MissionManager.Instance.SetLineMission(currentMissionLine.missionID);
            }
            else{
                missionState = DialogueMissionState.NoMission;
            }
        } */
        
        isChoosen = true;
        currentRespondLine = currentChoice.respond[i];
        TypeLine(DialogueState.InMainLineRespond, currentChoice.respond[i]);
        DialogueController.Instance.ClearChoice();
    }
}
