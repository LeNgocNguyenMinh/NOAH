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
    private bool isTyping = false;
    public static bool isDialogueActive = false;
    public bool test;
    private ObjectInteraction objectInteraction;
    private Tween typewriterTween; 
    private DialogueChoice currentChoice;

    private void Update()
    {
        test = isDialogueActive;
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
        //Some UI panel í active
        if(dialogueData == null || (UIMouseAndPriority.Instance.CanOpenThisUI() == false && isDialogueActive == false))
        {
            return;
        }
        //in option choice line
        if(CheckOptionChoice() && !isTyping)
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

        isTyping = true;
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
            isTyping = false;
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
        if(isTyping)
        {
            typewriterTween?.Kill();
            DialogueController.Instance.SetDialogueText(dialogueData.dialogueLine[dialogueIndex]);
            isTyping = false;
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
    //hide dialogue UI
    private void EndDialogue()
    {
        typewriterTween?.Kill();
        DialogueController.Instance.HideDialogueUI();
    }
    //create option choice button
    private void DisplayChoice(DialogueChoice dialogueChoice)
    {
        for(int i = 0; i < dialogueChoice.choice.Length; i++)
        {
            int nextIndex = dialogueChoice.nextDialogueIndex[i];
            DialogueController.Instance.CreateChoiceButton(dialogueChoice.choice[i], () => ChooseOption(nextIndex));
        }
    }
    //choose option choice and process next line
    private void ChooseOption(int nextIndex)
    {
        dialogueIndex = nextIndex;
        DialogueController.Instance.ClearChoice();
        TypeLine();
    }
}
