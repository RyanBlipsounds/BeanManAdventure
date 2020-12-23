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

    public Button EndingsReplay;
    public GameObject EndingsScreen;



    private void Start()
    {
        MMPlayButton.onClick.AddListener(GameStartClicked);
        EndingsReplay.onClick.AddListener(EndingsClicked);
        pc.bagMoving = true;
    }

    

    private void OnDestroy()
    {
        MMPlayButton.onClick.RemoveListener(GameStartClicked);
        EndingsReplay.onClick.RemoveListener(EndingsClicked);
    }
    private void GameStartClicked()
    {
        MainMenu.SetActive(false);
        pc.bagMoving = false;
        // Allow Movement
    }

    private void EndingsClicked()
    {
        EndingsScreen.SetActive(true);
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
