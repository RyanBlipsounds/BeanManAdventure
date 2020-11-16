﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public bool hasSpoken = false;
    public GameObject glasses = default;
    public GameState gameState = default;

    public void ShowGlasses()
    {
        glasses.SetActive(true);
    }

}
