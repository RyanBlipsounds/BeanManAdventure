using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoading : MonoBehaviour
{
    public EndingsManager endingsManager;
    public PlayerController playerController;
    public QuestList questList;
    public GameState gameState;
    //public HideyHole hideyHole;

    public void Save()
    {
        ES3.Save<List<QuestItem>>("AvailableQuestList", questList.availableQuestList);
        ES3.Save<List<QuestItem>>("CompletedQuestList", questList.completedQuestList);
        ES3.Save<List<GameObject>>("Endings", endingsManager.endingsSeenList);
        //ES3.Save<List<GameObject>>("HideyHole", hideyHole.PeeperList);
        ES3.Save<List<NPC>>("NPCs", playerController.scriptNPCList);
        //ES3.Save<List<Quest>>("Quests", playerController.scriptNPCList);
    }

    // Update is called once per frame
    public void Load()
    {
        questList.availableQuestList = ES3.Load("AvailableQuestList", questList.availableQuestList);
        questList.completedQuestList = ES3.Load("CompletedQuestList", questList.completedQuestList);
        //hideyHole.PeeperList = ES3.Load("HideyHole", hideyHole.PeeperList);
        playerController.scriptNPCList = ES3.Load("NPCs", playerController.scriptNPCList);
        endingsManager.endingsSeenList = ES3.Load("Endings", endingsManager.endingsSeenList);

        foreach (GameObject ending in endingsManager.endingsSeenList) {
            ending.SetActive(false);
        }

        if (endingsManager.endingsSeenList.Count > 0) {
            gameState.beanState = GameState.gameState.BEANGOHINT;
        }

        foreach (NPC NPC in playerController.scriptNPCList) {
            if (NPC.gameObject.name == "Fire Hydrant") {
                NPC.gameObject.tag = "NPC";
                break;
            }
        }
    }

    public void ClearSaveData() {
        //ES3.DeleteKey("HideyHole");
        ES3.DeleteKey("AvailableQuestList");
        ES3.DeleteKey("CompletedQuestList");
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
