using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassesFlip : MonoBehaviour
{
    public NPC thisNPC;

    public GameObject leftGlasses;
    public GameObject rightGlasses;

    public PlayerController BeanMan;

    private SpriteRenderer leftGlassesSpriteRenderer;
    private SpriteRenderer rightGlassesSpriteRenderer;

    private void Start()
    {
        leftGlassesSpriteRenderer = leftGlasses.GetComponent<SpriteRenderer>();
        rightGlassesSpriteRenderer = rightGlasses.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (thisNPC.isLeft)
        {
            leftGlasses.SetActive(true);
            rightGlasses.SetActive(false);
            rightGlassesSpriteRenderer.sprite = leftGlassesSpriteRenderer.sprite;


        }
        if (!thisNPC.isLeft)
        {
            leftGlasses.SetActive(false);
            rightGlasses.SetActive(true);
            rightGlassesSpriteRenderer.sprite = leftGlassesSpriteRenderer.sprite;
        }
    }
}
