using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActManager : MonoBehaviour
{
    public bool fadeToBlack;
    public SpriteRenderer BlackScreen;
    public Color ColorValue;

    public GameState m_gameState;
    public EndingsManager _endingsManager = default;

    private float blackScreenTimeToFade = 0;

    public GameObject EndingScreen;
    public GameObject BeangoScreen;
    public GameObject ChickPeaEnding;
    public GameObject BeanManWinEnding;
    public GameObject LinaBeanEnding;
    public GameObject PeanutTwinEnding;
    public GameObject GrannySmithEnding;
    public GameObject FireHydrantEnding;
    public GameObject SlimSausageWinning;
    public GameObject BeanManLeavesBagged;
    public GameObject BeanManLeavesTown;
    public GameObject GreenBenEnding;
    public GameObject BirthdayCakeEnding;
    public GameObject BeanManUncoolEnding;
    public GameObject credits;
    public GameObject spaceBar;

    public string EndingScreenText;
    public string BeangoScreenText = "It was an intense night of Beango, but Granny and Beanman were an unstoppable force. It was all hazy, but it was one of the greatest nights of Beanman and Granny's lives. Although something didn't feel quite right when Bean Man awoke in the morning. Someone had stolen his glasses. And he has to find out who took them.";
    public string ChickPeaEndingText = "In the quest to get his cool back, Bean Man ran the risk of selecting what he thought were his own glasses, but failed to recognize that Chickpea Deputy had been wearing them all along. The glasses gave our Deputy enough confidence to unlock his wizard powers, thus taking over the town, and preventing Bean Man from getting his cool back.";
    public string BeanManWinEndingText = "There wasn't a snowballs chance in Beans hell that Beanman was going to lose his cool today. In this journey, Bean Man thought that he had lost his cool when he lost his glasses, but really the cool was inside of him all along.";
    public string BeanManLeavesCoolText = "BeanMan knew his cool was so high above Beantown's coolest bean, that he decided to leave. Even Lina Bean couldn't handle the sheer coolness of his cool. Is Bean Man just too cool for the rest of the Beans? He doesn't say so, but he must know so.";
    public string LinaBeanEndingText = "Once Bean Man realized that Lina had his glasses, it was too late. Lina had grown 300 meters and made her way across the Pacific Ocean to Tokyo Japan. Tokyo faced one of it's greatest disasters in Japan's history as Lina tore down the city. The recovery still remains to be seen. Also, Lina reveals that she was edamame the whole time.";
    public string PeanutTwinEndingText = "After a tiring life of being referred to as the second twin, Peanut Twin number two took it upon himself to invent peanut butter with just a simple sacrifice from Peanut Twin number one and his good friend Butter. Peanut Twin number two made millions off of the idea, lost two of his best friends, and remained as Peanut Twin number two.";
    public string GrannySmithEndingText = "As it turns out, Granny was always jealous of Bean Man. Being only the second coolest in town carries a heavy burden that she just couldn't bare. Bean Man could do nothing but watch as his closest homie took the throne and made the town hers. Stay tuned to find out what happens in the Bean Man sequel. Coming in 2077.";
    public string FireHydrantEndingText = ".....Seriously? You let a fire hydrant beat you? A fire hydrant is cooler than you. Just let that sink in.";
    public string SlimSausageWinningText = "Considering that Slim Sausage was a masterclass rapper, there was no chance that Bean Man could keep up with his cool once Sausage had his glasses. Slim Sausage went on to have an illustrious career where he would tour with Frank Sinatra to perform in front of millions, as well as performing for popular figures like Oprah Winbean and Pope Beanedict XVII.";
    public string BeanManLeavesBaggedText = "There was no point. Bean Man just didn't belong. Bean just felt that his life filled with cool had come to an end. He decided to move on to his next journey.";
    public string BeanManLeavesUncoolText = "There was no point. Bean Man just didn't belong. Bean just felt that his life filled with cool had come to an end. He decided to move on to his next journey.";
    public string GreenBenEndingText = "With his cool shiny new glasses, Green Ben was looking and feeling better than ever. But because Green Ben is so selfless, he decided to give Bean Man his glasses back. And even though the shades were on Beanman once again, GreenBen was now the new king in town.";
    public string BirthdayCakeEndingText = "Once the glasses belonged to Birthday Cake, they had the confidence to finally take the full measure they had been wanting to commit to this whole time... to become a delicious treat for their friends. Birthday Cakes spirit was no longer trapped on earth, and could now live a long and prosperous afterlife.";

    public GameObject BeanManSpawnPosition;

    public PlayerController _playerController;

    public UIController _narrationBox;

    private float graphicShowTime = 0;

    public bool switchFade = false;
    public bool creditsRolling = false;

    public bool activateGraphicTransition = false;
    private bool hasPlayed = false;

    public UIController spacebar;

    public FMOD.Studio.EventInstance EndingMusicEvent;
    public FMOD.Studio.EventInstance IsNotCoolMusicEvent;
    public FMOD.Studio.EventInstance NarrationVOEvent;

    string FMODMusic = "event:/Good Ending";
    string FMODVO = "event:/VO_Beango";
    float graphicTime = 20f;
    public float fadeTime = 2f;

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
        if (activateGraphicTransition == true)
        {
            if (m_gameState.beanState == GameState.gameState.BEANGOHINT || m_gameState.beanState == GameState.gameState.ISNOTCOOL) {
                EndingScreen = BeangoScreen;
                EndingScreenText = BeangoScreenText;
                graphicTime = 20f;
                FMODMusic = "event:/Beango Music";
                FMODVO = "event:/VO_Beango";
            }
            if (m_gameState.beanState == GameState.gameState.ISCOOL)
            {
                //LoadGraphic(EndingScreen);
            }
            if (m_gameState.beanState == GameState.gameState.ISBAGGED || m_gameState.beanState == GameState.gameState.ENDING)
            {
                //LoadGraphic(EndingScreen);
            }
            LoadGraphic(EndingScreen, FMODMusic, FMODVO, graphicTime);
        }
    }

    private void FadeToBlack(sceneState state, GameObject graphic, GameObject oldGraphic, string FMODMusic, string FMODVO)
    {
        float blackScreenTmp;

        blackScreenTmp = Mathf.Lerp(0, 1, blackScreenTimeToFade / fadeTime);

        BlackScreen.color = new Color(0, 0, 0, blackScreenTmp);

        if (!switchFade)
        {
            blackScreenTimeToFade += Time.deltaTime;
        }
        else {
            if (m_gameState.beanState == GameState.gameState.BEANGOHINT && hasPlayed == false)
               
            {
                fadeTime = 2f;
                NarrationVOEvent = FMODUnity.RuntimeManager.CreateInstance(FMODVO);
                EndingMusicEvent = FMODUnity.RuntimeManager.CreateInstance(FMODMusic);
                EndingMusicEvent.start();
                NarrationVOEvent.start();
                m_gameState.IsNotCool();
                hasPlayed = true;
            }else if (m_gameState.beanState == GameState.gameState.ISBAGGED || m_gameState.beanState == GameState.gameState.WRONGBAGGED)
            {
                if (hasPlayed == false) {
                    fadeTime = 2f;
                    IsNotCoolMusicEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                    _playerController.WrongMusicEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                    _playerController.WrongBeatEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                    NarrationVOEvent = FMODUnity.RuntimeManager.CreateInstance(FMODVO);
                    EndingMusicEvent = FMODUnity.RuntimeManager.CreateInstance(FMODMusic);
                    EndingMusicEvent.start();
                    NarrationVOEvent.start();
                    m_gameState.Ending();
                    hasPlayed = true;
                }
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
                if (m_gameState.beanState == GameState.gameState.ENDING) {
                    //m_gameState.Ending();
                    ResetCredits();
                    m_gameState.beanState = GameState.gameState.ISCOOL;
                }

                if (m_gameState.beanState == GameState.gameState.ISNOTCOOL)
                {
                    IsNotCoolMusicEvent = FMODUnity.RuntimeManager.CreateInstance("event:/NotCoolMusic");
                    IsNotCoolMusicEvent.start();
                }

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

    public void LoadGraphic(GameObject graphic, string FMODMusic, string FMODVO, float graphicTime)
    {
        activateGraphicTransition = true;
        EndingScreen = graphic;

        if (m_gameState.beanState == GameState.gameState.WRONGBAGGED)
        {
            fadeTime = 3.5f;
        }
        else {
            fadeTime = 2f;
        }

        if (creditsRolling && sceneTransitionState != sceneState.backtoscene)
        {
            RollCredits();
        }
        
        if (sceneTransitionState == sceneState.started)
        {
            FadeToBlack(sceneState.graphic, graphic, null, FMODMusic, FMODVO);
        }
        if (sceneTransitionState == sceneState.graphic)
        {
            graphicShowTime += Time.deltaTime;
            if (_endingsManager.endingsSeenList.Count > 1)
            {
                if (graphicShowTime > 1)
                {
                    spacebar.isActive = true;
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    sceneTransitionState = sceneState.backtoscene;
                }
                if (graphicShowTime >= graphicTime)
                {
                    sceneTransitionState = sceneState.backtoscene;
                }
            }
            else if (graphic != BeangoScreen && _endingsManager.endingsSeenList.Count <= 1)
            {
                if (graphicShowTime >= 40)
                {
                    sceneTransitionState = sceneState.backtoscene;
                }

                if (graphicShowTime >= graphicTime && !creditsRolling)
                {
                    Debug.Log("Credits Rolling");
                    creditsRolling = true;
                }
            }
            else if (graphic == BeangoScreen) {
                if (_endingsManager.endingsSeenList.Count >= 1)
                {
                    if (graphicShowTime > 1)
                    {
                        spacebar.isActive = true;
                    }
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        sceneTransitionState = sceneState.backtoscene;
                    }
                }
                if (graphicShowTime >= graphicTime)
                {
                    sceneTransitionState = sceneState.backtoscene;
                }
            }
        }
        if (sceneTransitionState == sceneState.backtoscene)
        {
            NarrationVOEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            EndingMusicEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            Debug.Log("STOP TEST");

            spacebar.isActive = false;
            FadeToBlack(sceneState.started, null, graphic, FMODMusic, FMODVO);
            if (sceneTransitionState == sceneState.started)    
            {
                Debug.Log("CHEESE PIZZA IS STILL NOT COOL");

                activateGraphicTransition = false;
                graphicShowTime = 0;
                hasPlayed = false;
                creditsRolling = false;
                return;
            }
        }
    }

    public void RollCredits()
    {
        credits.SetActive(true);
    }

    public void ResetCredits()
    {
        Debug.Log("CHEESE PIZZA IS CREDITS");
        credits.SetActive(false);

    }

    public void LoadEnding(string ending)
    {
        if (ending == "Beanman")
        {
            graphicTime = 12.5f;
            FMODVO = "event:/VO_BeanmanWin";
            FMODMusic = "event:/Good Ending";
            EndingScreen = BeanManWinEnding;
            EndingScreenText = BeanManWinEndingText;
        }
        if (ending == "Beanman Leaves Bag Town") {
            graphicTime = 10f;
            FMODVO = "event:/VO_BeanLeavesTownUncool";
            FMODMusic = "event:/Good Ending";
            EndingScreen = BeanManLeavesTown;
            EndingScreenText = BeanManLeavesBaggedText;
        }
        if (ending == "Beanman Leaves Cool Town") {
            graphicTime = 14f;
            FMODVO = "VO_BeanmanLeaveCool";
            FMODMusic = "event:/Good Ending";
            EndingScreenText = BeanManLeavesCoolText;
            EndingScreen = BeanManLeavesTown;
        }
        if (ending == "Beanman Leaves Uncool Town"){
            graphicTime = 10f;
            FMODVO = "event:/VO_BeanLeavesTownUncool";
            FMODMusic = "event:/Good Ending";
            EndingScreenText = BeanManLeavesUncoolText;
            EndingScreen = BeanManLeavesTown;
        }
        if (ending == "Lina Bean")
        {
            graphicTime = 19f;
            FMODVO = "event:/VO_LinaBean";
            FMODMusic = "event:/Less Bad Ending";
            EndingScreenText = LinaBeanEndingText;
            EndingScreen = LinaBeanEnding;
        }
        if (ending == "Chickpea Deputy")
        {
            graphicTime = 19f;
            FMODVO = "event:/VO_Chickpea";
            FMODMusic = "event:/Bad Ending";
            EndingScreenText = ChickPeaEndingText;
            EndingScreen = ChickPeaEnding;
        }
        if (ending == "Granny Smith")
        {
            graphicTime = 18f;
            FMODVO = "event:/VO_GrannySmith";
            FMODMusic = "event:/Bad Ending";
            EndingScreenText = GrannySmithEndingText;
            EndingScreen = GrannySmithEnding;
        }
        if (ending == "Peanut Twins")
        {
            graphicTime = 18f;
            FMODVO = "event:/VO_PeanutTwin";
            FMODMusic = "event:/Bad Ending";
            EndingScreenText = PeanutTwinEndingText;
            EndingScreen = PeanutTwinEnding;
        }
        if (ending == "Slim Sausage")
        {
            graphicTime = 18f;
            FMODVO = "event:/VO_SlimSausage";
            FMODMusic = "event:/Good Ending";
            EndingScreenText = SlimSausageWinningText;
            EndingScreen = SlimSausageWinning;
        }
        if (ending == "Birthday Cake")
        {
            graphicTime = 16f;
            FMODVO = "event:/VO_BirthdayCake";
            FMODMusic = "event:/Less Bad Ending";
            EndingScreenText = BirthdayCakeEndingText;
            EndingScreen = BirthdayCakeEnding;
        }
        if (ending == "GreenBen")
        {
            graphicTime = 15f;
            FMODVO = "event:/VO_GreenBen";
            FMODMusic = "event:/Good Ending";
            EndingScreenText = GreenBenEndingText;
            EndingScreen = GreenBenEnding;
        }
        if (ending == "Fire Hydrant")
        {
            graphicTime = 13f;
            FMODVO = "event:/VO_FireHydrant";
            FMODMusic = "event:/Less Bad Ending";
            EndingScreenText = FireHydrantEndingText;
            EndingScreen = FireHydrantEnding;
        }

        if (!_endingsManager.endingsSeenList.Contains(EndingScreen))
        {
            Debug.Log("New Ending");
            _endingsManager.addEnding(EndingScreen);
        }

        activateGraphicTransition = true;

        LoadGraphic(EndingScreen, FMODMusic, FMODVO, graphicTime);

    }
}
