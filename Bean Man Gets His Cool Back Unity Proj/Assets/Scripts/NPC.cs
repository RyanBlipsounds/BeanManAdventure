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

    public GameObject trafficContainer;

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
        //spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Debug.Log(this.name);
        if (this.gameObject.name == "Corn Lady" && endingsManager.endingsSeenList.Count > 0) {
            return;
        }
        glasses.SetActive(false);
        noGlasses.SetActive(true);
    }

    public void ResetCoolStateNPC() {
        glasses.SetActive(false);
        noGlasses.SetActive(true);
        hasSpoken = false;
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
        var actualGlassesSpriteRenderer = glasses.GetComponent<SpriteRenderer>();
        

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
                actualGlassesSpriteRenderer.flipX = true;
                return;
            }
            isLeft = true;
            noglassesSpriteRenderer.flipX = false;
            glassesSpriteRenderer.flipX = false;
            actualGlassesSpriteRenderer.flipX = false;

        } else
        {
            if (this.gameObject.name == "Corn Lady" && gameState.beanState == GameState.gameState.ISNOTCOOL)
            {
                noglassesSpriteRenderer.flipX = false;
                glassesSpriteRenderer.flipX = false;
                actualGlassesSpriteRenderer.flipX = false;
                return;
            }
            isLeft = false;
            noglassesSpriteRenderer.flipX = true;
            glassesSpriteRenderer.flipX = true;
            actualGlassesSpriteRenderer.flipX = true;
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
        noGlasses.SetActive(false);
        if (chickPeaLogic != null)
        {
            chickPeaLogic.ChickPeaWizardMode();
        }
    }
}
