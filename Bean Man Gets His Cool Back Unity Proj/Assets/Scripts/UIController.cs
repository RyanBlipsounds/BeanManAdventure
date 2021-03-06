﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Febucci.UI;

public class UIController : MonoBehaviour
{
    public TextAnimatorPlayer dialogueBoxAnimator;
    public TextAnimatorPlayer canTalkBoxAnimator;
    public TextAnimatorPlayer narrationBoxText;

    public TextMeshProUGUI responseBoxTextYes;
    public TextMeshProUGUI responseBoxTextNo;
    public GameState gameState;
    public GameObject StartPoint;
    public GameObject EndPoint;
    public float speed = 1.0f;
    public PlayerController _playerController;
    public QuestList questList;

    public EndingsManager endingsManager;

    public GameObject ResponseBox;
    public GameObject DialogueBox;
    public GameObject CanTalkBox;

    public bool isActive = false;
    public bool hasMoved = false;
    public bool textActivated = false;

    public ActManager _actManager;

    public string thisGameObject;

    void Start()
    {
        
        transform.position = StartPoint.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)

    {

    }

    private void OnTriggerExit2D(Collider2D collision)

    {
        
    }

    void Update()
    {
        var currentState = gameState.beanState;
        

        thisGameObject = this.gameObject.name;
        string currentBeanState = gameState.beanState.ToString();
        
        if (isActive)
        {
            if (thisGameObject == "DialogueBox")
            {
                gameState.StartMusic.setParameterByName("CharacterProx", 1);
                _actManager.IsNotCoolMusicEvent.setParameterByName("CharacterProx", 1);
            }

            if (thisGameObject == "NarrationBox")
            {
                speed = 4.0f;
                narrationBoxText.ShowText(_actManager.EndingScreenText);
            }
            Debug.Log(gameState.beanState);
            speed = 3.0f;
            if (_actManager.sceneTransitionState == ActManager.sceneState.graphic && thisGameObject != "NarrationBox") {
                isActive = false;
            }
            if (!textActivated && thisGameObject == "DialogueBox")
            {
                if (_playerController.thisCharacter.gameObject.name == "Lemonade Stand" && endingsManager.endingsSeenList.Count > 6)
                {
                    questList.CompleteQuestItem("Get Lemonade Stand");
                }
                //dialogueBoxAnimator.onCharacterVisible
                bool hasNPC = false;
                hasNPC = false;
                textActivated = true;
                foreach (GameObject ending in endingsManager.endingsSeenList) {
                    if (!ending.gameObject.name.Contains("Beanman")) {
                        hasNPC = true;
                        break;
                    }
                }

                if (gameState.beanState == GameState.gameState.BEANGOHINT && endingsManager.endingsSeenList.Count > 0 && _playerController.thisCharacter.gameObject.name != "Granny Smith") {
                    dialogueBoxAnimator.ShowText(gameState.conversationDict["ISCOOL"]);
                }
                else
                {
                    dialogueBoxAnimator.ShowText(gameState.conversationDict[currentBeanState]);
                }
            }

            if (!textActivated && thisGameObject == "ResponseBox")
            { 
                if (_playerController.thisCharacter.gameObject.name == "ExitTown")
                {
                    responseBoxTextYes.text = "Get me out of here";
                    responseBoxTextNo.text = "I guess I'll stay";
                }
                else if (_playerController.thisCharacter.gameObject.name == "Granny Smith" && gameState.beanState == GameState.gameState.BEANGOHINT)
                {
                    responseBoxTextYes.text = "Let's Beango!";
                    responseBoxTextNo.text = "Not yet";
                }
                else {
                        responseBoxTextYes.text = "Are those my Glasses?";
                        responseBoxTextNo.text = "Talk To You Later";
                }
            }

            this.gameObject.transform.position = Vector3.MoveTowards(transform.position, EndPoint.transform.position, Time.deltaTime * speed);
        }
        else
        {
            if (thisGameObject == "DialogueBox")
            {
                gameState.StartMusic.setParameterByName("CharacterProx", 0);
                _actManager.IsNotCoolMusicEvent.setParameterByName("CharacterProx", 0);
            }
            if (thisGameObject == "NarrationBox")
            {
                narrationBoxText.StopShowingText();
            }
            if (thisGameObject == "NarrationBox") {
                speed = 9.0f;
            }
            textActivated = false;
            this.gameObject.transform.position = Vector3.MoveTowards(transform.position, StartPoint.transform.position, Time.deltaTime * speed);
        }
    }


    public void PlayVoiceSound()
    {

        if (_playerController.thisCharacter.name == "Chickpea Deputy")
        {
            if (gameState.beanState == GameState.gameState.ISNOTCOOL || gameState.beanState == GameState.gameState.ISBAGGED)
            {

                FMODUnity.RuntimeManager.PlayOneShot("event:/Chonkpea Deputy");
                return;

            }
        }

        if (_playerController.thisCharacter.name == "Dead Guy" || _playerController.thisCharacter.name == "Paid Actor")
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Fire Hydrant");
            return;
        }

        if (_playerController.thisCharacter.name == "Raccoon")
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Butter");
            return;
        }
        if (_playerController.thisCharacter.name == "Lina Edamame")
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Lina Bean");
            return;
        }

        if (_playerController.thisCharacter.name == "Ninja Officer Bush")
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Baked Bush");
            return;
        }

        FMODUnity.RuntimeManager.PlayOneShot("event:/" + _playerController.thisCharacter.name);

       



    }
}