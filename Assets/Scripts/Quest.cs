using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

[CreateAssetMenu()]
public class Quest : ScriptableObject
{
    public string questName;
    public string description;

    private enum QuestType { Kill, Item, Conversation }
    [SerializeField] private QuestType questType;

    [SerializeField] private int killsNeeded;
    private int killsLeft;
    [SerializeField] private string tag;

    [SerializeField] private Item requiredItem;

    public Quest followupQuest;

    [HideInInspector] public UnityEvent<Quest> questFinished;

    public QuestList questList;

    [HideInInspector] public bool verified;
    
    private void OnEnable()
    {
        killsLeft = killsNeeded;

        questFinished = new UnityEvent<Quest>();
    }

    public void EnemyKilled(string[] killedTags)
    {
        if (questType == QuestType.Kill)
        {
            foreach (string killedTag in killedTags)
                if (killedTag == tag)
                {
                    killsLeft--;
                    Debug.Log("AHHHHH");
                }

            if (killsLeft <= 0)
            {
                QuestFinished();
            }
        }
    }

    public void ItemCheck(Item obtainedItem)
    {
        if (questType == QuestType.Item)
        {
            if (obtainedItem == requiredItem)
            {
                QuestFinished();
            }
        }
    }

    public void DialogueCheck(string checkName)
    {
        if (questType == QuestType.Conversation)
        {
            if (questName == checkName)
            {
                QuestFinished();
            }
        }
    }

    void QuestFinished()
    {
        questFinished.Invoke(this);
        Debug.Log("Finished Quest " + questName);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(Quest))]
public class QuestObjectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Quest quest = (Quest)target;

        if (GUILayout.Button("Add To List"))
        {
            if (quest.questList != null)
            {
                if (quest.verified)
                {
                    Debug.LogError("This quest has already been added to the list.");
                } else
                {
                    bool questError = false;

                    foreach (Quest listQuest in quest.questList.quests)
                    {
                        if (listQuest.questName == quest.questName)
                        {
                            Debug.LogError("A quest with this same name exists please change it.");
                            questError = true;
                        } 
                    }

                    if (!questError)
                    {
                        quest.questList.quests.Add(quest);
                        quest.verified = true;
                        Debug.Log("Added to list.");
                    }
                }
            } else
            {
                Debug.LogError("Quest List has not been assigned.");
            }
        }
    }
}
#endif

/*
 * #if UNITY_EDITOR
[CustomEditor(typeof(Quest))]
public class QuestObjectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Quest quest = (Quest)target;

        if (GUILayout.Button("Add To List"))
        {
            if (quest.questList != null)
            {
                bool questError = false;

                foreach (Quest listQuest in quest.questList.quests)
                {
                    if (listQuest.questName == quest.questName && listQuest != quest)
                    {
                        Debug.LogError("A quest with this same name exists please change it.");
                        questError = true;
                    } 
                }

                if (!questError)
                {
                    if (!quest.verified)
                    {
                        quest.questList.quests.Add(quest);
                        quest.verified = true;
                        Debug.Log("Added to list.");
                    } else
                    {
                        Debug.LogError("This quest has already been added to the list.");
                    }
                }
            } else
            {
                Debug.LogError("Quest List has not been assigned.");
            }
        }
    }
}
#endif
 */