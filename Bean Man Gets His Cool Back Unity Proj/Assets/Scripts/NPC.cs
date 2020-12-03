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

    public bool isWinner = false;

    public GameState gameState = default;
    public GameObject player = default;

    public float angle = 90f;
    public float distance = 1f;

    public void Start()
    {
        //spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Debug.Log(this.name);
        glasses.SetActive(false);
        noGlasses.SetActive(true);
    }

    public void Update()
    {
        var noglassesSpriteRenderer = noGlasses.GetComponent<SpriteRenderer>();
        var glassesSpriteRenderer = glasses.GetComponent<SpriteRenderer>();
        var actualGlassesSpriteRenderer = glasses.GetComponent<SpriteRenderer>();

        if (player.transform.position.x < transform.position.x)
        {
            noglassesSpriteRenderer.flipX = false;
            glassesSpriteRenderer.flipX = false;
            actualGlassesSpriteRenderer.flipX = false;
        } else
        {
            noglassesSpriteRenderer.flipX = true;
            glassesSpriteRenderer.flipX = true;
            actualGlassesSpriteRenderer.flipX = true;
        }

        if (this.name == "Birthday Cake" && Vector3.Distance(transform.position, player.transform.position) <= distance)
        {

            var angleTest = Vector3.Angle(player.transform.right, transform.position - player.transform.position);
            Debug.Log(angleTest);

            if (Vector3.Angle(player.transform.right, transform.position - player.transform.position) < angle)
            {
                Debug.Log("Facing Birthday Cake");
            }
        }
        
    }

    public void ShowGlasses()
    {
        glasses.SetActive(true);
        noGlasses.SetActive(false);
    }

    
 
}
