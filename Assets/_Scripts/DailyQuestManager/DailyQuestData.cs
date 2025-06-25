using UnityEngine;
[CreateAssetMenu(fileName = "NewDailyQuest", menuName = "DailyQuest/QuestData")]
public class DailyQuestData : ScriptableObject
{
    public string questId;
    public Sprite icon;
    public string description;
    public int goal;
    public int rewardAmount;
    
}
