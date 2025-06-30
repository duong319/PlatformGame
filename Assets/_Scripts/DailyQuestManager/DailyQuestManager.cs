using UnityEngine;
using System;
using System.Collections.Generic;

public class DailyQuestManager : MonoBehaviour
{
    public DailyQuestDatabase questDatabase;
    public int numberOfQuests = 3;
    public List<DailyQuestInstance> activeQuests = new List<DailyQuestInstance>();
    public GameObject questItemPrefab;
    public Transform questListPanel;

    private string lastResetKey = "LastResetTime";

    void Start()
    {
        LoadQuests();
        CheckReset();
        DisplayQuests();
    }

    void LoadQuests()
    {
        if (!PlayerPrefs.HasKey("SavedDailyQuests"))
        {
            GenerateNewQuests();
            return;
        }

        
        string json = PlayerPrefs.GetString("SavedDailyQuests");
        DailyQuestSaveDataWrapper wrapper = JsonUtility.FromJson<DailyQuestSaveDataWrapper>(json);

        activeQuests.Clear();
        foreach (var saved in wrapper.savedQuests)
        {
            DailyQuestData data = questDatabase.allQuests.Find(q => q.questId == saved.questId);
            if (data != null)
            {
                var instance = new DailyQuestInstance(data)
                {
                    progress = saved.progress,
                    isCompleted = saved.isCompleted,
                    isClaimed = saved.isClaimed,
                };
                activeQuests.Add(instance);
            }
        }
    }

    void GenerateNewQuests()
    {
        activeQuests.Clear();

        var shuffled = new List<DailyQuestData>(questDatabase.allQuests);
        for (int i = 0; i < shuffled.Count; i++)
        {
            int rnd = UnityEngine.Random.Range(i, shuffled.Count);
            var temp = shuffled[i];
            shuffled[i] = shuffled[rnd];
            shuffled[rnd] = temp;
        }

        for (int i = 0; i < Mathf.Min(numberOfQuests, shuffled.Count); i++)
        {
            activeQuests.Add(new DailyQuestInstance(shuffled[i]));
        }

        SaveQuests();
        PlayerPrefs.SetString(lastResetKey, DateTime.UtcNow.ToString());
    }

    void SaveQuests()
    {
        var wrapper = new DailyQuestSaveDataWrapper();
        wrapper.savedQuests = new List<DailyQuestSaveData>();

        foreach (var quest in activeQuests)
        {
            wrapper.savedQuests.Add(new DailyQuestSaveData
            {
                questId = quest.questData.questId,
                progress = quest.progress,
                isCompleted = quest.isCompleted,
                isClaimed = quest.isClaimed,
            });
        }

        string json = JsonUtility.ToJson(wrapper);
        PlayerPrefs.SetString("SavedDailyQuests", json);
    }

    void CheckReset()
    {
        if (!PlayerPrefs.HasKey(lastResetKey))
        {
            GenerateNewQuests();
            return;
        }

        DateTime lastReset = DateTime.Parse(PlayerPrefs.GetString(lastResetKey));
        if ((DateTime.UtcNow - lastReset).TotalHours >= 24)
        {
            GenerateNewQuests();
        }
    }


    public void AddProgressToQuest(string questId, int amount)
    {
        foreach (var quest in activeQuests)
        {
            if (quest.questData.questId == questId)
            {
                quest.AddProgress(amount);
                SaveQuests();
                DisplayQuests();
                break;
            }
        }
    }
    public void DisplayQuests()
    {
        if (questListPanel == null || questItemPrefab == null)
        {
            
            return;
        }

        foreach (Transform child in questListPanel)
        {
            Destroy(child.gameObject);
        }

        foreach (var quest in activeQuests)
        {
            var go = Instantiate(questItemPrefab, questListPanel);
            var ui = go.GetComponent<QuestItemUI>();
            ui.Setup(quest, OnClaimQuest);
        }
    }
    void OnClaimQuest(string questId)
    {
        var quest = activeQuests.Find(q => q.questData.questId == questId);

        if (quest != null && quest.isCompleted && !quest.isClaimed)
        {
            PlayerStats.Instance.PlayerCoin += quest.questData.rewardAmount;
            quest.isClaimed = true;
            SaveQuests();
            DisplayQuests();
        }
        else
        {
            Debug.Log("Cannot claim reward.");
        }
    }
    
    [Serializable]
    public class DailyQuestSaveData
    {
        public string questId;
        public int progress;
        public bool isCompleted;
        public bool isClaimed;
    }

    [Serializable]
    public class DailyQuestSaveDataWrapper
    {
        public List<DailyQuestSaveData> savedQuests;
    }
}