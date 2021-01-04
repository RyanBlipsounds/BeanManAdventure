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

    public string EndingScreenText;
    public string BeangoScreenText = "It was a sweaty night of Beango, but Granny and Bean Man cleaned up the house. They were an unstoppable force. It was all hazy, but it was one of the greatest nights of Bean Man and Granny's lives. Although something didn't feel quite right when Bean Man awoke in the morning. Bean had lost his glasses.";
    public string ChickPeaEndingText = "In the quest to get his cool back, Bean Man ran the risk of selecting what he thought were his own glasses, but failed to recognize that Chickpea Deputy had been wearing them all along. Due to this, the glasses gave our Deputy enough confidence to unlock his wizard powers, thus ending the world, and preventing Bean Man from getting his cool back.";
    public string BeanManWinEndingText = "There wasn't a snowballs chance in Beans hell that Bean Man was going to lose his cool today. In this journey, Bean Man thought that had lost his cool when he lost his glasses, but really the cool was inside of him all along. The whole town rejoiced as Bean Man has found his cool for good.";
    public string BeanManLeavesCoolText = "BeanMan knew his cool was so high above Beantown's coolest bean. Even Lina Bean couldn't handle the sheer coolness of his cool. Could there be a cool person more cool than Beanman? He doesn't say so, but he must know so.";
    public string LinaBeanEndingText = "Once Bean Man realized that Lina had his glasses, it was too late. Lina had grown 300 meters and made her way across the Pacific Ocean to Tokyo Japan. Tokyo faced one of it's greatest disasters in Japan's history as Lina tore down the city. The recovery still remains to be seen. Also, Lina reveals that she was edamame the whole time.";
    public string PeanutTwinEndingText = "With great glasses comes great responsibility. After a tiring life of being referred to as the second twin, Peanut Twin number two took it upon himself to invent peanut butter with just a simple sacrifice from Peanut Twin number one and his good friend Butter. Peanut Twin number two made millions off of the idea, lost two of his best friends, and was still just Peanut Twin number two.";
    public string GrannySmithEndingText = "As it turns out, Granny was always jealous of Bean Man. Being only the second coolest in town carries a heavy burden that Granny just couldn't bare. Bean Man could do nothing but watch as his closest homie take the throne and make the town hers. Stay tuned to find out what happens in Bean Man sequel. Coming in 2077";
    public string FireHydrantEndingText = ".....Seriously? You let a Fire Hydrant beat you? A fire hydrant is cooler than you. Just let that sink in.";
    public string SlimSausageWinningText = "Considering that Slim Sausage was a masterclass rapper, there was no chance that Bean Man could keep up with his cool once Sausage had his glasses. Slim Sausage went on to have an illustrious career where he would tour with Frank Sinatra to perform in front of millions, as well as performing for popular figures like Oprah Winbean and Pope Beanedict XVII.";
    public string BeanManLeavesBaggedText = "There was no point. Bean Man just didn't belong. Bean just felt that his life filled with cool had come to an end. He decided to move on to his next journey.";
    public string BeanManLeavesUncoolText = "There was no point. Bean Man just didn't belong. Bean just felt that his life filled with cool had come to an end. He decided to move on to his next journey.";
    public string GreenBenEndingText = "With his cool shiny new glasses, Green Ben was looking and feeling better than ever. But because Green Ben is so selfless, he decided to give Bean Man his glasses back. And even though Bean Man had his glasses back, Green Ben was now the new king in town.";
    public string BirthdayCakeEndingText = "Once the glasses belonged to Birthday Cake, they had the confidence to finally take the full measure they had been wanting to commit to this whole time... to become a delicious treat for their friends. After their friends enjoyed a delicious birthday treat, Birthday Cakes spirit was no longer trapped on earth, and could now live a long and prosperous afterlife.";

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
        if (activateGraphicTransition == true)
        {
            if (m_gameState.beanState == GameState.gameState.BEANGOHINT || m_gameState.beanState == GameState.gameState.ISNOTCOOL) {
                EndingScreen = BeangoScreen;
                EndingScreenText = BeangoScreenText;
            }
            if (m_gameState.beanState == GameState.gameState.ISCOOL)
            {
                //LoadGraphic(EndingScreen);
            }
            if (m_gameState.beanState == GameState.gameState.ISBAGGED || m_gameState.beanState == GameState.gameState.ENDING)
            {
                //LoadGraphic(EndingScreen);
            }
            Debug.Log(BeanManLeavesCoolText);
            LoadGraphic(EndingScreen);
        }
    }

    private void FadeToBlack(sceneState state, GameObject graphic, GameObject oldGraphic)
    {
        float blackScreenTmp;

        blackScreenTmp = Mathf.Lerp(0, 1, blackScreenTimeToFade / 2);

        BlackScreen.color = new Color(0, 0, 0, blackScreenTmp);

        if (!switchFade)
        {
            blackScreenTimeToFade += Time.deltaTime;
        }
        else {
            if (m_gameState.beanState == GameState.gameState.BEANGOHINT && hasPlayed == false)
            {
                m_gameState.IsNotCool();
                hasPlayed = true;
            }
            if (m_gameState.beanState == GameState.gameState.ISBAGGED && hasPlayed == false)
            {
                m_gameState.Ending();
                hasPlayed = true;
            }
            if (graphic == BeanManLeavesTown) {
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
                if (m_gameState.beanState == GameState.gameState.ENDING) {
                    //m_gameState.Ending();
                    m_gameState.beanState = GameState.gameState.ISCOOL;
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

    public void LoadGraphic(GameObject graphic)
    {

        activateGraphicTransition = true;
        EndingScreen = graphic;

        //Debug.Log("Graphic " + graphicShowTime);
        if (sceneTransitionState == sceneState.started)
        {
            FadeToBlack(sceneState.graphic, graphic, null);
        }
        if (sceneTransitionState == sceneState.graphic)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                sceneTransitionState = sceneState.backtoscene;
            }
            graphicShowTime += Time.deltaTime;
            if (graphicShowTime >= 15) {
                sceneTransitionState = sceneState.backtoscene;
            }
        }
        if (sceneTransitionState == sceneState.backtoscene)
        {
            FadeToBlack(sceneState.started, null, graphic);
            if (sceneTransitionState == sceneState.started)
            {
                activateGraphicTransition = false;
                graphicShowTime = 0;
                hasPlayed = false;
                return;
            }
        }
    }

    public void LoadEnding(string ending)
    {
        Debug.Log(ending);

        if (ending == "Beanman")
        {
            EndingScreen = BeanManWinEnding;
            EndingScreenText = BeanManWinEndingText;
        }
        if (ending == "Beanman Leaves Bag Town") {
            EndingScreen = BeanManLeavesTown;
            EndingScreenText = BeanManLeavesBaggedText;
        }
        if (ending == "Beanman Leaves Cool Town") {
            EndingScreenText = BeanManLeavesCoolText;
            EndingScreen = BeanManLeavesTown;
        }
        if (ending == "Beanman Leaves Uncool Town"){
            EndingScreenText = BeanManLeavesUncoolText;
            EndingScreen = BeanManLeavesTown;
        }
        if (ending == "Lina Bean") 
        {
            EndingScreenText = LinaBeanEndingText;
            EndingScreen = LinaBeanEnding;
        }
        if (ending == "Chickpea Deputy")
        {
            EndingScreenText = ChickPeaEndingText;
            EndingScreen = ChickPeaEnding;
        }
        if (ending == "Granny Smith")
        {
            EndingScreenText = GrannySmithEndingText;
            EndingScreen = GrannySmithEnding;
        }
        if (ending == "Peanut Twins")
        {
            EndingScreenText = PeanutTwinEndingText;
            EndingScreen = PeanutTwinEnding;
        }
        if (ending == "Slim Sausage")
        {
            EndingScreenText = SlimSausageWinningText;
            EndingScreen = SlimSausageWinning;
        }
        if (ending == "Birthday Cake")
        {
            EndingScreenText = BirthdayCakeEndingText;
            EndingScreen = BirthdayCakeEnding;
        }
        if (ending == "GreenBen")
        {
            EndingScreenText = GreenBenEndingText;
            EndingScreen = GreenBenEnding;
        }
        if (ending == "Fire Hydrant")
        {
            EndingScreenText = FireHydrantEndingText;
            EndingScreen = FireHydrantEnding;
        }

        if (!_endingsManager.endingsSeenList.Contains(EndingScreen))
        {
            Debug.Log("New Ending");
            _endingsManager.addEnding(EndingScreen);
        }

        activateGraphicTransition = true;

        LoadGraphic(EndingScreen);

    }

}
