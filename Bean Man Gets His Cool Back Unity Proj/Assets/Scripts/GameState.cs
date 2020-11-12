using UnityEngine;

public class GameState : MonoBehaviour
{
    // The game's UI
    [SerializeField]
    private UILogic UIScript = default;
    public enum gameState
    {
        IsCool,
        IsNotCool,
        IsBagged
    }

    public gameState beanState = gameState.IsCool;
    private string characterConversation = default;

    // Start is called before the first frame update
    void Start()
    {
        UIScript.ChickPea.onClick.AddListener(chickpeaClicked); // this will be talking to character
        UIScript.Lina.onClick.AddListener(linaClicked);
        UIScript.Granny.onClick.AddListener(grannyClicked);
    }

    private void chickpeaClicked()
    {
        // Need to set up which character
        
        //HandleConversation(characterConversation);
    }
    private void linaClicked()
    {
        characterConversation = "lina";
        //HandleConversation(characterConversation);
    }
    private void grannyClicked()
    {
        characterConversation = "granny";
        //HandleConversation(characterConversation);
    }


    private void HandleConversation(string character)
    {
        if (beanState == gameState.IsCool)
        {
           
            UIScript.Chatting.text = ChickpeaDeputy.ISCOOL;
        }
    }
}
