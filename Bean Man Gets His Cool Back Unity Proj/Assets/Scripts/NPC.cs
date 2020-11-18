using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public bool hasSpoken = false;
    public GameObject glasses = default;
    public GameObject noGlasses = default;

    public GameState gameState = default;

    public void Start()
    {
        glasses.SetActive(false);
        noGlasses.SetActive(true);
    }

    public void ShowGlasses()
    {
        glasses.SetActive(true);
        noGlasses.SetActive(false);
    }
}
