using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public bool hasSpoken = false;
    public GameObject glasses = default;
    public GameObject noGlasses = default;
    public SpriteRenderer spriteRenderer;
    public List<Sprite> glassesList = new List<Sprite>();

    public bool isWinner = false;

    public GameState gameState = default;

    public void Start()
    {
        //spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        glasses.SetActive(false);
        noGlasses.SetActive(true);
    }

    public void ShowGlasses()
    {
        glasses.SetActive(true);
        noGlasses.SetActive(false);
    }
}
