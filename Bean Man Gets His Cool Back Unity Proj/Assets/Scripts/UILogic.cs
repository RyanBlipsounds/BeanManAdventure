using System;
using UnityEngine;
using UnityEngine.UI;

public class UILogic : MonoBehaviour
{
    // Main Menu Game Objects
    public GameObject MainMenu;
    public Button MMPlayButton;
    public PlayerController pc;
    public GameObject endingsEncountered;
    public Text endingsNumber;

    public GameState _gamestate = default;



    private void Start()
    {
        MMPlayButton.onClick.AddListener(GameStartClicked);
        pc.bagMoving = true;
    }

    

    private void OnDestroy()
    {
        MMPlayButton.onClick.RemoveListener(GameStartClicked);
    }
    private void GameStartClicked()
    {
        MainMenu.SetActive(false);
        pc.bagMoving = false;
        // Allow Movement
    }

    /// <summary>
    /// Update the endings seen count
    /// </summary>
    /// <param name="count">int for the amount of endings seen (from gamestate)</param>
    public void UpdateEndingsCount(int count)
    {
        endingsEncountered.SetActive(true);
        endingsNumber.text = count + " / 10";
    }

}
