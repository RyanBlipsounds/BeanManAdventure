using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    // The game's UI
    [SerializeField]
    private UILogic UIScript = default;

    [SerializeField]
    private PlayerController _playerController;
    public NPC _npc = default;
    public ActManager _actManager = default;

    public List<GameObject> listTotalNPC = new List<GameObject>();

    public GameState m_gameState = null;
    public enum gameState
    {
        ISCOOL,
        ISNOTCOOL,
        ISBAGGED,
        BEANGOHINT,
        MYGLASSES
    }

    public gameState beanState = gameState.ISCOOL;
    private string characterConversation = default;
    public Dictionary<string, string> conversationDict = new Dictionary<string, string>();
    private bool talkedChickpea = false;
    private bool talkedGranny = false;
    private bool talkedLina = false;

    // Start is called before the first frame update
    void Start()
    {
        listTotalNPC.AddRange(GameObject.FindGameObjectsWithTag("NPC"));

        conversationDict.Add("ISCOOL", "ISCOOL");
        conversationDict.Add("ISNOTCOOL", "ISNOTCOOL");
        conversationDict.Add("ISBAGGED", "ISBAGGED");
        conversationDict.Add("BEANGOHINT", "BEANGOHINT");
        conversationDict.Add("MYGLASSES", "MYGLASSES");
        conversationDict.Add("ENDCONVO", "ENDCONVO");

        //UIScript.State.text = "IS COOL!";
    }

    public void Conversation(string gameObjectName) {
        if (gameObjectName == "Granny Smith") {
            conversationDict["ISCOOL"] = "Wassup Bean Man! Beango isn't quite ready yet. Go mingle.";
            conversationDict["ISNOTCOOL"] = "Oh! BM you need some help. Here.";
            conversationDict["ISBAGGED"] = "Long time no See!";
            conversationDict["BEANGOHINT"] = "Let's go Beango!"; // This state should push into Beango
            conversationDict["MYGLASSES"] = "Et...Tu..Granny?";
            conversationDict["ENDCONVO"] = "I personally think that the bag over the head is just too Cliche.";
            return;
        }
        if (gameObjectName == "Chickpea Deputy")
        {
            conversationDict["ISCOOL"] = "J-walked recently? haha, just kidding....";
            conversationDict["ISNOTCOOL"] = "Move Along";
            conversationDict["ISBAGGED"] = "I'm watching you";
            conversationDict["BEANGOHINT"] = "Don't forget the Daily Beango match with Granny!";
            conversationDict["MYGLASSES"] = "Did you Steal my Glasses?";
            conversationDict["ENDCONVO"] = "Stay cool, Deputy";

            return;
        }
        if (gameObjectName == "Lina Bean")
        {
            conversationDict["ISCOOL"] = "Bean Man! How are you so CoOoOoL?!";
            conversationDict["ISNOTCOOL"] = "*Swipes Left*";
            conversationDict["ISBAGGED"] = "Working out WON'T get my attention. *Wink*";
            conversationDict["BEANGOHINT"] = "I wish we could hang out after Beango at Granny's";
            conversationDict["MYGLASSES"] = "You look SoOoOo good in my Glasses.";
            conversationDict["ENDCONVO"] = "I'll see you.    Later.";

            return;
        }

        
    }

    public void RandomizeGlasses()
    {
        var copyNPC = new List<NPC>(_playerController.scriptNPCList);
        NPC winningNPC = copyNPC[Random.Range(0, copyNPC.Count)];
        Debug.Log("Winning NPC " + winningNPC);

        winningNPC.spriteRenderer.sprite = winningNPC.glassesList[0];
        copyNPC.Remove(winningNPC);

        foreach ( NPC character in copyNPC)
        {
            character.spriteRenderer.sprite = character.glassesList[Random.Range(1, character.glassesList.Count)];
        }
    }

    public void TalkedTo(string characterName) {
        HandleConversation(characterName);
        if (beanState == GameState.gameState.ISCOOL)
        {
            if (listTotalNPC.Count > _playerController.scriptNPCList.Count)
            {
                return;
            }
            else {
                beanState = GameState.gameState.BEANGOHINT;
            }
        }
        if (characterName == "Granny Smith") {
            talkedGranny = true;
        }
        if (characterName == "Chickpea Deputy")
        {
            talkedChickpea = true;
        }
        if (characterName == "Lina Bean")
        {
            talkedLina = true;
        }
    }

    public void IsNotCool() {
        beanState = gameState.ISNOTCOOL;
        RandomizeGlasses();

        foreach (NPC character in _playerController.scriptNPCList)
        {
            character.ShowGlasses();
        }
        _playerController.NoGlasses();
        //BEAN MAN SPRITE CHANGE
    }

    public void IsBagged() {
        beanState = gameState.ISBAGGED;
        _playerController.Bagged();
    }

    private void HandleConversation(string characterName)
    {

        if (beanState == gameState.ISNOTCOOL)
        {
            //_npc.ShowGlasses();
            if (talkedGranny) 
            {
                beanState = gameState.ISBAGGED;
                //UIScript.State.text = "IS BAGGED";
                talkedChickpea = false;
                talkedGranny = false;
            }
        }

        if (beanState == gameState.BEANGOHINT) {
            if (characterName == "Granny Smith")
            {
                _actManager.activateGraphicTransition = true;
                talkedChickpea = false;
                talkedLina = false;
                talkedGranny = false;
            }
        }

        if (beanState == gameState.ISBAGGED)
        {
            _playerController.Bagged();
            //UIScript.Chatting.text = conversationDict["ISNOTCOOL"];
            talkedChickpea = false;
            talkedGranny = false;
        }
    }
}
