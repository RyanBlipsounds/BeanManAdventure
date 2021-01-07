using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    // The game's UI
    [SerializeField]
    private UILogic _UILogic = default;

    [SerializeField]
    private PlayerController _playerController;

    public EndingsManager endingsManager;

    public NPC _npc = default;
    public ActManager _actManager = default;

    public ButterLogic butter;

    public PeanutTwinsLogic peanutTwins;

    public SaveLoading saveLoading;

    public TrafficConeCollection _trafficConeCollection;

    public List<HouseStateLogic> listHouses = new List<HouseStateLogic>();

    public List<GameObject> listTotalNPC = new List<GameObject>();

    public GameObject peanutButter;
    public GameObject trafficCone;

    public HideyHole _hideyHole;

    public ChickPeaLogic chickPeaLogic;

    public LemonadeStand _lemonadeStand;
    public FireHydrantVomit _fireHydrantvomit;
    public UILogic uILogic;

    public QuestList questList;

    public GameState m_gameState = null;
    public enum gameState
    {
        ISCOOL,
        ISNOTCOOL,
        ISBAGGED,
        BEANGOHINT,
        MYGLASSES,
        ENDING
    }

    public gameState beanState = gameState.ISCOOL;
    private string characterConversation = default;
    public Dictionary<string, string> conversationDict = new Dictionary<string, string>();
    private bool talkedGranny = false;
    private bool talkedLina = false;
    private bool talkedChickpea = false;

    private void Awake()
    {
        saveLoading.Load();
    }
    // Start is called before the first frame update
    void Start()
    {
        listTotalNPC.AddRange(GameObject.FindGameObjectsWithTag("NPC"));
        conversationDict.Add("ISCOOL", "ISCOOL");
        conversationDict.Add("ISNOTCOOL", "ISNOTCOOL");
        conversationDict.Add("ISBAGGED", "ISBAGGED");
        conversationDict.Add("BEANGOHINT", "BEANGOHINT");
        ResetGame();
        if (beanState == gameState.BEANGOHINT) {
            IsBeanGoHint();
        }
    }

    public void Conversation(string gameObjectName, int count) {

        // Conversation loading for Exit town
        if (gameObjectName == "ExitTown") {
            conversationDict["ISCOOL"] = "Do you Want to Leave Town?";
            conversationDict["BEANGOHINT"] = "Do you Want to Leave Town?";
            conversationDict["ISNOTCOOL"] = "Do you Want to Leave Town?";
            conversationDict["ISBAGGED"] = "Do you Want to Leave Town?";
        }

        // Conversation loading for fire Hydrant
        // ISCOOL is dynamic. Fire Hydrant needs to be spoken to multiple times to be active.
        if (gameObjectName == "Fire Hydrant")
        {
            Debug.Log(count);
            if (count < 5)
            {
                string dialogue = "..";
                dialogue = "..";
                int countStart = 0;
                countStart = 0;
                while (countStart < count)
                {
                    dialogue += ".";
                    countStart++;
                }
                conversationDict["ISCOOL"] = dialogue;
                conversationDict["BEANGOHINT"] = dialogue;
                conversationDict["ISNOTCOOL"] = "...";
                conversationDict["ISBAGGED"] = "Oh thank god. You look so much better";
                return;
            }
            else {
                conversationDict["ISCOOL"] = "Why are you talking to me? I'm a fire hydrant.";
                conversationDict["BEANGOHINT"] = "Why are you talking to me? I'm a fire hydrant.";
                conversationDict["ISNOTCOOL"] = "Bean Man! Looks like you lost your glasses some how! Here, take this";
                conversationDict["ISBAGGED"] = "Oh thank god. You look so much better";
            }


        }

        // Conversation laoding for Granny Smith
        if (gameObjectName == "Granny Smith") {

            conversationDict["ISCOOL"] = "My main man Bean Man! Beango isn't ready yet, go mingle with other peeps";
            conversationDict["BEANGOHINT"] = "Are you ready for Beango?";
            conversationDict["ISNOTCOOL"] = "Bean Man! Looks like you lost your glasses some how! Here, take this";
            conversationDict["ISBAGGED"] = "Long time no See!";
            return;
        }

        if (gameObjectName == "Traffic Cone")
        {
            conversationDict["ISCOOL"] = "Please don't tell Birthday cake that I'm here.";
            conversationDict["BEANGOHINT"] = "Please don't tell Birthday cake that I'm here.";
            conversationDict["ISNOTCOOL"] = "Please don't tell Birthday cake that I'm here.";
            conversationDict["ISBAGGED"] = "Please don't tell Birthday cake that I'm here.";
            return;
        }

        if (gameObjectName == "Butter")
        {
            conversationDict["ISCOOL"] = "Those Peanut Twins are some good pals!";

            if (endingsManager.endingsSeenList.Count > 0)
            {
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BeanManUncoolEnding ||
                    endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BeanManLeavesTown ||
                    endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BeanManLeavesBagged)
                {
                    conversationDict["ISCOOL"] = "I've never left town before. What's it like out there?";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.LinaBeanEnding)
                {
                    conversationDict["ISCOOL"] = "";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BeanManWinEnding)
                {
                    conversationDict["ISCOOL"] = "";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.ChickPeaEnding)
                {
                    conversationDict["ISCOOL"] = ".";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.FireHydrantEnding)
                {
                    conversationDict["ISCOOL"] = ".";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.GreenBenEnding)
                {
                    conversationDict["ISCOOL"] = ".";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BirthdayCakeEnding)
                {
                    conversationDict["ISCOOL"] = ".";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.GrannySmithEnding)
                {
                    conversationDict["ISCOOL"] = ".";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.SlimSausageWinning)
                {
                    conversationDict["ISCOOL"] = ".";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.PeanutTwinEnding)
                {
                    conversationDict["ISCOOL"] = "Why am I still here?";
                }
            }
            conversationDict["BEANGOHINT"] = "I wish I was old enough to go to Beango!!"; // This state should push into Beango
            conversationDict["ISNOTCOOL"] = "Yeah, I need to go";
            conversationDict["ISBAGGED"] = "Okay I can deal with this.";
            return;
        }

        // Conversation laoding for Chickpea Deputy
        if (gameObjectName == "Chickpea Deputy")
        {
            conversationDict["ISCOOL"] = "J-walked recently? haha, just kidding...";

            if (endingsManager.endingsSeenList.Count > 0) {
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BeanManUncoolEnding || 
                    endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BeanManLeavesTown || 
                    endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BeanManLeavesBagged)
                {
                    conversationDict["ISCOOL"] = "Oh. You're back.";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BeanManWinEnding) {
                    conversationDict["ISCOOL"] = "Aw gee, those glasses just look so good on you Bean";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.LinaBeanEnding)
                {
                    conversationDict["ISCOOL"] = "If I'm not careful, Lina Bean will be the new sheriff in town.";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.ChickPeaEnding)
                {
                    conversationDict["ISCOOL"] = "I had it. I had so much power. It was all mine... If only those glasses fit me properly.";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.FireHydrantEnding)
                {
                    conversationDict["ISCOOL"] = "That Fire Hydrant sure is smart.";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.GreenBenEnding)
                {
                    conversationDict["ISCOOL"] = "Green Ben thinks he's all that, but I think he's cold beans.";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BirthdayCakeEnding)
                {
                    conversationDict["ISCOOL"] = "Hello Beanman. You should join Birthday Cakes super awesome club.";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.GrannySmithEnding)
                {
                    conversationDict["ISCOOL"] = "Granny still didn't invite me to Beango.";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.SlimSausageWinning)
                {
                    conversationDict["ISCOOL"] = "I'm working on my own rap album after I saw slim get all of his success.";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.PeanutTwinEnding)
                {
                    conversationDict["ISCOOL"] = "Boy that peanut butter sure is tasty!";
                }
            }
            
            conversationDict["ISNOTCOOL"] = "Watch yourself.";
            conversationDict["ISBAGGED"] = "What do you want?";
            conversationDict["BEANGOHINT"] = "Oh boy Beango? Gosh, I wish I was cool enough to join in!";

            return;
        }

        if (gameObjectName == "Lemonade Stand")
        {
            conversationDict["ISCOOL"] = "When life throws lemons at me, I make lemonade!";

            if (endingsManager.endingsSeenList.Count > 0)
            {
                conversationDict["ISCOOL"] = "They won't stop throwing lemons at me.";
            }
            if (endingsManager.endingsSeenList.Count > 1)
            {
                conversationDict["ISCOOL"] = "I never really wanted this life. I much prefer oranges to lemons";
            }
            if (endingsManager.endingsSeenList.Count > 2)
            {
                conversationDict["ISCOOL"] = "They're still throwing lemons. I just want to sell my orangeade in peace.";
            }
            if (endingsManager.endingsSeenList.Count > 3)
            {
                conversationDict["ISCOOL"] = "It turns out that no one likes the name 'Orangeade'. And life keeps throwing lemons.";
            }
            if (endingsManager.endingsSeenList.Count > 4)
            {
                conversationDict["ISCOOL"] = "Welp. I'm shutting down for business.";
            }
            if (endingsManager.endingsSeenList.Count > 5)
            {
                conversationDict["ISCOOL"] = "I really should've appreciated the lemons that life had been throwing at me this whole time.";
            }
            if (endingsManager.endingsSeenList.Count > 6)
            {
                conversationDict["ISCOOL"] = "I've decided to open and stick to selling lemonade.";
            }
            if (endingsManager.endingsSeenList.Count > 7)
            {
                conversationDict["ISCOOL"] = "Buy some lemonade?";
            }

            conversationDict["ISNOTCOOL"] = "Well at least I'm not you.";
            conversationDict["ISBAGGED"] = "I was happier when you weren't like this.?";
            conversationDict["BEANGOHINT"] = "You should probably get going to Beango";

            return;
        }

        // Conversation loading for Lina Bean
        if (gameObjectName == "Lina Bean")
        {
            conversationDict["ISCOOL"] = "Bean Man! Are we still on for the movies this weekend?!";

            if (endingsManager.endingsSeenList.Count > 0)
            {
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BeanManUncoolEnding ||
                    endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BeanManLeavesTown ||
                    endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BeanManLeavesBagged)
                {
                    conversationDict["ISCOOL"] = "Bean man! You're back! I thought you bailed on me!";
                }

                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.LinaBeanEnding)
                {
                    conversationDict["ISCOOL"] = "Sorry I got a little crazy there... I hope we can still go out to the movies this weekend.";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.ChickPeaEnding)
                {
                    conversationDict["ISCOOL"] = "Chickpea caught my attention for a little bit there...";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.FireHydrantEnding)
                {
                    conversationDict["ISCOOL"] = "Oh hey Bean... Me and Fire Hydrant are going to the movies this weekend instead... Sorry";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.GreenBenEnding)
                {
                    conversationDict["ISCOOL"] = "Hey Bean! I invited GreenBen to the movies this weekend. He's a real sweet bean!";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BirthdayCakeEnding)
                {
                    conversationDict["ISCOOL"] = "Hello Beanman. You should join Birthday Cakes super awesome club.";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.GrannySmithEnding)
                {
                    conversationDict["ISCOOL"] = "Beango was so much better last week! Granny had a whole different vibe!";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.SlimSausageWinning)
                {
                    conversationDict["ISCOOL"] = "Slims new album is fire... it's so great to see him back in town!";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.PeanutTwinEnding)
                {
                    conversationDict["ISCOOL"] = "I'm glad I didn't eat too much Peanut Butter";
                }
            }
            else {
                conversationDict["BEANGOHINT"] = "I wish we could hang out after Beango at Granny's";
            }
            
            conversationDict["ISNOTCOOL"] = "*Swipes Left*";
            conversationDict["ISBAGGED"] = "Working out WON'T get my attention. *Wink*";

            return;
        }

        // Conversation laoding for Birthday Cake
        if (gameObjectName == "Birthday Cake")
        {
            conversationDict["ISCOOL"] = "I'm not hoarding traffic cones! Also, would you be interested in joining my cul- I mean club?";

            if (endingsManager.endingsSeenList.Count > 0)
            {
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BeanManUncoolEnding ||
                endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BeanManLeavesTown ||
                endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BeanManLeavesBagged)
                {
                    conversationDict["ISCOOL"] = "You're lucky you aren't a traffic cone... you never would have esacped if you were.";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.LinaBeanEnding)
                {
                    conversationDict["ISCOOL"] = "I secretly rode on Lina's back all the way to Tokyo! ...No one joined my club.";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.ChickPeaEnding)
                {
                    conversationDict["ISCOOL"] = "Chickpea almost passed a law making my club illegal... Where would all of my members go?";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.FireHydrantEnding)
                {
                    conversationDict["ISCOOL"] = "I've decided to start collecting Fire Hydrants instead!";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.GreenBenEnding)
                {
                    conversationDict["ISCOOL"] = "GreenBen is such a swell guy. He joined my club!";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BirthdayCakeEnding)
                {
                    conversationDict["ISCOOL"] = "Hey Beanman!";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.GrannySmithEnding)
                {
                    conversationDict["ISCOOL"] = "I'll be allowed to join Beango next time! I swear it!";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.SlimSausageWinning)
                {
                    conversationDict["ISCOOL"] = "Do you think slim would sign my traffic cones?";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.PeanutTwinEnding)
                {
                    conversationDict["ISCOOL"] = "I don't get it. How did peanut butter become more popular than my cul- I mean club";
                }
            }
            //NEEDS TO BE LESS OBVIOUS THAT HE IS STARTING A CULT
            conversationDict["ISNOTCOOL"] = "Oh, yeah we don't really need you to join us.";
            conversationDict["ISBAGGED"] = "Hello sir! Are you interested in learning about a fun new religion?";
            conversationDict["BEANGOHINT"] = "Maybe if I go to beango I can get new members...";

            return;
        }

        // Conversation laoding for Peanut Twins
        if (gameObjectName == "Peanut Twins")
        {
            conversationDict["ISCOOL"] = "Bean Man! We want to be just like you when we're older!";
            if (endingsManager.endingsSeenList.Count > 0)
            {
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BeanManUncoolEnding ||
                endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BeanManLeavesTown ||
                endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BeanManLeavesBagged)
                {
                    conversationDict["ISCOOL"] = "We had no idea what to do with ourselves after you left town Bean Man!";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.LinaBeanEnding)
                {
                    conversationDict["ISCOOL"] = "Does this mean Lina Bean is our new mommy?";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.ChickPeaEnding)
                {
                    conversationDict["ISCOOL"] = "Does this mean Chickpea is our new daddy?";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.FireHydrantEnding)
                {
                    conversationDict["ISCOOL"] = "We want to be like Fire Hydrant when we're older!";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.GreenBenEnding)
                {
                    conversationDict["ISCOOL"] = "Green Ben gave us a picky back ride today!";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BirthdayCakeEnding)
                {
                    conversationDict["ISCOOL"] = "Hello Beanman. You should join Birthday Cakes super awesome club.";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.GrannySmithEnding)
                {
                    conversationDict["ISCOOL"] = "Granny let us into Beango! We're cool now!";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.SlimSausageWinning)
                {
                    conversationDict["ISCOOL"] = "I don't really understand Slims music, but it sure seems popular!";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.PeanutTwinEnding)
                {
                    conversationDict["ISCOOL"] = "Oh hey Bean... Oh? My brother? Yeah not sure where he went.";
                }
            }
            conversationDict["ISNOTCOOL"] = "Oh hey Bean Boy, I'm pretty busy. Maybe go talk with that stick?";
            conversationDict["ISBAGGED"] = "What is it son, I'm very busy.";
            conversationDict["BEANGOHINT"] = "When we're older, we want to go to Beango with you!";

            return;
        }

        // Conversation laoding for Greenben
        if (gameObjectName == "GreenBen")
        {
            conversationDict["ISCOOL"] = "Is that you Bean Man?. I really hope I can feel better soon.";
            if (endingsManager.endingsSeenList.Count > 0)
            {
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BeanManUncoolEnding ||
                endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BeanManLeavesTown ||
                endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BeanManLeavesBagged)
                {
                    conversationDict["ISCOOL"] = "Hey Bean. Glad to see you're back.";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.LinaBeanEnding)
                {
                    conversationDict["ISCOOL"] = "I won't lie Bean. I'm a little upset there was someone taller than me.";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.ChickPeaEnding)
                {
                    conversationDict["ISCOOL"] = "The whole town was gone... thank goodness we could rebuild it all.";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.FireHydrantEnding)
                {
                    conversationDict["ISCOOL"] = "I liek Fire Hydrants";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.GreenBenEnding)
                {
                    conversationDict["ISCOOL"] = "You'll never be as cool as me Bean.";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BirthdayCakeEnding)
                {
                    conversationDict["ISCOOL"] = "Hello Beanman. You should join Birthday Cakes super awesome club.";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.GrannySmithEnding)
                {
                    conversationDict["ISCOOL"] = "I would've gone into Beango. But I hit my head on the door and got sick again.";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.SlimSausageWinning)
                {
                    conversationDict["ISCOOL"] = "I think Slim and I are going to do a collab!";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.PeanutTwinEnding)
                {
                    conversationDict["ISCOOL"] = "God... I can't get enough of this peanut butter.";
                }
            }
            conversationDict["ISNOTCOOL"] = "Ouch, what happened to you?";
            conversationDict["ISBAGGED"] = "Oh you look much better now!";
            conversationDict["BEANGOHINT"] = "Beango sounds great but I think I need to get some rest.";

            return;
        }

        // Conversation laoding for Slim Sausage
        if (gameObjectName == "Slim Sausage")
        {
            conversationDict["ISCOOL"] = "Yo it's the mean Bean! Not even slim sausage is as slick as you!";
            if (endingsManager.endingsSeenList.Count > 0)
            {
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BeanManUncoolEnding ||
                endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BeanManLeavesTown ||
                endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BeanManLeavesBagged)
                {
                    conversationDict["ISCOOL"] = "BEAN! We missed you my man!";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.LinaBeanEnding)
                {
                    conversationDict["ISCOOL"] = "Lina Bean was looking fiiiine crossing that Pacific Ocean";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.ChickPeaEnding)
                {
                    conversationDict["ISCOOL"] = "I'm just surprised as you are about Chickpea";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.FireHydrantEnding)
                {
                    conversationDict["ISCOOL"] = "That fire hydrant is an inspiration. My next album will be about him";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.GreenBenEnding)
                {
                    conversationDict["ISCOOL"] = "Man... Green Ben is high demand right now. I really want him for my next album";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BirthdayCakeEnding)
                {
                    conversationDict["ISCOOL"] = "Hello Beanman. You should join Birthday Cakes super awesome club.";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.GrannySmithEnding)
                {
                    conversationDict["ISCOOL"] = "Granny had it going DOWN at Beango. It was the best one yet!!";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.SlimSausageWinning)
                {
                    conversationDict["ISCOOL"] = "Hey Bean! I'm back in town from my tour! And hey, nice new glasses";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.PeanutTwinEnding)
                {
                    conversationDict["ISCOOL"] = "If you ask me, peanuts and sausage sound way better than peanuts and butter";
                }
            }
            conversationDict["ISNOTCOOL"] = "Did I say mean Bean before? What I meant to say was gross.";
            conversationDict["ISBAGGED"] = "I'm feeling REAL good lately.";
            conversationDict["BEANGOHINT"] = "I'm rooting for you at Beango tonight my main man Bean Man!";

            return;
        }
    }

    /// <summary>
    /// Randomized Glasses does two things:
    /// 1: Assigns winning NPC and assign proper glasses to them
    /// 2: Assigns random glasses to the rest (RED HERRINGS!)
    /// </summary>
    public void RandomizeGlasses()
    {
        var copyNPC = new List<NPC>(_playerController.scriptNPCList);
        bool newWinnerFound;
        newWinnerFound = false;
        NPC winningNPC = copyNPC[Random.Range(0, copyNPC.Count)];
        while (newWinnerFound == false) {
            int count;
            count = 0;
            if (endingsManager.endingsSeenList.Count > 0) {
                foreach (GameObject endingsFound in endingsManager.endingsSeenList)
                {
                    Debug.Log("Endings count!");
                    if (!endingsFound.gameObject.name.Contains("Beanman"))
                    {
                        count++;
                    }
                }
            }
            Debug.Log(count + " " + endingsManager.endingsSeenList.Count);

            Debug.Log("NPC LIST COUNT " + _playerController.scriptNPCList.Count);

            if (count == _playerController.scriptNPCList.Count)
            {
                newWinnerFound = true;
                count = 0;
                Debug.Log("All Character Endings have been found. We're choosing a random character as the winner");
                break;
            }
            winningNPC = copyNPC[Random.Range(0, copyNPC.Count)];

            if (endingsManager.endingsSeenList.Count == 0) {
                Debug.Log("This is the FIRST winner " + winningNPC);
                newWinnerFound = true;
                break;
            }
            bool hasEnding = false;
            hasEnding = false;

            Debug.Log("This is the current trying winning NPC " + winningNPC.gameObject.name);
            foreach (GameObject ending in endingsManager.endingsSeenList) {
                if (ending.gameObject.name.Contains(winningNPC.gameObject.name)) {
                    Debug.Log("There was a match in endings for " + winningNPC.gameObject.name);
                    hasEnding = true;
                    break;
                }
            }
            if (hasEnding == false) {
                Debug.Log("This is the winner " + winningNPC.gameObject.name);
                newWinnerFound = true;
                break;
            }
        }
        winningNPC.spriteRenderer.sprite = winningNPC.glassesList[0];
        winningNPC.isWinner = true;
        copyNPC.Remove(winningNPC);

        if (winningNPC.gameObject.name == "Peanut Twins") {
            butter.KillButter();
        }

        foreach (NPC character in copyNPC)
        {
            character.spriteRenderer.sprite = character.glassesList[Random.Range(1, character.glassesList.Count)];
        }
    }

    public void IsBeanGoHint() {
        foreach (HouseStateLogic house in listHouses)
        {
            house.BeangoState();
        }

        questList.CompleteQuestItem("Everyone in Town");
        questList.ActivateQuestItem("Beango");
        _actManager.EndingScreenText = _actManager.BeangoScreenText;

        beanState = gameState.BEANGOHINT;
       
    }

    public void IsNotCool() {
        questList.CompleteQuestItem("Beango");
        questList.ActivateQuestItem("TownsPeople Again");
        foreach (HouseStateLogic house in listHouses)
        {
            house.NotCoolState();
        }
        beanState = gameState.ISNOTCOOL;
        RandomizeGlasses();

        Debug.Log("IS NOT COOL");

        _playerController.MoveToStart();

        foreach (NPC character in _playerController.scriptNPCList)
        {
            character.ShowGlasses();
        }
        _playerController.NoGlasses();
        //BEAN MAN SPRITE CHANGE
    }

    public void IsBagged() {
        foreach (HouseStateLogic house in listHouses) {
            house.BaggedState();
        }
        beanState = gameState.ISBAGGED;
        questList.CompleteQuestItem("TownsPeople Again");
        questList.ActivateQuestItem("Get Your Cool Back");
    }

    public void Ending() {
        talkedGranny = false;
        beanState = gameState.ENDING;
        ResetGame();
    }

    /// <summary>
    /// Resets the game to start playing again
    /// </summary>
    public void ResetGame()
    {
        _playerController._responseBox.isActive = false;
        _playerController._dialogueBox.isActive = false;
        _playerController._canTalkBox.isActive = false;
        _playerController.ResetBeanSpawnPosition();
        uILogic.UpdateEndingsCount(endingsManager.endingsSeenList.Count);
        foreach (HouseStateLogic house in listHouses)
        {
            house.CoolState();
        }
        if (_playerController.scriptNPCList.Count != 0) {
            foreach (NPC npc in _playerController.scriptNPCList)
            {
                npc.ResetCoolStateNPC();
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BirthdayCakeEnding)
                {
                    trafficCone.gameObject.tag = "SideNPC";
                    npc.ShowTrafficCone();
                }
            }
        }

        _playerController.Glasses();
        //_playerController.fireHydrantTalkCount = 0;
        _playerController.Bag.transform.position = _playerController.m_BagStartPosition.transform.position;
        _playerController.finishedBagMove = false;
        _playerController.bagMoving = true;
        //_playerController.scriptNPCList.Clear();
        _playerController.MoveToStart();
        _UILogic.MainMenu.SetActive(true);
        _hideyHole.NewPeeperSet();
        _lemonadeStand.SetLemonadeStandSanity();
        _fireHydrantvomit.hasVommittedThisRound = false;
        if (_playerController.scriptNPCList.Count != 0)
        {
            if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.FireHydrantEnding)
            {
                _trafficConeCollection.FireHydrantPile();
            }
            else
            {
                _trafficConeCollection.TrafficConePile();
            }

            if (endingsManager.endingsSeenList.Contains(_actManager.PeanutTwinEnding))
            {
                peanutButter.SetActive(true);
                peanutTwins.KillPeanutTwin();
                butter.KillButter();
            }
            else
            {
                peanutButter.SetActive(false);
                peanutTwins.KillPeanutTwin();
                peanutTwins.PeanutTwinLives();
                butter.ButterLives();
            }
        }

        chickPeaLogic.ChickPeaWizardMode();

        saveLoading.Save();
    }

}