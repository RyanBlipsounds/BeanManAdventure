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

    public GameObject BeangoScreen;
    public GameObject ChickPeaLoseEnding;
    public GameObject BeanManWinEnding;

    public GameObject BeanManSpawnPosition;

    private float graphicShowTime = 0;

    public bool switchFade = false;

    public enum sceneState
    {
        started,
        graphic,
        backtoscene
    }
    public sceneState sceneTransitionState = sceneState.started;

    void Start()
    {
        BlackScreen = GameObject.Find("BlackScreen").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        LoadGraphic();
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
            blackScreenTimeToFade -= Time.deltaTime;
        }

        if (blackScreenTmp >= 1)
        {
            if (graphic != null)
            {
                graphic.SetActive(true);
            }
            else
            {
                oldGraphic.SetActive(false);
            }
            switchFade = true;
        }
        if (blackScreenTmp < 0)
        {
            switchFade = false;
            sceneTransitionState = state;
            blackScreenTmp = 0;
        }
    }

    private void LoadGraphic()
    {
        Debug.Log(sceneTransitionState);
        Debug.Log("Graphic " + graphicShowTime);
        if (sceneTransitionState == sceneState.started) {
            FadeToBlack(sceneState.graphic, BeangoScreen, null);
        }
        if (sceneTransitionState == sceneState.graphic) {
            graphicShowTime += Time.deltaTime;
            if (graphicShowTime >= 10) {
                //sceneTransitionState = sceneState.backtoscene;
            }
        }
        if (sceneTransitionState == sceneState.backtoscene)
        {
            FadeToBlack(sceneState.started, null, BeangoScreen);
        }
    }

    private void LoadEnding(SpriteRenderer ending) {

    }

    //In Load New Act, it should probably take dictionary arguments 
    public void LoadNewAct()
    {
    }

}
