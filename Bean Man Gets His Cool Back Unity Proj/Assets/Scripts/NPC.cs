using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public bool hasSpoken = false;
    public GameObject glasses = default;
    public GameObject noGlasses = default;
    public GameObject actualGlasses = default;
    public SpriteRenderer spriteRenderer;
    public List<Sprite> glassesList = new List<Sprite>();

    public GlassesMove glassesMove;

    public bool transitionWinner;

    public GameObject trafficContainer;

    public GameObject ExclamationPoint;
    public ExclamationPoint exclamationUI;

    public bool isWinner = false;

    public GameState gameState = default;
    public GameObject player = default;
    public bool isLeft = true;

    public EndingsManager endingsManager;

    public ChickPeaLogic chickPeaLogic;

    public float angle = 90f;
    public float distance = 1f;

    public void Start()
    {
        SetExclamation();
        //spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Debug.Log(this.name);
        if (this.gameObject.name == "Corn Lady" && endingsManager.endingsSeenList.Count > 0) {
            return;
        }
        glasses.SetActive(false);
        noGlasses.SetActive(true);
        actualGlasses.SetActive(true);
    }

    public void ResetCoolStateNPC() {
        actualGlasses.SetActive(true);
        glasses.SetActive(false);
        noGlasses.SetActive(true);
        isWinner = false;
        if (chickPeaLogic != null)
        {
            chickPeaLogic.ChickPeaWizardMode();
        }
    }

    public void Update()
    {

        var noglassesSpriteRenderer = noGlasses.GetComponent<SpriteRenderer>();
        var glassesSpriteRenderer = glasses.GetComponent<SpriteRenderer>();
        //var actualGlassesSpriteRenderer = glasses.GetComponent<SpriteRenderer>();
        

        if (gameObject.name == "Fire Hydrant") {
            return;
        }

        if (gameObject.name == "Poparazzi Corn")
        {
            return;
        }

        if (player.transform.position.x < transform.position.x)
        {

            if (this.gameObject.name == "Corn Lady" && gameState.beanState == GameState.gameState.ISNOTCOOL)
            {
                noglassesSpriteRenderer.flipX = true;
                glassesSpriteRenderer.flipX = true;
                //actualGlassesSpriteRenderer.flipX = true;
                return;
            }
            isLeft = true;
            noglassesSpriteRenderer.flipX = false;
            glassesSpriteRenderer.flipX = false;
            //actualGlassesSpriteRenderer.flipX = false;

        } else
        {
            if (this.gameObject.name == "Corn Lady" && gameState.beanState == GameState.gameState.ISNOTCOOL)
            {
                noglassesSpriteRenderer.flipX = false;
                glassesSpriteRenderer.flipX = false;
                //actualGlassesSpriteRenderer.flipX = false;
                return;
            }
            isLeft = false;
            noglassesSpriteRenderer.flipX = true;
            glassesSpriteRenderer.flipX = true;
            //actualGlassesSpriteRenderer.flipX = true;
        }
        
    }

    public void ShowTrafficCone()
    {
        trafficContainer.SetActive(true);
    }

    public void RemoveTrafficCone()
    {
        trafficContainer.SetActive(false);
    }
    
    public void ShowGlasses()
    {
        RemoveTrafficCone();
        glasses.SetActive(true);
        actualGlasses.SetActive(true);
        noGlasses.SetActive(false);
        if (chickPeaLogic != null)
        {
            chickPeaLogic.ChickPeaWizardMode();
        }
    }

    public void RemoveGlasses()
    {
        Debug.Log(this.gameObject.name);
        RemoveTrafficCone();
        actualGlasses.SetActive(false);
    }

    public void AddExclamation()
    {
        Debug.Log(this.gameObject.name + " ADD EXCLAMATION");
        hasSpoken = true;
        ExclamationPoint.SetActive(true);
    }

    public void RemoveExclamation()
    {
        exclamationUI.hideUI = true;
        Debug.Log(this.gameObject.name);
        hasSpoken = true;
    }
    //SET THIS AT THE START OF THE GAME
    public void SetExclamation()
    {
        if (gameState.beanState == GameState.gameState.ISCOOL && endingsManager.endingsSeenList.Count == 0)
        {
            if (this.gameObject.name != "Granny Smith")
            {

                ExclamationPoint.SetActive(true);
            }
            else
            {
                ExclamationPoint.SetActive(false);
            }
        }
        else if(gameState.beanState == GameState.gameState.BEANGOHINT && endingsManager.endingsSeenList.Count > 0)
        {
            if (!transitionWinner)
            {

                ExclamationPoint.SetActive(false);
            }
            else
            {
                ExclamationPoint.SetActive(true);
            }
        }
        else
        {
            if (this.gameObject.name != "Granny Smith")
            {
                ExclamationPoint.SetActive(false);
            }
            else
            {
                hasSpoken = false;
                ExclamationPoint.SetActive(true);
            }
        }
        if (this.gameObject.name == "Fire Hydrant")
        {
            ExclamationPoint.SetActive(false);
            return;
        }
    }
}
