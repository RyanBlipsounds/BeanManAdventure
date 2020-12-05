﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Space]
    [Header("Character Attributes:")]
    public float MOVEMENT_BASE_SPEED = 1.0f;

    [Space]
    [Header("Character Statistics:")]
    public Vector2 movementDirection;
    public float movementSpeed;
    public Rigidbody2D rb;
    public Animator GlassesAnimator;
    public Animator BaggedAnimator;
    public Animator NoGlassesanimator;

    public SpriteRenderer GlassesSpriteRenderer;
    public SpriteRenderer NoGlassesSpriteRenderer;
    public SpriteRenderer BaggedSpriteRenderer;

    public GameState gameState;

    public bool characterInRange;

    public GameObject GlassesBeanMan;
    public GameObject NoGlassesBeanMan;
    public GameObject BaggedBeanMan;

    //public GameObject UI
    public UIController _canTalkBox;
    public UIController _dialogueBox;
    public UIController _responseBox;

    public GameObject Bag;
    public GameObject m_BagStartPosition;
    public GameObject m_BagEndPosition;
    
    public List<GameObject> Characters = new List<GameObject>();
    public List<NPC> scriptNPCList = new List<NPC>();

    public ActManager _actManager;
    public bool bagMoving = false;
    public bool finishedBagMove = false;

    public GameObject thisCharacter = null;
    private int index = 0;
    private NPC thisNPCList = null;

    public int fireHydrantTalkCount = 0;

    private bool _hasPlayed = false;

    public Transform startTransform = default;

    void Start()
    {
        Bag.transform.position = m_BagStartPosition.transform.position;

        GlassesBeanMan.SetActive(true);
        NoGlassesBeanMan.SetActive(false);
        BaggedBeanMan.SetActive(false);

        _canTalkBox = GameObject.Find("CanTalkBox").GetComponent<UIController>();
        _dialogueBox = GameObject.Find("DialogueBox").GetComponent<UIController>();
        _responseBox = GameObject.Find("ResponseBox").GetComponent<UIController>();

    }

    // Update is called once per frame
    void Update()
    {
        _actManager.LoadNewAct();

        //Movement and Animation
        if (_actManager.activateGraphicTransition == false)
        {
            if (bagMoving == false)
            {
                Dialogue();
                ProcessInputs();
            }
            else {
                movementSpeed = 0;
                movementDirection = new Vector2(0.0f, 0.0f);
                movementDirection.Normalize();
            }
        }
        else {
            movementSpeed = 0;
            movementDirection = new Vector2(0.0f, 0.0f);
            movementDirection.Normalize();
        }
        Move();
        Animate();
        
        if (Bag.transform.position.y < m_BagEndPosition.transform.position.y + 0.05 && bagMoving == true && finishedBagMove == false)
        {
            Bagged();
            Bag.SetActive(false);
            bagMoving = false;
            finishedBagMove = true;
        } else if (gameState.beanState == GameState.gameState.ISBAGGED && Bag.transform.position != m_BagEndPosition.transform.position && finishedBagMove == false) {
            bagMoving = true;
            Bag.transform.position = Vector2.MoveTowards(Bag.transform.position, m_BagEndPosition.transform.position, Time.deltaTime * 1);
        }

    }

    public void Glasses() {
        GlassesBeanMan.SetActive(true);
        NoGlassesBeanMan.SetActive(false);
        BaggedBeanMan.SetActive(false);
        Bag.SetActive(true);
    }

    public void NoGlasses() {
        GlassesBeanMan.SetActive(false);
        NoGlassesBeanMan.SetActive(true);
    }

    public void Bagged()
    {
        NoGlassesBeanMan.SetActive(false);
        BaggedBeanMan.SetActive(true);
    }

    void LoadBag() {

    }

    void Dialogue() {
        if (characterInRange)
        {
            _canTalkBox.isActive = true;
            if (Input.GetKeyDown(KeyCode.Space) && _dialogueBox.isActive == false)
            {
                if (!scriptNPCList.Contains(thisCharacter.GetComponent<NPC>()))
                {
                    if (thisCharacter.gameObject.name == "Fire Hydrant" && fireHydrantTalkCount < 5 && gameState.beanState == GameState.gameState.ISCOOL)
                    {
                        fireHydrantTalkCount++;
                        _dialogueBox.isActive = true;
                        gameState.Conversation(thisCharacter.gameObject.name, fireHydrantTalkCount);
                        return;
                    }
                    else if (thisCharacter.gameObject.name == "Fire Hydrant" && gameState.beanState == GameState.gameState.ISCOOL){
                        thisCharacter.gameObject.tag = "NPC";
                        if (!gameState.listTotalNPC.Contains(thisCharacter)) {
                            gameState.listTotalNPC.Add(thisCharacter);
                        }
                        
                        gameState.Conversation(thisCharacter.gameObject.name, 6);
                    }
                    if (thisCharacter.GetComponent<NPC>()) {
                        scriptNPCList.Add(thisCharacter.GetComponent<NPC>());
                    }
                }

                if (thisCharacter.gameObject.name == "Fire Hydrant" && thisCharacter.gameObject.tag == "NPC")
                {
                    if (gameState.beanState == GameState.gameState.ISNOTCOOL || gameState.beanState == GameState.gameState.ISBAGGED)
                    {
                        Debug.Log("Start Vomiting");
                        return;
                    }
                }

                if (gameState.beanState == GameState.gameState.ISBAGGED || thisCharacter.gameObject.name == "ExitTown"){
                    _responseBox.isActive = true;
                }

                if (!_dialogueBox.isActive)
                {
                    if (thisCharacter.gameObject.name != "ExitTown") {
                        index = scriptNPCList.IndexOf(thisCharacter.GetComponent<NPC>());
                        thisNPCList = scriptNPCList[index];
                        thisNPCList.hasSpoken = true;
                    }
                }
                gameState.TalkedTo(thisCharacter.name);
                if (gameState.beanState == GameState.gameState.BEANGOHINT && thisCharacter.name == "Granny Smith")
                {
                    _actManager.LoadNewAct();
                }
                _dialogueBox.isActive = true;
            }
            else if (Input.GetKeyDown(KeyCode.Space) && _dialogueBox.isActive == true)
            {
                if (gameState.listTotalNPC.Count == scriptNPCList.Count)
                {
                    gameState.IsBeanGoHint();
                }
                _dialogueBox.isActive = false;
                _responseBox.isActive = false;
            }
        }
        else
        {
            if (gameState.listTotalNPC.Count == scriptNPCList.Count) {
                gameState.IsBeanGoHint();
            }
        _dialogueBox.isActive = false;
        _responseBox.isActive = false;
        _canTalkBox.isActive = false;
        }

        if (Input.GetKeyDown(KeyCode.Y) && _responseBox.isActive == true)
        {
            if (thisCharacter.gameObject.name != "ExitTown")
            {
                if (thisNPCList.isWinner)
                {
                    _actManager.LoadEnding("Beanman");
                }
                else
                {
                    foreach (NPC character in scriptNPCList)
                    {
                        if (character.isWinner == true)
                        {
                            _actManager.LoadEnding(character.gameObject.name);
                            break;
                        }
                    }
                }
            }
            else
            {
                if (gameState.beanState == GameState.gameState.BEANGOHINT || gameState.beanState == GameState.gameState.ISCOOL)
                {
                    _actManager.LoadEnding("Beanman Leaves Cool Town");
                }
                if (gameState.beanState == GameState.gameState.ISNOTCOOL)
                {
                    _actManager.LoadEnding("Beanman Leaves Uncool Town");
                }
                if (gameState.beanState == GameState.gameState.ISBAGGED)
                {
                    _actManager.LoadEnding("Beanman Leaves Bag Town");
                }
                gameState.beanState = GameState.gameState.ENDING;
            }
        }
        if (Input.GetKeyDown(KeyCode.N) && _responseBox.isActive == true)
        {
            _dialogueBox.isActive = false;
            _responseBox.isActive = false;
        }

        if (_canTalkBox.isActive == false && _hasPlayed == false)
        {
            _hasPlayed = true;
            // Bruh this sound is horrendous
            //FMODUnity.RuntimeManager.PlayOneShot("event:/TalkBox");
        } else if(_canTalkBox.isActive == true && _hasPlayed == true)
        {
            _hasPlayed = false;
            //FMODUnity.RuntimeManager.PlayOneShot("event:/TalkBox");
        }
    }

    void ProcessInputs() {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movementSpeed = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);
        movementDirection.Normalize();
    }

    public void MoveToStart()
    {
        transform.position = startTransform.transform.position;
    }

    void Move() {
        rb.velocity = movementDirection * movementSpeed * MOVEMENT_BASE_SPEED;
    }

    void Animate() {
        if (movementDirection.x < 0) {
            GlassesSpriteRenderer.flipX = false;
            BaggedSpriteRenderer.flipX = false;
            NoGlassesSpriteRenderer.flipX = false;
        } else if(movementDirection.x > 0) {
            GlassesSpriteRenderer.flipX = true;
            BaggedSpriteRenderer.flipX = true;
            NoGlassesSpriteRenderer.flipX = true;
        }

        NoGlassesanimator.SetFloat("Speed", movementSpeed);
        NoGlassesanimator.SetFloat("YAxisDirection", movementDirection.y);

        GlassesAnimator.SetFloat("Speed", movementSpeed);
        GlassesAnimator.SetFloat("YAxisDirection", movementDirection.y);

        BaggedAnimator.SetFloat("Speed", movementSpeed);
        BaggedAnimator.SetFloat("YAxisDirection", movementDirection.y);
    }
}
