using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestItem : MonoBehaviour
{
    public TextMeshProUGUI questItemText;
    // Start is called before the first frame update
    void Start()
    {
        //questItemText.color = 
        questItemText.text = this.gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
