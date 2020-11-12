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

    //public GameObject UI

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
        Move();
        Animate();
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
            
        }
    }
}
