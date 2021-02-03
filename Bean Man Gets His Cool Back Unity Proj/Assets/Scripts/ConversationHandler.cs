using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationHandler : MonoBehaviour
{

    public PlayerController _playerController;
    public GameState _gamestate;
    public QuestList questList;
    public ActManager actManager;


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "StickHeart")
        {
            Debug.Log("Stick Heart");
            questList.CompleteQuestItem("Find the Hidden Stick");
        }
        if (collision.gameObject.name == "ExitTown")
        {
            actManager.LoadEnding("Beanman Leaves Town");
        }
        if (collision.gameObject.tag == "NPC" || collision.gameObject.tag == "SideNPC" || collision.gameObject.name == "Fire Hydrant")
        {
            Debug.Log(collision.gameObject.name);
            //This initializes 
            if (!_playerController.Characters.Contains(collision.gameObject))
            {
                _playerController.thisCharacter = GameObject.Find(collision.gameObject.name);
            }

            _playerController._canTalkBox.canTalkBoxAnimator.ShowText(collision.gameObject.name);

            _playerController.characterInRange = true;

            if (_gamestate.beanState == GameState.gameState.ISBAGGED)
            {
                _gamestate.wrongNPCGameObject = collision.gameObject;
            }

            _gamestate.Conversation(collision.gameObject.name, 0);
            Debug.Log("Pizza boy " + _gamestate.conversationDict["WRONGBAGGED"]);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "NPC" || other.gameObject.tag == "SideNPC" || other.gameObject.name == "Fire Hydrant" || other.gameObject.name == "ExitTown")
        {
            _playerController.characterInRange = false;
        }
    }
}
