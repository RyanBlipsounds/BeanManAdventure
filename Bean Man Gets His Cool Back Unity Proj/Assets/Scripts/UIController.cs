using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Febucci.UI;

public class UIController : MonoBehaviour
{
    public TextAnimatorPlayer dialogueBoxAnimator;
    public TextAnimatorPlayer canTalkBoxAnimator;
    public TextAnimatorPlayer narrationBoxText;
    public GameState gameState;

    public GameObject StartPoint;
    public GameObject EndPoint;
    public float speed = 1.0f;

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

    void Update()
    {
        var currentState = gameState.beanState;
        

        thisGameObject = this.gameObject.name;
        string currentBeanState = gameState.beanState.ToString();

        if (isActive)
        {
            if (thisGameObject == "NarrationBox")
            {
                narrationBoxText.ShowText(_actManager.EndingScreenText);
            }
            Debug.Log(gameState.beanState);
            speed = 3.0f;
            if (_actManager.sceneTransitionState == ActManager.sceneState.graphic && thisGameObject != "NarrationBox") {
                isActive = false;
            }
            if (!textActivated && thisGameObject == "DialogueBox")
            {
                textActivated = true;
                dialogueBoxAnimator.ShowText(gameState.conversationDict[currentBeanState]);
            }

            this.gameObject.transform.position = Vector3.MoveTowards(transform.position, EndPoint.transform.position, Time.deltaTime * speed);
        }
        else
        {
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
}