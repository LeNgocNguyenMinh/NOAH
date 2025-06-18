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
    private MissionLine currentMissionLine;
    
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
    private DialogueMissionState missionState;
    private void Update()
    {
        if(missionState == DialogueMissionState.InMission && currentDialogueMission.isFinish == true)
        {
            missionState = DialogueMissionState.FinishMission;
        }
        objectInteraction = GetComponent<ObjectInteraction>();
        if(objectInteraction.GetCanInteract())
        {
            if(Input.GetKeyDown(KeyCode.F))
            {  
                Debug.Log(dialogueIndex+"," + CheckOptionChoice() + "," + mainLineIsTyping + "," + mainLineRespondIsTyping + "," + isChoosen);
                Interact();
            }
        }
    }
    private void Interact()
    {
        //Some UI panel is active
        if(dialogueData == null || (UIMouseAndPriority.Instance.CanOpenThisUI() == false && isDialogueActive == false))
        {
            return;
        }
        //in option choice line
        if(CheckOptionChoice() && !mainLineIsTyping && !mainLineRespondIsTyping && !isChoosen)
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
        dialogueIndex = 0;
        if(missionState == DialogueMissionState.InMission)
        {
            TypeLine(DialogueState.InQuestLineStart, "");
        }
        else if(missionState == DialogueMissionState.FinishMission)
        {
            TypeLine(DialogueState.InQuestLineFinish, "");
        }
        else
        {
            TypeLine(DialogueState.InMainLine, "");
        }
    }
    //function for write line and actione when line finish 
    private void TypeLine(DialogueState dialogueState, string fullText = "")
    {
        /* if(CheckIsMissionFinish())return; */
        //Check if last line
        if(dialogueState == DialogueState.InMainLine)
        {
            if(dialogueData.dialogueLine.Length <= dialogueIndex)
            {
                return;
            }
            else{
                mainLineIsTyping = true;
                fullText = dialogueData.dialogueLine[dialogueIndex];
            }
        }
        else if(dialogueState == DialogueState.InMainLineRespond)
        {
            mainLineRespondIsTyping = true;
        }
        else if(dialogueState == DialogueState.InQuestLineStart)
        {
            fullText = currentMissionLine.inQuestDialogue;
            questLineRespondIsTyping = true;
        }
        else{
            fullText = currentMissionLine.finishQuestDialogue;
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
                if(CheckOptionChoice())
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
                missionState = DialogueMissionState.NoMission;
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
            if(CheckOptionChoice())
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
                return;
            }
            else if(missionState == DialogueMissionState.FinishMission)
            {
                DialogueController.Instance.SetDialogueText(currentMissionLine.finishQuestDialogue);
                return;
            }
        }  
        if(!questLineRespondIsTyping && missionState == DialogueMissionState.InMission)
        {
            EndDialogue();
            return;
        }
        if(!questLineRespondIsTyping && missionState == DialogueMissionState.FinishMission)
        {
            missionState = DialogueMissionState.NoMission;
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
    private bool CheckOptionChoice()
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
    private void CheckMissionLine()
    {
        foreach(MissionLine missionLine in dialogueData.missionLine)
        {
            if(missionLine.dialogueIndex == dialogueIndex)
            {
                missionState = DialogueMissionState.InMission;
                currentMissionLine = missionLine;
                currentDialogueMission = MissionManager.Instance.GetMissionByID(missionLine.missionID);
                MissionManager.Instance.SetLineMission(missionLine.missionID);
                missionState = DialogueMissionState.InMission;
            }
        }
    }
    private bool CheckIsMissionFinish()
    {
        foreach(MissionLine missionLine in dialogueData.missionLine)
        {
            if(missionLine.dialogueIndex == dialogueIndex)
            {
                return MissionManager.Instance.GetMissionByID(missionLine.missionID).isFinish;
            }
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
        if(i == 0)
        {
            CheckMissionLine();
        }
        isChoosen = true;
        currentRespondLine = currentChoice.respond[i];
        TypeLine(DialogueState.InMainLineRespond, currentChoice.respond[i]);
        DialogueController.Instance.ClearChoice();
    }

    public void MissionFinish()
    {
        missionState = DialogueMissionState.FinishMission;
    }

}
