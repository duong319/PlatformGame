using UnityEngine;
[System.Serializable]
public class DailyQuestInstance 
{
    public DailyQuestData questData;
    public int progress;
    public bool isCompleted;

    public DailyQuestInstance(DailyQuestData data)
    {
        questData = data;
        progress = 0;
        isCompleted = false;
    }

    public void AddProgress(int amount)
    {
        if (isCompleted) return;
        progress += amount;
        if (progress >= questData.goal)
        {
            isCompleted = true;
        }
    }
}
