using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestList : MonoBehaviour
{

    public List<QuestItem> totalQuestItemsList = new List<QuestItem>();
    public List<QuestItem> availableQuestList = new List<QuestItem>();
    public List<QuestItem> completedQuestList = new List<QuestItem>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ActivateQuestItem("Queen");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ActivateQuestItem("Butter");
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            ActivateQuestItem("Lina Bean");
            ActivateQuestItem("Fire Hydrant");
            ActivateQuestItem("Glasses");
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            CompleteQuestItem("Queen");
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            CompleteQuestItem("Butter");
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            CompleteQuestItem("Lina Bean");
        }
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

        availableQuestList.Add(selectedQuest);
        selectedQuest.QuestActivated();
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

        availableQuestList.Remove(selectedQuest);
        completedQuestList.Add(selectedQuest);
        selectedQuest.QuestFinished();
    }

    public void ShowQuestCompletedPopUp() {
        //Shows same prompt but says "Quest Completed"
    }

    public void ShowNewQuestsAvailablePopUp()
    {
        //Pop up that prompts the player to open up their quest journal that says "New Quests Available"
    }
    public void ShowQuestList() {

    }
}
