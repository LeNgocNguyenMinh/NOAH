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
    private bool mainLineIsTyping = false;
    public static bool isDialogueActive = false;
    public bool test;
    private ObjectInteraction objectInteraction;
    private Tween typewriterTween; 
    private DialogueChoice currentChoice;
    private string currentRespondLine;
    private bool respondLineIsTyping = false;
    private bool isChoosen = false;

    private void Update()
    {
        test = isDialogueActive;
        objectInteraction = GetComponent<ObjectInteraction>();
        if(objectInteraction.GetCanInteract())
        {
            if(Input.GetKeyDown(KeyCode.F))
            {  
                Debug.Log(dialogueIndex+"," + CheckOptionChoice() + "," + mainLineIsTyping + "," + respondLineIsTyping);
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
        if(CheckOptionChoice() && !mainLineIsTyping && !respondLineIsTyping && !isChoosen)
        {
            return;
        }
        //in normal line
        if(isDialogueActive)
        {
            Debug.Log(1);
            NextLine();
        }//in first line
        else{
            Debug.Log(2);
            StartDialogue();
        }
    }
    private void StartDialogue()
    {
        DialogueController.Instance.ShowDialogueUI();
        DialogueController.Instance.SetDialogue(dialogueData.npcName, dialogueData.npcPortrait);
        dialogueIndex = 0;
        TypeLine();
    }
    //function for write line and actione when line finish 
    private void TypeLine()
    {
        
        //Check if last line
        if (dialogueData.dialogueLine.Length <= dialogueIndex) return;

        mainLineIsTyping = true;
        string fullText = dialogueData.dialogueLine[dialogueIndex];
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
            mainLineIsTyping = false;
            // check if current line is option choice line
            if(CheckOptionChoice())
            {
                DialogueController.Instance.ClearChoice();
                DisplayChoice(currentChoice);
            }
            
        });
    }

    private void NextLine()
    {
        // if typing,show the full line
        if(respondLineIsTyping)
        {
            typewriterTween?.Kill();
            respondLineIsTyping = false;
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
        //if this is the finish line then end the dialogue 
        if(dialogueData.endDialogueLine.Length > dialogueIndex && dialogueData.endDialogueLine[dialogueIndex])
        {
            EndDialogue();
            return;
        }
        //if this this not the last line then continue
        if(++dialogueIndex < dialogueData.dialogueLine.Length)
        {
            TypeLine();
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
                MissionManager.Instance.SetLineMission(missionLine.mission);
            }
        }
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
        RespondLine(currentChoice.respond[i]);
        DialogueController.Instance.ClearChoice();
    }
    private void RespondLine(string line)
    {
        respondLineIsTyping = true;
        string fullText = line;
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
            respondLineIsTyping = false;
        });
    }
}
