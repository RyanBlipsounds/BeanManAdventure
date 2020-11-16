using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    // The game's UI
    [SerializeField]
    private UILogic UIScript = default;

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

    // Start is called before the first frame update
    void Start()
    {
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

            HandleConversation();
            talkedGranny = true;
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

            HandleConversation();
            talkedChickpea = true;
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

            HandleConversation();
            return;
        }

        
    }
    

    private void HandleConversation()
    {
        if (beanState == gameState.ISCOOL)
        {
           
            //UIScript.Chatting.text = conversationDict["ISCOOL"];

            if (talkedChickpea)
            {
                //UIScript.Chatting.text = conversationDict["BEANGOHINT"];
                if (talkedGranny)
                {
                    beanState = gameState.ISNOTCOOL;
                    //UIScript.State.text = "IS NOT COOL (NO GLASSES)";
                    talkedChickpea = false;
                    talkedGranny = false;
                }
            }
        }

        if (beanState == gameState.ISNOTCOOL)
        {
            //UIScript.Chatting.text = conversationDict["ISNOTCOOL"];

            if (talkedGranny)
            {
                beanState = gameState.ISBAGGED;
                //UIScript.State.text = "IS BAGGED";
                talkedChickpea = false;
                talkedGranny = false;
            }
        }

        if (beanState == gameState.ISBAGGED)
        {
            //UIScript.Chatting.text = conversationDict["ISNOTCOOL"];
            talkedChickpea = false;
            talkedGranny = false;
        }
    }
}
