
using UnityEngine;
using UnityEngine.UI;

public class QuestItemUI : MonoBehaviour
{
    public Text questTitle;
    public Image questIcon;
    public Text questDescription;
    public Slider progressSlider;
    public Button claimButton;
    public Text progressText;
    public Text rewardText;

    private DailyQuestInstance questData;

    public void Setup(DailyQuestInstance quest, System.Action<string> onClaimClicked)
    {
        questData = quest;

        questTitle.text = quest.questData.questId;
        questDescription.text = quest.questData.description;
        questIcon.sprite=quest.questData.icon;
        progressSlider.maxValue = quest.questData.goal;
        progressSlider.value = quest.progress;
        progressText.text = $"{quest.progress} / {quest.questData.goal}";
        rewardText.text = quest.questData.rewardAmount.ToString();
        claimButton.interactable = quest.isCompleted;
        claimButton.onClick.RemoveAllListeners();
        claimButton.onClick.AddListener(() =>
        {
            onClaimClicked?.Invoke(quest.questData.questId);
        });
    }

    public void UpdateProgress()
    {
        progressSlider.value = questData.progress;
        claimButton.interactable = questData.isCompleted;
        progressText.text = $"{questData.progress} / {questData.questData.goal}";
    }
}
