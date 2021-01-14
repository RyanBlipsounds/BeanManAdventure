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

    public GameObject PauseScreen;

    public GameObject CreditsScreen;
    public Button CreditsButton;

    public EndingsManager endingsManager;
    public QuestList questList;
    public ActManager _actManager;
   

    public bool firstPlayClick = false;

    private void Start()
    {
        MMPlayButton.onClick.AddListener(GameStartClicked);
        EndingsReplay.onClick.AddListener(EndingsClicked);
        CreditsButton.onClick.AddListener(CreditsClicked);
        pc.bagMoving = true;
    }

    private void Update()
    {
        if (PauseScreen.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseScreen.SetActive(false);
            }
        }
        else if (!CreditsScreen.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseScreen.SetActive(true);
            }
        }

        
    }

    private void OnDestroy()
    {
        MMPlayButton.onClick.RemoveListener(GameStartClicked);
        EndingsReplay.onClick.RemoveListener(EndingsClicked);
        CreditsButton.onClick.RemoveListener(CreditsClicked);
    }
    private void GameStartClicked()
    {
        _gamestate.StartMusic.setParameterByName("Sax Transition", 1);
        MainMenu.SetActive(false);
        pc.bagMoving = false;
        // Allow Movement
        if (firstPlayClick == false) {
            questList.ActivateQuestItem("Everyone in Town");
            firstPlayClick = true;
        }

        if (endingsManager.endingsSeenList.Contains(_actManager.GrannySmithEnding)) {
            questList.CompleteQuestItem("Queen");
        }
        if (endingsManager.endingsSeenList.Contains(_actManager.BirthdayCakeEnding))
        {
            questList.CompleteQuestItem("Club");
            questList.ActivateQuestItem("Traffic Cone");
        }
        if (endingsManager.endingsSeenList.Contains(_actManager.FireHydrantEnding))
        {
            questList.CompleteQuestItem("Lose to Fire Hydrant");
        }
        if (endingsManager.endingsSeenList.Contains(_actManager.LinaBeanEnding))
        {
            questList.CompleteQuestItem("Lina Bean");
        }
        if (endingsManager.endingsSeenList.Contains(_actManager.GreenBenEnding))
        {
            questList.CompleteQuestItem("GreenBen");
        }
        if (endingsManager.endingsSeenList.Contains(_actManager.BeanManLeavesTown))
        {
            questList.CompleteQuestItem("Escape");
        }
        if (endingsManager.endingsSeenList.Contains(_actManager.SlimSausageWinning))
        {
            questList.CompleteQuestItem("Slim Sausage");
        }
        if (endingsManager.endingsSeenList.Contains(_actManager.PeanutTwinEnding))
        {
            questList.CompleteQuestItem("Peanut Twin");
            questList.CompleteQuestItem("Butter");
        }
        if (endingsManager.endingsSeenList.Contains(_actManager.ChickPeaEnding)) {
            questList.CompleteQuestItem("Chickpea");
        }
        if (endingsManager.endingsSeenList.Contains(_actManager.BeanManWinEnding))
        {
            questList.CompleteQuestItem("Get Your Cool Back");
            questList.ActivateQuestItem("Peanut Twin");
            questList.ActivateQuestItem("Lina Bean");
            questList.ActivateQuestItem("GreenBen");
            questList.ActivateQuestItem("Chickpea");
            questList.ActivateQuestItem("Club");
            questList.ActivateQuestItem("Slim Sausage");
        }
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

    public void CreditsClicked()
    {
        CreditsScreen.SetActive(true);
    }



}
