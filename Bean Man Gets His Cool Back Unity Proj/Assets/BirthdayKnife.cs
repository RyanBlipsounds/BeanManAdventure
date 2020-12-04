using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirthdayKnife : MonoBehaviour
{
    public NPC thisNPC;

    public GameObject LeftKnife;
    public GameObject RightKnife;

    public PlayerController BeanMan;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (thisNPC.isLeft && BeanMan.GlassesSpriteRenderer.flipX == true)
        {
            LeftKnife.SetActive(false);
            RightKnife.SetActive(false);
            return;
        }

        if (!thisNPC.isLeft && BeanMan.GlassesSpriteRenderer.flipX == false)
        {
            LeftKnife.SetActive(false);
            RightKnife.SetActive(false);
            return;
        }

        if (thisNPC.isLeft)
        {
            LeftKnife.SetActive(true);
            RightKnife.SetActive(false);
        }
        if (!thisNPC.isLeft)
        {
            LeftKnife.SetActive(false);
            RightKnife.SetActive(true);
        }
    }
}
