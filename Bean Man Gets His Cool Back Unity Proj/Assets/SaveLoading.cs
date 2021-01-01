using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoading : MonoBehaviour
{

    public EndingsManager endingsManager;
    public PlayerController playerController;

    // Start is called before the first frame update
    public void Save()
    {
        ES3.Save<List<GameObject>>("Endings", endingsManager.endingsSeenList);
        ES3.Save<List<NPC>>("NPCs", playerController.scriptNPCList);
        //ES3.Save<List<Quest>>("Quests", playerController.scriptNPCList);
    }

    // Update is called once per frame
    public void Load()
    {
        playerController.scriptNPCList = ES3.Load("NPCs", playerController.scriptNPCList);
        endingsManager.endingsSeenList = ES3.Load("Endings", endingsManager.endingsSeenList);
        foreach (GameObject ending in endingsManager.endingsSeenList) {
            ending.SetActive(false);
        }
    }

    public void ClearSaveData() {
        ES3.DeleteKey("NPCs");
        ES3.DeleteKey("Endings");
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) {
            ClearSaveData();
        }
    }
}
