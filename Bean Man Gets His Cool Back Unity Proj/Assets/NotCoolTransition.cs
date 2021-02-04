using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotCoolTransition : MonoBehaviour
{

    public List<TransitionActor> transitionsList = new List<TransitionActor>();
    public PlayerController playerController;

    public void Activate(GameObject character)
    {
        ScreenFade.main.FadeIn();
        playerController.bagMoving = true;
        foreach (TransitionActor actor in transitionsList)
        {
            if (actor.gameObject.name == playerController.thisCharacter.gameObject.name)
            {

            }
        }
    }
    public void Deactive()
    {
        ScreenFade.main.FadeOut();
        Invoke("BeanManMove", 2f);
    }

    public void BeanManMove()
    {
        playerController.bagMoving = false;
    }
}