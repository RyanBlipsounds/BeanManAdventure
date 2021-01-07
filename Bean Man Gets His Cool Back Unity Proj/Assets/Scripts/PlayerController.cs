using System;
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
    public UIController _narrationBox;

    public GameObject Bag;
    public GameObject m_BagStartPosition;
    public GameObject m_BagEndPosition;
    
    public List<GameObject> Characters = new List<GameObject>();
    public List<NPC> scriptNPCList = new List<NPC>();

    public ActManager _actManager;
    public bool bagMoving = false;
    public bool finishedBagMove = false;

    public EndingsManager endingsManager;

    public GameObject thisCharacter = null;
    private int index = 0;
    private NPC thisNPCList = null;

    public int fireHydrantTalkCount = 0;

    private bool _hasPlayed = false;

    public Transform startTransform = default;

    public FireHydrantVomit fireHydrantVomit;
    public bool isVommiting = false;

    public bool skippedTypewriter = false;

    public bool spriteFlip = false;

    public QuestList questList;
    public UIController journalController;
    public QuestListLayout questListLayout;

    public QuestNotification questNotification;

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
        Journal();
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

    void Journal()
    {
        if (_narrationBox.isActive == true)
        {
            journalController.isActive = false;
            return;
        }
        if (Input.GetKeyDown(KeyCode.Q) && journalController.isActive == false)
        {
            questNotification.isActive = false;

            questListLayout.UpdateQuestList();
            questListLayout.UpdateCompletedQuestList();
            journalController.isActive = true;
        }
        else if (Input.GetKeyDown(KeyCode.Q) && journalController.isActive == true)
        {
            journalController.isActive = false;
        }
    }

    void Dialogue() {
        if (characterInRange)
        {
            if (journalController.isActive == true)
            {
                _dialogueBox.isActive = false;
                _responseBox.isActive = false;
                _canTalkBox.isActive = false;
                return;
            }
            _canTalkBox.isActive = true;
            if (Input.GetKeyDown(KeyCode.Space) && _dialogueBox.isActive == false)
            {
                if (thisCharacter.gameObject.name == "Stick") {
                    return;
                }
                // Specifically adds characters to the NPC list
                if (!scriptNPCList.Contains(thisCharacter.GetComponent<NPC>()))
                {
                    // Handle Fire Hydrant NPC Logic
                    if (thisCharacter.gameObject.name == "Fire Hydrant" && fireHydrantTalkCount < 4)
                    {
                        if (gameState.beanState == GameState.gameState.ISCOOL || gameState.beanState == GameState.gameState.BEANGOHINT) {
                            fireHydrantTalkCount++;
                            _dialogueBox.isActive = true;
                            gameState.Conversation(thisCharacter.gameObject.name, fireHydrantTalkCount);
                            return;
                        }
                    }
                    // Adds NPC to list
                    if (thisCharacter.GetComponent<NPC>() && thisCharacter.gameObject.tag == "NPC") {
                        scriptNPCList.Add(thisCharacter.GetComponent<NPC>());
                    }
                }
                if (thisCharacter.gameObject.name == "Fire Hydrant")
                {

                    if (gameState.beanState == GameState.gameState.ISCOOL || gameState.beanState == GameState.gameState.BEANGOHINT)
                    {
                        thisCharacter.gameObject.tag = "NPC";
                        if (thisCharacter.GetComponent<NPC>() && thisCharacter.gameObject.tag == "NPC")
                        {
                            scriptNPCList.Add(thisCharacter.GetComponent<NPC>());
                        }
                        if (!gameState.listTotalNPC.Contains(thisCharacter))
                        {
                            questList.CompleteQuestItem("Fire Hydrant to Talk");
                            fireHydrantVomit.fireHydrantActivated = true;
                            gameState.listTotalNPC.Add(thisCharacter);
                        }
                        //Triggers conversation in both ISCOOl and BAGGED
                        gameState.Conversation(thisCharacter.gameObject.name, 6);
                        _dialogueBox.isActive = true;

                        return;
                    }
                }
                if (fireHydrantVomit.fireHydrantActivated == false && thisCharacter.gameObject.name == "Fire Hydrant")
                {
                    if (gameState.beanState == GameState.gameState.ISBAGGED || gameState.beanState == GameState.gameState.ISNOTCOOL)
                    {
                        if (thisCharacter.gameObject.tag == "NPC")
                        {
                            if (gameState.beanState == GameState.gameState.ISNOTCOOL)
                            {
                                isVommiting = true;
                                return;
                            }
                        }
                        return;
                    }
                }
                


                if (thisCharacter.gameObject.name == "Traffic Cone") {
                    questList.CompleteQuestItem("Traffic Cone");
                }

                // Handle if Beanman is trying to leave town while bagged
                if (gameState.beanState == GameState.gameState.ISBAGGED && thisCharacter.gameObject.tag != "SideNPC")
                {
                    _responseBox.isActive = true;
                }
                if (thisCharacter.gameObject.name == "ExitTown")
                {
                    Debug.Log("Exit Town!");
                    _responseBox.isActive = true;
                }
                
                // Checks if the NPC has been spoken to
                if (!_dialogueBox.isActive)
                {

                    if (thisCharacter.gameObject.tag != "SideNPC") {
                        index = scriptNPCList.IndexOf(thisCharacter.GetComponent<NPC>());
                        thisNPCList = scriptNPCList[index];
                        thisNPCList.hasSpoken = true;
                    }
                }

                if (thisCharacter.gameObject.name == "Granny Smith")
                {
                    if (gameState.beanState == GameState.gameState.BEANGOHINT || gameState.beanState == GameState.gameState.ISCOOL)
                    {
                        if (scriptNPCList.Count == gameState.listTotalNPC.Count || endingsManager.endingsSeenList.Count > 0)
                        {
                            Debug.Log("Granny Smith3");
                            _responseBox.isActive = true;
                        }
                        if (scriptNPCList.Count == gameState.listTotalNPC.Count - 1 && !gameState.listTotalNPC.Contains(GameObject.Find("Granny Smith")))
                        {
                            Debug.Log("Has NPC is true and it's beango hint AND this is granny smith");
                            _responseBox.isActive = true;
                        }
                    }
                    if (gameState.beanState == GameState.gameState.ISNOTCOOL)
                    {
                        gameState.Conversation(thisCharacter.gameObject.name, 0);
                        gameState.IsBagged();
                    }
                }

                _dialogueBox.isActive = true;
            }
            else if (Input.GetKeyDown(KeyCode.Space) && _dialogueBox.isActive == true && skippedTypewriter == false)
            {
                if (thisCharacter.gameObject.name == "Fire Hydrant")
                {
                    _dialogueBox.isActive = false;
                    _responseBox.isActive = false;
                }
                else
                {
                    skippedTypewriter = true;
                    _dialogueBox.dialogueBoxAnimator.SkipTypewriter();
                }
            }
            else if (Input.GetKeyDown(KeyCode.Space) && _dialogueBox.isActive == true && skippedTypewriter == true) {
                _dialogueBox.isActive = false;
                skippedTypewriter = false;
            }
        }
        else
        {
            if (gameState.listTotalNPC.Count == scriptNPCList.Count && gameState.beanState == GameState.gameState.ISCOOL)
            {
                gameState.IsBeanGoHint();
            }
            _dialogueBox.isActive = false;
            skippedTypewriter = false;
            _dialogueBox.dialogueBoxAnimator.StopShowingText();
            _responseBox.isActive = false;
            _canTalkBox.isActive = false;
        }

        // Checking if the response box is true and if the "Y" Button is pressed.
        if (Input.GetKeyDown(KeyCode.Y) && _responseBox.isActive == true)
        {
            if (thisCharacter.gameObject.name == "Granny Smith" && gameState.beanState == GameState.gameState.BEANGOHINT)
            {
                gameState.beanState = GameState.gameState.BEANGOHINT;
                gameState.Conversation(thisCharacter.gameObject.name, 0);
                _actManager.activateGraphicTransition = true;
                return;
            }
            if (thisCharacter.gameObject.tag != "SideNPC")
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
                    Debug.Log("Here");
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
            FMODUnity.RuntimeManager.PlayOneShot("event:/TalkBox 2");
        } else if(_canTalkBox.isActive == true && _hasPlayed == true)
        {
            _hasPlayed = false;
            FMODUnity.RuntimeManager.PlayOneShot("event:/TalkBox");
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
            spriteFlip = false;
            GlassesSpriteRenderer.flipX = false;
            BaggedSpriteRenderer.flipX = false;
            NoGlassesSpriteRenderer.flipX = false;
        } else if(movementDirection.x > 0) {
            spriteFlip = true;
            GlassesSpriteRenderer.flipX = true;
            BaggedSpriteRenderer.flipX = true;
            NoGlassesSpriteRenderer.flipX = true;
        }

        if (NoGlassesanimator.isActiveAndEnabled)
        {
            NoGlassesanimator.SetFloat("Speed", movementSpeed);
            NoGlassesanimator.SetFloat("YAxisDirection", movementDirection.y);
        }

        if (GlassesAnimator.isActiveAndEnabled) {
            GlassesAnimator.SetFloat("Speed", movementSpeed);
            GlassesAnimator.SetFloat("YAxisDirection", movementDirection.y);
        }
        if (BaggedAnimator.isActiveAndEnabled) {
            BaggedAnimator.SetFloat("Speed", movementSpeed);
            BaggedAnimator.SetFloat("YAxisDirection", movementDirection.y);
        }
    }
    public void ResetBeanSpawnPosition()
    {
        GlassesSpriteRenderer.flipX = true;
        BaggedSpriteRenderer.flipX = true;
        NoGlassesSpriteRenderer.flipX = true;

        if (NoGlassesanimator.isActiveAndEnabled)
        {
            Debug.Log("Reset Y Position");
            NoGlassesanimator.Play("BeanIdleFront");
        }

        if (GlassesAnimator.isActiveAndEnabled)
        {
            Debug.Log("Reset Y Position");
            GlassesAnimator.Play("BeanIdleFront");
        }

        if (BaggedAnimator.isActiveAndEnabled)
        {
            Debug.Log("Reset Y Position");
            BaggedAnimator.Play("BeanIdleFront");
        }
    }
}
