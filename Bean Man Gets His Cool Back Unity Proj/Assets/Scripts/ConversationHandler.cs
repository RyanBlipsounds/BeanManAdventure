﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationHandler : MonoBehaviour
{

    public PlayerController _playerController;
    public GameState _gamestate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "NPC" || collision.gameObject.name == "Fire Hydrant")
        {
            //This initializes 
            if (!_playerController.Characters.Contains(collision.gameObject))
            {
                _playerController.thisCharacter = GameObject.Find(collision.gameObject.name);
            }

            _playerController._canTalkBox.canTalkBoxAnimator.ShowText(collision.gameObject.name);

            _playerController.characterInRange = true;

            //Sets the Dictionary in GameState to the proper character dict and state
            _gamestate.Conversation(collision.gameObject.name, 0);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "NPC" || other.gameObject.name == "Fire Hydrant")
        {
            _playerController.characterInRange = false;
        }
    }
}
