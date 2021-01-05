using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonNPC : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    
    public GameObject player = default;
    public bool isLeft = true;


    public void Start()
    {
        //spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Debug.Log(this.name);
    }

    public void Update()
    {
        if (player.transform.position.x < transform.position.x)
        {
            isLeft = true;
            spriteRenderer.flipX = true;
        }
        else
        {
            isLeft = false;
            spriteRenderer.flipX = false;
        }
    }
}
