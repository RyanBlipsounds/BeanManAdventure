using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestItem : MonoBehaviour
{
    public TextMeshProUGUI questItemText;
    public GameObject strikeThrough;

    public bool completed = false;
    public bool activated = false;

    void Start()
    {
        questItemText.text = this.gameObject.name;
        questItemText.faceColor = Color.black;
        questItemText.enabled = false;
        strikeThrough.SetActive(false);
        if (activated == true) {
            if (completed == true)
            {
                QuestFinished();
            }
            else
            {
                questItemText.faceColor = Color.black;
            }
        }
    }

    public void QuestFinished() {
        completed = true;
        questItemText.faceColor = Color.gray;
    }
    
    public void QuestActivated()
    {
        questItemText.faceColor = Color.black;
        questItemText.enabled = true;
    }

    public void QuestRemoved() {
        questItemText.faceColor = Color.black;
        questItemText.enabled = false;
    }

}
