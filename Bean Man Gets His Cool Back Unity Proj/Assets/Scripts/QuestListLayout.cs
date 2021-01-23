using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestListLayout : MonoBehaviour
{

    public QuestList questList;

    public TextMeshPro questCompletedCount;

    public GameObject availableQuestsStartingPosition;
    public GameObject completedQuestsStartingPosition;

    public float questListDistance;
    public Vector3 lastQuestPosition;
    public float lastQuestPositionY;
    public float lastQuestPositionX;
    public Vector3 lastCompletedQuestPosition;

    public void UpdateQuestList()
    {
        lastQuestPosition = availableQuestsStartingPosition.gameObject.transform.position;
        foreach (QuestItem quest in questList.availableQuestList) {
            lastQuestPosition = new Vector3(lastQuestPosition.x, lastQuestPosition.y, 0f);
            quest.gameObject.transform.position = lastQuestPosition;
            lastQuestPosition.y -= questListDistance;
            quest.QuestActivated();
        }
    }

    public void UpdateCompletedQuestList()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/JournalActivate");

        questCompletedCount.text = "Quests Completed " + questList.completedQuestList.Count.ToString() + "/" + questList.totalQuestItemsList.Count.ToString();

        lastCompletedQuestPosition = completedQuestsStartingPosition.gameObject.transform.position;
        foreach (QuestItem quest in questList.completedQuestList) {
            lastCompletedQuestPosition = new Vector3(completedQuestsStartingPosition.gameObject.transform.position.x, lastCompletedQuestPosition.y, 0f);
            quest.gameObject.transform.position = lastCompletedQuestPosition;
            lastCompletedQuestPosition.y -= questListDistance;
            quest.QuestActivated();
            quest.QuestFinished();
        }
        if (questList.completedQuestList.Count >= 18)
        {
            questList.completedQuestList[17].QuestHide();
        }
        if (questList.completedQuestList.Count >= 19)
        {
            questList.completedQuestList[18].QuestHide();
        }
        if (questList.completedQuestList.Count >= 20)
        {
            questList.completedQuestList[19].QuestHide();
        }
        if (questList.completedQuestList.Count >= 21)
        {
            questList.completedQuestList[20].QuestHide();
        }
    }

}