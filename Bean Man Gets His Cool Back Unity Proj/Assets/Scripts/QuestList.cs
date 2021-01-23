using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestList : MonoBehaviour
{

    public List<QuestItem> totalQuestItemsList = new List<QuestItem>();
    public List<QuestItem> availableQuestList = new List<QuestItem>();
    public List<QuestItem> completedQuestList = new List<QuestItem>();

    public QuestNotification questNotification;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) {
            //CompleteQuestItem("Lina Bean");
        }
    }

    public void RemoveQuestItem(string characterName)
    {
        QuestItem selectedQuest = totalQuestItemsList[0];
        bool foundQuest = false;

        foreach (QuestItem quest in totalQuestItemsList)
        {
            if (quest.gameObject.name.Contains(characterName))
            {
                selectedQuest = quest;
                foundQuest = true;
                break;
            }
        }

        if (!foundQuest)
        {
            return;
        }

        if (completedQuestList.Contains(selectedQuest) || !availableQuestList.Contains(selectedQuest))
        {
            return;
        }

        availableQuestList.Remove(selectedQuest);
        selectedQuest.QuestRemoved();
    }

    public void ActivateQuestItem(string characterName)
    {
        QuestItem selectedQuest = totalQuestItemsList[0];
        bool foundQuest = false;

        foreach (QuestItem quest in totalQuestItemsList)
        {
            if (quest.gameObject.name.Contains(characterName))
            {
                selectedQuest = quest;
                foundQuest = true;
                break;
            }
        }

        if (!foundQuest)
        {
            return;
        }

        if (completedQuestList.Contains(selectedQuest) || availableQuestList.Contains(selectedQuest)) {
            return;
        }

        FMODUnity.RuntimeManager.PlayOneShot("event:/NewQuest", this.transform.position);
        availableQuestList.Insert(0, selectedQuest);
        selectedQuest.QuestActivated();
        questNotification.isActive = true;
    }

    public void CompleteQuestItem(string characterName)
    {
        QuestItem selectedQuest = totalQuestItemsList[0];
        bool foundQuest = false;

        foreach (QuestItem quest in totalQuestItemsList) {
            if (quest.gameObject.name.Contains(characterName)) {
                selectedQuest = quest;
                foundQuest = true;
                break;
            }
        }

        if (!foundQuest) {
            return;
        }

        if (completedQuestList.Contains(selectedQuest)) {
            return;
        }

        if (!availableQuestList.Contains(selectedQuest))
        {
            ActivateQuestItem(characterName);
        }

        FMODUnity.RuntimeManager.PlayOneShot("event:/NewQuest", this.transform.position);
        availableQuestList.Remove(selectedQuest);
        completedQuestList.Insert(0, selectedQuest);
        selectedQuest.QuestFinished();
        questNotification.isActive = true;
    }
}
