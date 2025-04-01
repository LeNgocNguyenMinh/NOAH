using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class DialogueController : MonoBehaviour
{
    public static DialogueController Instance;
    [SerializeField]private GameObject dialoguePanel;
    [SerializeField]private TMP_Text dialogueText, nameText;
    [SerializeField]private Image npcPortrait;
    [SerializeField]private RectTransform panel;
    [SerializeField]private Vector2 hiddenPosition;
    [SerializeField]private Vector2 visiblePosition;
    [SerializeField]private Transform choicePanel;
    [SerializeField]private GameObject choiceButtonPrefab;
    void Awake()
    {
        Instance = this;
    }
    public void ShowDialogueUI()
    { 
        dialoguePanel.SetActive(true);
    }
    public void HideDialogueUI()
    { 
        dialoguePanel.SetActive(false);
    }
    public void SetDialogue(string name, Sprite portrait)
    {
        nameText.text = name;
        npcPortrait.sprite = portrait;
    }
    public void SetDialogueText(string text)
    {
        dialogueText.text = text;
    }
    public void ClearChoice()
    {
        foreach(Transform child in panel)
        {
            Destroy(child.gameObject);
        }
    }
    public void CreateChoiceButton(string choiceText, UnityEngine.Events.UnityAction onClick)
    {
        GameObject choiceButton = Instantiate(choiceButtonPrefab, choicePanel);
        choiceButton.GetComponentInChildren<TMP_Text>().text = choiceText;
        choiceButton.GetComponent<Button>().onClick.AddListener(onClick);
    }
}
