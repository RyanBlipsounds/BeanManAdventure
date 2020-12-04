using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActManager : MonoBehaviour
{
    public bool fadeToBlack;
    public SpriteRenderer BlackScreen;
    public Color ColorValue;

    public GameState m_gameState;

    private float blackScreenTimeToFade = 0;

    private GameObject EndingScreen;
    public GameObject BeangoScreen;
    public GameObject ChickPeaEnding;
    public GameObject BeanManWinEnding;
    public GameObject LinaBeanEnding;
    public GameObject PeanutTwinEnding;
    public GameObject GrannySmithEnding;
    public GameObject FireHydrantEnding;
    public GameObject SlimSausageWinning;
    public GameObject BeanManLeavesBagged;
    public GameObject BeanManLeaves;
    public GameObject GreenBenEnding;
    public GameObject BirthdayCakeEnding;

    public GameObject BeanManSpawnPosition;

    public PlayerController _playerController;

    public UIController _narrationBox;

    private float graphicShowTime = 0;

    public bool switchFade = false;

    public bool activateGraphicTransition = false;
    private bool hasPlayed = false;

    public enum sceneState
    {
        started,
        graphic,
        backtoscene
    }
    public sceneState sceneTransitionState = sceneState.started;

    void Start()
    {
        _narrationBox = GameObject.Find("NarrationBox").GetComponent<UIController>();
        BlackScreen = GameObject.Find("BlackScreen").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (activateGraphicTransition == true) {
            if (m_gameState.beanState == GameState.gameState.ISNOTCOOL || m_gameState.beanState == GameState.gameState.ISCOOL || m_gameState.beanState == GameState.gameState.BEANGOHINT) {
                EndingScreen = BeangoScreen;
                LoadGraphic(EndingScreen);
            }
            if (m_gameState.beanState == GameState.gameState.ISBAGGED || m_gameState.beanState == GameState.gameState.ENDING)
            {
                LoadGraphic(EndingScreen);
            }
        }
    }

    private void FadeToBlack(sceneState state, GameObject graphic, GameObject oldGraphic) {
        float blackScreenTmp;

        blackScreenTmp = Mathf.Lerp(0, 1, blackScreenTimeToFade / 2);

        BlackScreen.color = new Color(0, 0, 0, blackScreenTmp);

        if (!switchFade)
        {
            blackScreenTimeToFade += Time.deltaTime;
        }
        else {
            if (m_gameState.beanState == GameState.gameState.BEANGOHINT && hasPlayed == false) {
                Debug.Log("PIZZA");
                m_gameState.IsNotCool();
                hasPlayed = true;
            }
            if (m_gameState.beanState == GameState.gameState.ISBAGGED && hasPlayed == false)
            {
                Debug.Log("PIZZA33");
                m_gameState.Ending();
                hasPlayed = true;
            }
            blackScreenTimeToFade -= Time.deltaTime;
        }

        if (blackScreenTmp >= 1)
        {
            if (graphic != null)
            {
                _narrationBox.isActive = true;
                graphic.SetActive(true);
            }
            else
            {
                _narrationBox.isActive = false;
                oldGraphic.SetActive(false);
            }
            switchFade = true;
        }
        if (blackScreenTmp == 0 && switchFade == true)
        {
            switchFade = false;
            sceneTransitionState = state;
            blackScreenTmp = 0;
            blackScreenTimeToFade = 0;
        }
    }

    public void LoadGraphic(GameObject graphic)
    {

        activateGraphicTransition = true;

        //Debug.Log("Graphic " + graphicShowTime);
        if (sceneTransitionState == sceneState.started) {
            FadeToBlack(sceneState.graphic, graphic, null);
        }
        if (sceneTransitionState == sceneState.graphic) {
            graphicShowTime += Time.deltaTime;
            if (graphicShowTime >= 6) {
                sceneTransitionState = sceneState.backtoscene;
            }
        }
        if (sceneTransitionState == sceneState.backtoscene)
        {
            FadeToBlack(sceneState.started, null, graphic);
            if (sceneTransitionState == sceneState.started) {
                activateGraphicTransition = false;
                graphicShowTime = 0;
                hasPlayed = false;
                return;
            }
        }
    }

    public void LoadEnding(string ending) {
        if (ending == "Bean") {
            EndingScreen = BeanManWinEnding;
        }
        if (ending == "Lina Bean") {
            EndingScreen = LinaBeanEnding;
        }
        if (ending == "Chickpea Deputy")
        {
            EndingScreen = ChickPeaEnding;
        }
        if (ending == "Granny Smith") {
            EndingScreen = GrannySmithEnding;
        }
        if (ending == "Peanut Twins")
        {
            EndingScreen = PeanutTwinEnding;
        }
        if (ending == "Slim Sausage")
        {
            EndingScreen = SlimSausageWinning;
        }
        if (ending == "Birthday Cake")
        {
            EndingScreen = BirthdayCakeEnding;
        }
        if (ending == "GreenBen")
        {
            EndingScreen = GreenBenEnding;
        }
        if (ending == "Fire Hydrant")
        {
            EndingScreen = FireHydrantEnding;
        }


        Debug.Log(ending);

        activateGraphicTransition = true;

        LoadGraphic(EndingScreen);
    }

    //In Load New Act, it should probably take dictionary arguments 
    public void LoadNewAct()
    {
    }

}
