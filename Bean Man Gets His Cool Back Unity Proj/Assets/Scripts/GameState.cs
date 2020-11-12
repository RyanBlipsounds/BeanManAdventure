using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    // The game's UI
    [SerializeField]
    private UILogic UIScript = default;

    public GameState m_gameState;
    public enum gameState
    {
        IsCool,
        IsNotCool,
        IsBagged
    }

    public gameState beanState = gameState.IsCool;
    private string characterConversation = default;
    private Dictionary<string, string> conversationDict = new Dictionary<string, string>();
    private bool talkedChickpea = false;
    private bool talkedGranny = false;

    // Start is called before the first frame update
    void Start()
    {
        UIScript.ChickPea.onClick.AddListener(chickpeaClicked); // this will be talking to character
        UIScript.Lina.onClick.AddListener(linaClicked);
        UIScript.Granny.onClick.AddListener(grannyClicked);

        conversationDict.Add("ISCOOL", "ISCOOL");
        conversationDict.Add("ISNOTCOOL", "ISNOTCOOL");
        conversationDict.Add("ISBAGGED", "ISBAGGED");
        conversationDict.Add("BEANGOHINT", "BEANGOHINT");
        conversationDict.Add("MYGLASSES", "MYGLASSES");
        conversationDict.Add("ENDCONVO", "ENDCONVO");

        UIScript.State.text = "IS COOL!";
    }

    private void chickpeaClicked()
    {
        conversationDict["ISCOOL"] = "J-walked recently? haha, just kidding....";
        conversationDict["ISNOTCOOL"] = "Move Along";
        conversationDict["ISBAGGED"] = "I'm watching you";
        conversationDict["BEANGOHINT"] = "Don't forget the Daily Beango match with Granny!";
        conversationDict["MYGLASSES"] = "Did you Steal my Glasses?";
        conversationDict["ENDCONVO"] = "Stay cool, Deputy";

        HandleConversation();
        talkedChickpea = true;

    }

    private void grannyClicked()
    {
        conversationDict["ISCOOL"] = "Wassup Bean Man! Beango isn't quite ready yet. Go mingle.";
        conversationDict["ISNOTCOOL"] = "Oh! BM you need some help. Here.";
        conversationDict["ISBAGGED"] = "Long time no See!";
        conversationDict["BEANGOHINT"] = "Let's go Beango!"; // This state should push into Beango
        conversationDict["MYGLASSES"] = "Et...Tu..Granny?";
        conversationDict["ENDCONVO"] = "I personally think that the bag over the head is just too Cliche.";

        HandleConversation();
        talkedGranny = true;

    }

    private void linaClicked()
    {
        conversationDict["ISCOOL"] = "Bean Man! How are you so CoOoOoL?!";
        conversationDict["ISNOTCOOL"] = "*Swipes Left*";
        conversationDict["ISBAGGED"] = "Working out WON'T get my attention. *Wink*";
        conversationDict["BEANGOHINT"] = "I wish we could hang out after Beango at Granny's";
        conversationDict["MYGLASSES"] = "You look SoOoOo good in my Glasses.";
        conversationDict["ENDCONVO"] = "I'll see you.    Later.";

        HandleConversation();
    }



    private void HandleConversation()
    {
        if (beanState == gameState.IsCool)
        {
           
            UIScript.Chatting.text = conversationDict["ISCOOL"];

            if (talkedChickpea)
            {
                UIScript.Chatting.text = conversationDict["BEANGOHINT"];
                if (talkedGranny)
                {
                    beanState = gameState.IsNotCool;
                    UIScript.State.text = "IS NOT COOL (NO GLASSES)";
                    talkedChickpea = false;
                    talkedGranny = false;
                }
            }
        }

        if (beanState == gameState.IsNotCool)
        {
            UIScript.Chatting.text = conversationDict["ISNOTCOOL"];

            if (talkedGranny)
            {
                beanState = gameState.IsBagged;
                UIScript.State.text = "IS BAGGED";
                talkedChickpea = false;
                talkedGranny = false;
            }
        }

        if (beanState == gameState.IsBagged)
        {
            UIScript.Chatting.text = conversationDict["ISNOTCOOL"];
            talkedChickpea = false;
            talkedGranny = false;
        }
    }
}
