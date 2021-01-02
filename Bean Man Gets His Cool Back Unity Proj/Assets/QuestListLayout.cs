using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestListLayout : MonoBehaviour
{

    public QuestList questList;

    public GameObject availableQuestsStartingPosition;
    public GameObject completedQuestsStartingPosition;

    public float questListDistance;
    public Vector3 lastQuestPosition;
    // Start is called before the first frame update
    public void Start()
    {
        
    }

    public void UpdateQuestList()
    {
        lastQuestPosition = availableQuestsStartingPosition.gameObject.transform.position;
        foreach (QuestItem quest in questList.availableQuestList) {
            quest.gameObject.transform.position = lastQuestPosition;
            lastQuestPosition = new Vector3(lastQuestPosition.x, lastQuestPosition.y, 0f);
            lastQuestPosition.y -= questListDistance;
            Debug.Log(lastQuestPosition);
        }
        //questList.availableQuestList;
        //questList.completedQuestList;
        //questList.questItemsList;
    }

    public void UpdateCompletedQuestList()
    {
        lastQuestPosition = completedQuestsStartingPosition.gameObject.transform.position;
        foreach (QuestItem quest in questList.completedQuestList)
        {
            quest.gameObject.transform.position = lastQuestPosition;
            lastQuestPosition = new Vector3(lastQuestPosition.x, lastQuestPosition.y, 0f);
            lastQuestPosition.y -= questListDistance;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            UpdateCompletedQuestList();
            UpdateQuestList();
        }
    }
}
