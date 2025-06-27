using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueData dialogueToPlay;
    private bool hasTriggered = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            DialogueManager.Instance.StartDialogue(dialogueToPlay, OnDialogueEnd);
        }
    }
    void OnDialogueEnd()
    {
        Destroy(gameObject); 
    }
}