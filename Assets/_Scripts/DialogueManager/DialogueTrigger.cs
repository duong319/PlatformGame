using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public string tutorialKey = "HasSeenTutorial";
    public DialogueData dialogueToPlay;
    private bool hasTriggered = false;


    //private void Start()
    //{
        
    //    if (PlayerPrefs.GetInt(tutorialKey, 0) == 1)
    //    {
    //        Destroy(gameObject);
    //    }
    //}
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
        PlayerPrefs.SetInt(tutorialKey, 1);
        PlayerPrefs.Save();
        Destroy(gameObject); 
    }
}