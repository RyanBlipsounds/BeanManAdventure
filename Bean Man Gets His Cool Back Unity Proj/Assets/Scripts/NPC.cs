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

    public float angle = 90f;
    public float distance = 1f;

    public void Start()
    {
        //spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Debug.Log(this.name);
        glasses.SetActive(false);
        noGlasses.SetActive(true);
    }

    public void ResetCoolStateNPC() {
        glasses.SetActive(false);
        noGlasses.SetActive(true);
        hasSpoken = false;
        isWinner = false;
    }

    public void Update()
    {
        var noglassesSpriteRenderer = noGlasses.GetComponent<SpriteRenderer>();
        var glassesSpriteRenderer = glasses.GetComponent<SpriteRenderer>();
        var actualGlassesSpriteRenderer = glasses.GetComponent<SpriteRenderer>();

        if (gameObject.name == "Fire Hydrant") {
            return;
        }

        if (player.transform.position.x < transform.position.x)
        {
            isLeft = true;
            noglassesSpriteRenderer.flipX = false;
            glassesSpriteRenderer.flipX = false;
            actualGlassesSpriteRenderer.flipX = false;
        } else
        {
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

    public void ShowGlasses()
    {
        trafficContainer.SetActive(false);
        glasses.SetActive(true);
        noGlasses.SetActive(false);
    }
}
