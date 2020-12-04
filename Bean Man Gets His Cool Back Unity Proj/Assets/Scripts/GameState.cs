using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    // The game's UI
    [SerializeField]
    private UILogic _UILogic = default;

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
        MYGLASSES,
        ENDING
    }

    public gameState beanState = gameState.ISCOOL;
    private string characterConversation = default;
    public Dictionary<string, string> conversationDict = new Dictionary<string, string>();
    private bool talkedChickpea = false;
    private bool talkedGranny = false;
    private bool talkedLina = false;

    
    private float coolMeterHigh = 1.0f;
    private float coolMeterMed = .72f;
    private float coolMeterLow = .365f;

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

        _UILogic.setCoolness(coolMeterHigh);
    }

    public void Conversation(string gameObjectName, int count) {
        if (gameObjectName == "Fire Hydrant")
        {
            if (count != 6)
            {
                string dialogue = "..";
                dialogue = "..";
                int countStart = 0;
                countStart = 0;
                while (countStart < count)
                {
                    dialogue += ".";
                    countStart++;
                }
                conversationDict["ISCOOL"] = dialogue;
                return;
            }
            else {
                conversationDict["ISCOOL"] = "Why are you talking to me? I'm a fire hydrant.";
                conversationDict["BEANGOHINT"] = "Why are you talking to me? I'm a fire hydrant.";
            }
        }
        Debug.Log("1");
        if (gameObjectName == "Granny Smith") {
            conversationDict["ISCOOL"] = "My main man Bean Man! Beango isn't ready yet, go mingle with other peeps";
            conversationDict["BEANGOHINT"] = "Let's go Beango!"; // This state should push into Beango
            conversationDict["ISNOTCOOL"] = "Bean Man! Looks like you lost your glasses some how! Here, take this";
            conversationDict["ISBAGGED"] = "Long time no See!";
            conversationDict["MYGLASSES"] = "Et...Tu..Granny?";
            conversationDict["ENDCONVO"] = "I personally think that the bag over the head is just too Cliche.";
            return;
        }
        Debug.Log("2");
        if (gameObjectName == "Chickpea Deputy")
        {
            conversationDict["ISCOOL"] = "J-walked recently? haha, just kidding...";
            conversationDict["ISNOTCOOL"] = "Watch yourself.";
            conversationDict["ISBAGGED"] = "What do you want?";
            conversationDict["BEANGOHINT"] = "Oh boy Beango? Gosh, I wish I was cool enough to join in!";

            return;
        }
        Debug.Log("3");
        if (gameObjectName == "Lina Bean")
        {
            conversationDict["ISCOOL"] = "Bean Man! Are we still on for the movies this weekend?!";
            conversationDict["ISNOTCOOL"] = "*Swipes Left*";
            conversationDict["ISBAGGED"] = "Working out WON'T get my attention. *Wink*";
            conversationDict["BEANGOHINT"] = "I wish we could hang out after Beango at Granny's";

            return;
        }
        Debug.Log("4");
        if (gameObjectName == "Birthday Cake")
        {
            //NEEDS TO BE LESS OBVIOUS THAT HE IS STARTING A CULT
            conversationDict["ISCOOL"] = "H-hey Beanman, would you be interested in joining my cul- I mean club?";
            conversationDict["ISNOTCOOL"] = "Oh, yeah we don't really need you to join us.";
            conversationDict["ISBAGGED"] = "Hello sir! Are you interested in learning about a fun new religion?";
            conversationDict["BEANGOHINT"] = "Maybe if I go to beango I can get new members...";

            return;
        }
        Debug.Log("5");
        if (gameObjectName == "Peanut Twins")
        {
            conversationDict["ISCOOL"] = "Bean Man! We want to be just like you when we're older!";
            conversationDict["ISNOTCOOL"] = "Oh hey Bean Boy, I'm pretty busy. Maybe go talk with that stick?";
            conversationDict["ISBAGGED"] = "What is it son, I'm very busy.";
            conversationDict["BEANGOHINT"] = "When we're older, we want to go to Beango with you!";

            return;
        }
        Debug.Log("6");
        if (gameObjectName == "GreenBen")
        {
            conversationDict["ISCOOL"] = "Is that you Bean Man?. I really hope I can feel better soon.";
            conversationDict["ISNOTCOOL"] = "Ouch, what happened to you?";
            conversationDict["ISBAGGED"] = "Oh you look much better now!";
            conversationDict["BEANGOHINT"] = "Beango sounds great but I think I need to get some rest.";

            return;
        }
        Debug.Log("7");
        if (gameObjectName == "Slim Sausage")
        {
            conversationDict["ISCOOL"] = "Yo it's the mean Bean! Not even slim sausage is as slick as you!";
            conversationDict["ISNOTCOOL"] = "Did I say mean Bean before? What I meant to say was gross.";
            conversationDict["ISBAGGED"] = "I'm feeling REAL good lately.";
            conversationDict["BEANGOHINT"] = "I'm rooting for you at Beango tonight my main man Bean Man!";

            return;
        }
    }

    public void RandomizeGlasses()
    {
        var copyNPC = new List<NPC>(_playerController.scriptNPCList);
        NPC winningNPC = copyNPC[Random.Range(0, copyNPC.Count)];
        Debug.Log("Winning NPC " + winningNPC);

        winningNPC.spriteRenderer.sprite = winningNPC.glassesList[0];
        winningNPC.isWinner = true;
        copyNPC.Remove(winningNPC);

        foreach ( NPC character in copyNPC)
        {
            character.spriteRenderer.sprite = character.glassesList[Random.Range(1, character.glassesList.Count)];
        }
    }

    public void TalkedTo(string characterName) {
        if (beanState == GameState.gameState.ISCOOL)
        {
            Debug.Log(listTotalNPC.Count + " " + _playerController.scriptNPCList.Count);
            if (listTotalNPC.Count > _playerController.scriptNPCList.Count)
            {
                return;
            }
            else {
                beanState = GameState.gameState.BEANGOHINT;
            }
        }
        HandleConversation(characterName);
    }

    public void IsNotCool() {
        beanState = gameState.ISNOTCOOL;
        _UILogic.setCoolness(coolMeterLow);
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
        _UILogic.setCoolness(coolMeterMed);
    }

    public void Ending() {
        beanState = gameState.ENDING;
    }

    private void HandleConversation(string characterName)
    {
        if (beanState == gameState.ISCOOL)
        {
            if (characterName == "Granny Smith")
            {
                talkedGranny = false;
            }
        }

        if (beanState == gameState.ISNOTCOOL)
        {
            if (characterName == "Granny Smith")
            {
                talkedGranny = true;
            }
            if (talkedGranny) 
            {
                beanState = gameState.ISBAGGED;
                _UILogic.setCoolness(coolMeterMed);
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
            //_playerController.Bagged();
            //UIScript.Chatting.text = conversationDict["ISNOTCOOL"];
            talkedChickpea = false;
            talkedGranny = false;
        }
    }
}

