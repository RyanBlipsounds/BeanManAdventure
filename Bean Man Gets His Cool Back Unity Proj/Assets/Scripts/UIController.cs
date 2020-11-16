using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Febucci.UI;

public class UIController : MonoBehaviour
{
    public TextAnimatorPlayer dialogueBoxAnimator;
    public TextAnimatorPlayer canTalkBoxAnimator;
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

        //gameState.beanState.ToString();

        if (isActive)
        {
            if (!textActivated && thisGameObject == "DialogueBox")
            {
                textActivated = true;
                dialogueBoxAnimator.ShowText(gameState.conversationDict[currentBeanState]);
            }

            transform.position = Vector2.MoveTowards(transform.position, EndPoint.transform.position, Time.deltaTime * speed);
        }
        else
        {
            textActivated = false;
            transform.position = Vector2.MoveTowards(transform.position, StartPoint.transform.position, Time.deltaTime * speed);
        }
    }
}