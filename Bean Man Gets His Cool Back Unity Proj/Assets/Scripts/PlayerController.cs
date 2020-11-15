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
    public Animator animator;
    public SpriteRenderer spriterenderer;

    public bool characterInRange;

    //public GameObject UI
    public UIController _canTalkBox;
    public UIController _dialogueBox;
    public UIController _responseBox;

    void Start()
    {
        _canTalkBox = GameObject.Find("CanTalkBox").GetComponent<UIController>();
        _dialogueBox = GameObject.Find("DialogueBox").GetComponent<UIController>();
        _responseBox = GameObject.Find("ResponseBox").GetComponent<UIController>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
        Move();
        Animate();
        if (characterInRange)
        {
            _canTalkBox.isActive = true;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _dialogueBox.isActive = true;
            }
        }
        else {
            _dialogueBox.isActive = false;
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
            spriterenderer.flipX = false;
        } else if(movementDirection.x > 0) {
            spriterenderer.flipX = true;
        }
        animator.SetFloat("Speed", movementSpeed);
        animator.SetFloat("YAxisDirection", movementDirection.y);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "NPC") {
            characterInRange = true;
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
