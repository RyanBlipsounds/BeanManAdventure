using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoading : MonoBehaviour
{
    public EndingsManager endingsManager;
    public PlayerController playerController;
    public QuestList questList;
    public GameState gameState;
    public FireHydrantVomit fireHydrantVomit;
    public HideyHole hideyHole;
    public SpriteRenderer Map;
    public GameObject VomitColliders;
    public StickLogic Stick;

    public void Save()
    {
        ES3.Save<List<QuestItem>>("AvailableQuestList", questList.availableQuestList);
        ES3.Save<List<GameObject>>("PeeperList", hideyHole.PeeperList);
        ES3.Save<List<QuestItem>>("CompletedQuestList", questList.completedQuestList);
        ES3.Save<List<GameObject>>("Endings", endingsManager.endingsSeenList);
        ES3.Save<List<NPC>>("TransitionsList", gameState.transitionsList);
        ES3.Save<List<NPC>>("CompletedTransitionList", gameState.completedTransitionsList);
        ES3.Save<SpriteRenderer>("Map", Map);
        ES3.Save<StickLogic>("Stick", Stick);
        ES3.Save<GameObject>("VomitColliders", VomitColliders);
        ES3.Save<List<NPC>>("NPCs", playerController.scriptNPCList);
        ES3.Save<int>("VomitCount", fireHydrantVomit.vomitCount);
        ES3.Save<List<GameObject>>("VomitList", fireHydrantVomit.vomitList);
    }

    public void Load()
    {
        questList.availableQuestList = ES3.Load("AvailableQuestList", questList.availableQuestList);
        questList.completedQuestList = ES3.Load("CompletedQuestList", questList.completedQuestList);
        hideyHole.PeeperList = ES3.Load("PeeperList", hideyHole.PeeperList);
        VomitColliders = ES3.Load("VomitColliders", VomitColliders);
        Map = ES3.Load("Map", Map);
        Stick = ES3.Load("Stick", Stick);
        gameState.transitionsList = ES3.Load("TransitionsList", gameState.transitionsList);
        gameState.completedTransitionsList = ES3.Load("CompletedTransitionList", gameState.completedTransitionsList);
        playerController.scriptNPCList = ES3.Load("NPCs", playerController.scriptNPCList);
        endingsManager.endingsSeenList = ES3.Load("Endings", endingsManager.endingsSeenList);
        fireHydrantVomit.vomitCount = ES3.Load("VomitCount", fireHydrantVomit.vomitCount);
        fireHydrantVomit.vomitList = ES3.Load("VomitList", fireHydrantVomit.vomitList);

        foreach (GameObject ending in endingsManager.endingsSeenList) {
            ending.SetActive(false);
        }

        int count;
        count = 0;
        if (endingsManager.endingsSeenList.Count > 0)
        {
            foreach (GameObject endingsFound in endingsManager.endingsSeenList)
            {
                Debug.Log("Endings count!");
                if (!endingsFound.gameObject.name.Contains("Beanman"))
                {
                    count++;
                }
            }
        }

        if (count > 0) {
            gameState.beanState = GameState.gameState.BEANGOHINT;
        }

        foreach (NPC NPC in playerController.scriptNPCList) {
            if (NPC.gameObject.name == "Fire Hydrant") {
                FireHydrantVomit vomitScript = NPC.gameObject.GetComponent<FireHydrantVomit>();
                vomitScript.fireHydrantActivated = true;
                NPC.gameObject.tag = "NPC";
                break;
            }
        }
    }

    public void ClearSaveData() {
        //ES3.DeleteKey("HideyHole");
        ES3.DeleteKey("Stick");
        ES3.DeleteKey("VomitColliders");
        ES3.DeleteKey("TransitionsList");
        ES3.DeleteKey("CompletedTransitionList");
        ES3.DeleteKey("AvailableQuestList");
        ES3.DeleteKey("CompletedQuestList");
        ES3.DeleteKey("PeeperList");
        ES3.DeleteKey("NPCs");
        ES3.DeleteKey("Map");
        ES3.DeleteKey("Endings");
        ES3.DeleteKey("VomitCount");
        ES3.DeleteKey("VomitList");
        gameState.StartMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    
}
