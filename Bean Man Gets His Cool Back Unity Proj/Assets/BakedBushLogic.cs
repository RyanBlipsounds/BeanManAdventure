using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakedBushLogic : MonoBehaviour
{
    public PlayerController playerController;
    public bool beanInRange = false;

    public SpriteRenderer sprite;
    public Animator crazyBush;

    public string newAnimation;
    public string oldAnimation;

    // Update is called once per frame
    void Update()
    {

        if (playerController.GlassesSpriteRenderer.flipX == true && sprite.flipX == true)
        {
            newAnimation = "BigEyeBush";
        }
        if (playerController.GlassesSpriteRenderer.flipX == false && sprite.flipX == true)
        {
            newAnimation = "SmallEyeBush";
        }
        if (playerController.GlassesSpriteRenderer.flipX == true && sprite.flipX == false)
        {
            newAnimation = "BigEyeBush";
        }
        if (playerController.GlassesSpriteRenderer.flipX == false && sprite.flipX == false)
        {
            newAnimation = "SmallEyeBush";
        }

        if (newAnimation != oldAnimation)
        {
            Debug.Log("SWITCH ANIMATION");
            oldAnimation = newAnimation;
            crazyBush.Play(newAnimation);
        }

    }
}
