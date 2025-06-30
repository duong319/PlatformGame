
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
    public GameObject ClaimUi;
    public GameObject hasClaimedUI;

    private DailyQuestInstance questData;

    public void Setup(DailyQuestInstance quest, System.Action<string> onClaimClicked)
    {
        questData = quest;

        questTitle.text = quest.questData.questId;
        questDescription.text = quest.questData.description;
        questIcon.sprite = quest.questData.icon;

        progressSlider.maxValue = quest.questData.goal;
        progressSlider.value = quest.progress;
        progressText.text = $"{quest.progress} / {quest.questData.goal}";
        rewardText.text = quest.questData.rewardAmount.ToString();

        bool canClaim=quest.isCompleted&&!quest.isClaimed;

        claimButton.interactable = canClaim;

        if (hasClaimedUI != null&&ClaimUi!=null)
        {
            hasClaimedUI.SetActive(quest.isClaimed);
            ClaimUi.SetActive(!quest.isClaimed);
        }



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

        if (hasClaimedUI != null&&ClaimUi != null)
        {
            hasClaimedUI.SetActive(questData.isClaimed);
            ClaimUi.SetActive(!questData.isClaimed);
        }
        

    }
}
