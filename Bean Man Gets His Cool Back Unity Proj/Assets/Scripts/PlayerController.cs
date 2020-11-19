﻿using System.Collections;
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

    public List<GameObject> Characters = new List<GameObject>();
    public List<NPC> scriptNPCList = new List<NPC>();

    public ActManager _actManager;

    private GameObject thisCharacter = null;
    private int index = 0;
    private NPC thisNPCList = null;

    void Start()
    {
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
            ProcessInputs();
        }
        else {
            movementSpeed = 0;
            movementDirection = new Vector2(0.0f, 0.0f);
            movementDirection.Normalize();
        }
        Move();
        Animate();
        Dialogue();
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

    void Dialogue() {
        if (characterInRange)
        {
            _canTalkBox.isActive = true;
            if (Input.GetKeyDown(KeyCode.Space) && _dialogueBox.isActive == false)
            {
                if (gameState.beanState == GameState.gameState.ISBAGGED){
                    _responseBox.isActive = true;
                }

                if (!_dialogueBox.isActive)
                {
                    index = scriptNPCList.IndexOf(thisCharacter.GetComponent<NPC>());
                    thisNPCList = scriptNPCList[index];
                    thisNPCList.hasSpoken = true;
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
                _dialogueBox.isActive = false;
            }
            if (Input.GetKeyDown(KeyCode.Y) && _responseBox.isActive == true) {
                Debug.Log("Yes");
            }
            if (Input.GetKeyDown(KeyCode.N) && _responseBox.isActive == true)
            {
                Debug.Log("No dawg");
            }
        }
        else
        {
            _dialogueBox.isActive = false;
            _responseBox.isActive = false;
            _canTalkBox.isActive = false;
        }
    }

    void ProcessInputs() {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movementSpeed = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);
        movementDirection.Normalize();
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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "NPC") {

            //This initializes 
            if (!Characters.Contains(collision.gameObject))
            {
                thisCharacter = GameObject.Find(collision.gameObject.name);

                scriptNPCList.Add(thisCharacter.GetComponent<NPC>());
                Characters.Add(thisCharacter);
            }

            _canTalkBox.canTalkBoxAnimator.ShowText(collision.gameObject.name);

            if (Characters.Count >= index) {
                index = Characters.IndexOf(collision.gameObject);
                thisCharacter = Characters[index];
            }

            characterInRange = true;

            //Sets the Dictionary in GameState to the proper character dict and state
            gameState.Conversation(collision.gameObject.name);


        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "NPC")
        {
            characterInRange = false;
        }
    }
}
