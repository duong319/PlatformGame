using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI; 

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Image characterImage;
    public GameObject dialoguePanel;

    private DialogueLine[] currentLines;
    private int currentIndex;
    private bool isTyping;
    public float typingSpeed = 0.04f;

    public static DialogueManager Instance;
    private System.Action onDialogueComplete;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void StartDialogue(DialogueData dialogueData, System.Action onComplete = null)
    {
        currentLines = dialogueData.lines;
        currentIndex = 0;
        dialoguePanel.SetActive(true);
        Time.timeScale = 0f;
        onDialogueComplete = onComplete;
        StartCoroutine(TypeLine());
    }

    private void Update()
    {
        if (dialoguePanel.activeSelf && Input.GetKeyDown(KeyCode.S))
        {
            if (isTyping)
            {
                StopAllCoroutines();
                dialogueText.text = currentLines[currentIndex].dialogueText;
                isTyping = false;
            }
            else
            {
                NextLine();
            }
        }
    }

    void NextLine()
    {
        currentIndex++;
        if (currentIndex < currentLines.Length)
        {
            StartCoroutine(TypeLine());
        }
        else
        {
            dialoguePanel.SetActive(false);
            Time.timeScale = 1f;
            if (onDialogueComplete != null)
                onDialogueComplete.Invoke();
        }
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        DialogueLine line = currentLines[currentIndex];
        nameText.text = line.characterName;
        dialogueText.text = "";
        characterImage.sprite = line.characterPortrait;

        foreach (char c in line.dialogueText)
        {
            dialogueText.text += c;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }

        isTyping = false;
    }

}