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

    private bool _hasPlayed = false;

    private float angle = 10f;

    void Start()
    {
        Bag.transform.position = m_BagStartPosition.transform.position;

        GlassesBeanMan.SetActive(true);
        NoGlassesBeanMan.SetActive(false);
        BaggedBeanMan.SetActive(false);

        _canTalkBox = GameObject.Find("CanTalkBox").GetComponent<UIController>();
        Debug.Log("turd 2");
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
            Debug.Log("turd 3");
            if (Input.GetKeyDown(KeyCode.Space) && _dialogueBox.isActive == false)
            {
                if (!scriptNPCList.Contains(thisCharacter.GetComponent<NPC>()))
                {
                    scriptNPCList.Add(thisCharacter.GetComponent<NPC>());
                }

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
                _responseBox.isActive = false;
            }
            if (Input.GetKeyDown(KeyCode.Y) && _responseBox.isActive == true) {
                if (thisNPCList.isWinner)
                {
                    _actManager.LoadEnding("Bean");
                }
                else {
                    foreach (NPC character in scriptNPCList) {
                        if (character.isWinner == true) {
                            _actManager.LoadEnding(character.gameObject.name);
                            break;
                        }
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.N) && _responseBox.isActive == true)
            {
                _dialogueBox.isActive = false;
                _responseBox.isActive = false;
            }

        }
        else
        {
            _dialogueBox.isActive = false;
            _responseBox.isActive = false;
            _canTalkBox.isActive = false;
            

        }
        if (_canTalkBox.isActive == false && _hasPlayed == false)
        {
            _hasPlayed = true;
            // Bruh this sound is horrendous
            //FMODUnity.RuntimeManager.PlayOneShot("event:/TalkBox");
            Debug.Log("Sadboi");
        } else if(_canTalkBox.isActive == true && _hasPlayed == true)
        {
            _hasPlayed = false;
            //FMODUnity.RuntimeManager.PlayOneShot("event:/TalkBox");
            Debug.Log("Sadboi 2");
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
}
