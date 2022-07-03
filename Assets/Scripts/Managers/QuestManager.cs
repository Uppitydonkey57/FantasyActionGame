using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Yarn.Unity;

[CreateAssetMenu(fileName = "QuestManager", menuName = "Managers/QuestManager")]
public class QuestManager : ScriptableObject
{
    private List<Quest> questList;
    private List<Quest> finishedQuests;
    private static List<Quest> finishedQuestsStatic;

    [SerializeField] private ItemCollection inventory;

    [HideInInspector] public UnityEvent<Quest> questFinished;

    [SerializeField] private EnemyManager enemyManager;

    private static UnityEvent<string> questCheck;

    private void OnEnable()
    {
        questList = new List<Quest>();
        finishedQuests = new List<Quest>();
        finishedQuestsStatic = new List<Quest>();

        questFinished = new UnityEvent<Quest>();

        questCheck = new UnityEvent<string>();
        questCheck.AddListener(CheckQuestDialogue);

        inventory.itemObtained.AddListener(CheckQuestItems);
        enemyManager.enemyKilled.AddListener(CheckQuestEnemies);
    }

    public void AddQuest(Quest quest)
    {
        questList.Add(quest);
        quest.questFinished.AddListener(OnQuestFinished);
    }

    void CheckQuestItems(Item item)
    {
        foreach (Quest quest in questList)
        {
            quest.ItemCheck(item);
        }
    }

    void CheckQuestEnemies(string[] tags)
    {
        foreach (Quest quest in questList)
        {
            quest.EnemyKilled(tags);
        }
    }


    [YarnCommand("complete_quest")]
    public static void YarnDialogueQuest(string questName)
    {
        questCheck.Invoke(questName);
    }

    public void CheckQuestDialogue(string questName)
    {
        foreach (Quest quest in questList.ToArray())
        {
            quest.DialogueCheck(questName);
        }
    }

    void OnQuestFinished(Quest quest)
    {
        questFinished.Invoke(quest);
        finishedQuests.Add(quest);
        questList.Add(quest.followupQuest);
        questList.Remove(quest);
        finishedQuestsStatic.Add(quest);
    }

    [YarnFunction("quest_complete")]
    public static bool QuestCompleteCheck(string questName)
    {
        foreach (Quest quest in finishedQuestsStatic)
        {
            if (quest.questName == questName)
            {
                return true;
            }
        }

        return false;
    }
}
