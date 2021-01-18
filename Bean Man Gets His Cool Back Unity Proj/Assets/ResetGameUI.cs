using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetGameUI : MonoBehaviour
{
    public Button AcceptResetBTN;
    public SaveLoading saveLoading; 

    // Start is called before the first frame update
    void Start()
    {
        AcceptResetBTN.onClick.AddListener(ResetGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ResetGame()
    {
        saveLoading.ClearSaveData();
        gameObject.SetActive(false);
    }
}
