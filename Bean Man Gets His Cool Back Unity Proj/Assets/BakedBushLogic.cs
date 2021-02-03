using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakedBushLogic : MonoBehaviour
{
    public PlayerController playerController;

    public EndingsManager endingsManager;
    public GameState gameState;

    public bool beanInRange = false;

    public SpriteRenderer sprite;
    public Animator crazyBush;

    public string newAnimation;
    public string oldAnimation;

    // Update is called once per frame
    void Update()
    {
        if (endingsManager.endingsSeenList.Count >= 2 && gameState.beanState == GameState.gameState.ISNOTCOOL)
        {
            this.gameObject.name = "Ninja Officer Bush";
            OfficerBush();
        }
        else
        {
            this.gameObject.name = "Baked Bush";
            NormalBush();
        }

        if (newAnimation != oldAnimation)
        {
            Debug.Log("SWITCH ANIMATION");
            oldAnimation = newAnimation;
            crazyBush.Play(newAnimation);
        }

    }

    public void NormalBush()
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
    }

    public void OfficerBush()
    {
        if (playerController.GlassesSpriteRenderer.flipX == true && sprite.flipX == true)
        {
            newAnimation = "OfficerBigEye";
        }
        if (playerController.GlassesSpriteRenderer.flipX == false && sprite.flipX == true)
        {
            newAnimation = "OfficerSmallEye";
        }
        if (playerController.GlassesSpriteRenderer.flipX == true && sprite.flipX == false)
        {
            newAnimation = "OfficerBigEye";
        }
        if (playerController.GlassesSpriteRenderer.flipX == false && sprite.flipX == false)
        {
            newAnimation = "OfficerSmallEye";
        }
    }
}
