using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public string characterName;
    [TextArea(2, 5)]
    public string dialogueText;
    public Sprite characterPortrait; 
}
