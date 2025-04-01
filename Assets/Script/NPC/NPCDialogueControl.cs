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
    private bool isTyping, isDialogueActive = false;
    private UIMouseAndPriority uiMouseAndPriority;
    private ObjectInteraction objectInteraction;
    private Tween typewriterTween; 

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
        uiMouseAndPriority = FindObjectOfType<UIMouseAndPriority>().GetComponent<UIMouseAndPriority>();
        if(dialogueData == null || uiMouseAndPriority.CanOpenThisUI() == false)
        {
            return;
        }
        if(isDialogueActive)
        {
            NextLine();
        }
        else{
            StartDialogue();
        }
    }
    private void StartDialogue()
    {
        DialogueController.Instance.ShowDialogueUI();
        isDialogueActive = true;
        DialogueController.Instance.SetDialogue(dialogueData.npcName, dialogueData.npcPortrait);
        dialogueIndex = 0;
        Time.timeScale = 0f;
        TypeLine();
    }
    private void TypeLine()
    {
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
            // Xử lý tự động chuyển dòng
            if (dialogueData.autoProgressLine.Length > dialogueIndex && dialogueData.autoProgressLine[dialogueIndex])
            {
                DOVirtual.DelayedCall(dialogueData.autoProgressDelay, () => NextLine());
            }
        });
    }

    private void NextLine()
    {
        if(isTyping)
        {
            // Nếu đang gõ, hủy Tween và hiển thị toàn bộ text
            typewriterTween?.Kill();
            DialogueController.Instance.SetDialogueText(dialogueData.dialogueLine[dialogueIndex]);
            isTyping = false;
        }
        DialogueController.Instance.ClearChoice();
        if(dialogueData.endDialogueLine.Length > dialogueIndex && dialogueData.endDialogueLine[dialogueIndex])
        {
            EndDialogue();
            return;
        }
        foreach(DialogueChoice choice in dialogueData.choice)
        {
            if(choice.dialogueIndex == dialogueIndex)
            {
                DisplayChoice(choice);
                return;
            }
        }
        if(++dialogueIndex < dialogueData.dialogueLine.Length)
        {
            TypeLine();
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        typewriterTween?.Kill();
        DialogueController.Instance.HideDialogueUI();
        isDialogueActive = false;
        Time.timeScale = 1f;
    }
    private void DisplayChoice(DialogueChoice dialogueChoice)
    {
        for(int i = 0; i < dialogueChoice.choice.Length; i++)
        {
            int nextIndex = dialogueChoice.nextDialogueIndex[i];
            DialogueController.Instance.CreateChoiceButton(dialogueChoice.choice[i], );
        }
    }
    private void ChooseOption(int nextIndex)
    {
        dialogueIndex = nextIndex;
        DialogueController.Instance.ClearChoice();
    }
    private void DisplayCurrentLine()
    {
        
    }
}
