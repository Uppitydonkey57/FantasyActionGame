using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Managers/QuestList")]
public class QuestList : ScriptableObject
{
    public List<Quest> quests = new List<Quest>();

    public QuestManager questManager;

    public static UnityEvent<string> questAdded;

    private void OnEnable()
    {
        questAdded = new UnityEvent<string>();
        questAdded.AddListener(AddQuest);
    }

    [YarnCommand("assign_quest")]
    public static void AssignQuest(string questName)
    {
        questAdded.Invoke(questName);
        Debug.Log(questName);
    }

    void AddQuest(string questName)
    {
        foreach (Quest quest in quests)
        {
            if (quest.questName == questName)
            {
                questManager.AddQuest(quest);
            }
        }
    }
}
