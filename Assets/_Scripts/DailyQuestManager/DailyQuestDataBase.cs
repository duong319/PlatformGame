using UnityEngine;

using System.Collections.Generic;

[CreateAssetMenu(fileName = "QuestDatabase", menuName = "DailyQuest/QuestDatabase")]
public class DailyQuestDatabase : ScriptableObject
{
    public List<DailyQuestData> allQuests;
}
