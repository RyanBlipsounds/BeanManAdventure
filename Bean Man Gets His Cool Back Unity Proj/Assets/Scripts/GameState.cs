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

    public GreenBenLogic greenBen;

    public GameObject LinaBean;
    public GameObject BirthdayCake;

    public ButterLogic butter;
    public HouseDestroy houseDestroy;

    public CornLadyLogic cornLady;

    public Depth2D censorBar;
    public CoryLogic coryLogic;

    public PeanutTwinsLogic peanutTwins;

    public SaveLoading saveLoading;

    public TrafficConeCollection _trafficConeCollection;

    public List<HouseStateLogic> listHouses = new List<HouseStateLogic>();

    public List<GameObject> listTotalNPC = new List<GameObject>();

    public List<NPC> everyNPCList = new List<NPC>();

    public List<NPC> transitionsList = new List<NPC>();
    public List<NPC> completedTransitionsList = new List<NPC>();

    public GameObject ChickpeaDebris;

    public GameObject peanutButter;
    public GameObject trafficCone;

    public HideyHole _hideyHole;

    public ChickPeaLogic chickPeaLogic;

    public BlockCheeseLogic blockCheeseLogic;

    public PoparazziLogic poparazziCorn;

    public LemonadeStand _lemonadeStand;
    public FireHydrantVomit _fireHydrantvomit;
    public UILogic uILogic;

    public StickLogic stick;
    public QuestList questList;

    public GameState m_gameState = null;
    public enum gameState
    {
        ISCOOL,
        ISNOTCOOL,
        ISBAGGED,
        BEANGOHINT,
        WRONGBAGGED,
        ENDING
    }

    public List<NPC> trafficConeNPCs = new List<NPC>();

    public gameState beanState = gameState.ISCOOL;
    private string characterConversation = default;
    public Dictionary<string, string> conversationDict = new Dictionary<string, string>();
    private bool talkedGranny = false;
    private bool talkedLina = false;
    private bool talkedChickpea = false;
    public FMOD.Studio.EventInstance StartMusic;
    public bool recentEndingPlayed = false;

    public DynamicMusic dynamicMusic;

    public GameObject winningNPCGameObject;
    public GameObject wrongNPCGameObject;

    public GameObject PaidActor;
    public LastEndingManager lastEndingConesMove;

    private void Awake()
    {
        saveLoading.Load();
    }
    // Start is called before the first frame update
    void Start()
    {
        CoolMusic();
        listTotalNPC.AddRange(GameObject.FindGameObjectsWithTag("NPC"));
        conversationDict.Add("ISCOOL", "ISCOOL");
        conversationDict.Add("ISNOTCOOL", "ISNOTCOOL");
        conversationDict.Add("ISBAGGED", "ISBAGGED");
        conversationDict.Add("BEANGOHINT", "BEANGOHINT");
        conversationDict.Add("WRONGBAGGED", "WRONGBAGGED");
        conversationDict.Add("WRONGGUESS", "WRONGGUESS");
        ResetGame();
        if (beanState == gameState.BEANGOHINT) {
            IsBeanGoHint();
        }
        

    }

    public void CoolMusic()
    {
        if (_playerController.scriptNPCList.Count > 1)
        {
            if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.LinaBeanEnding ||
                    endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.FireHydrantEnding)
            {
                StartMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Enter Beanman");
                StartMusic.start();
            }
            else if(endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.ChickPeaEnding || 
                endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BirthdayCakeEnding || 
                endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.GrannySmithEnding ||
                endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.PeanutTwinEnding)
            {
                StartMusic = FMODUnity.RuntimeManager.CreateInstance("event:/After Bad Ending");
                StartMusic.start();
            }
            else
            {
                StartMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Enter Beanman");
                StartMusic.start();
                return;
            }
        }
        else
        {
            StartMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Enter Beanman");
            StartMusic.start();
            return;
        }
    }
    
    private void Update()
    {
        int count = 0;

        if (endingsManager.endingsSeenList.Count > 0 && recentEndingPlayed == true && beanState == gameState.ISCOOL)
        {
            foreach (GameObject endingsFound in endingsManager.endingsSeenList)
            {
                if (!endingsFound.gameObject.name.Contains("Leaves"))
                {
                    count++;
                }
            }
            if (count > 0)
            {
                IsBeanGoHint();
            }
            CoolMusic();
            recentEndingPlayed = false;
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
            if (count < 3)
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
                conversationDict["WRONGGUESS"] = "Oof. No. Someone left these glasses on me earlier today.";
                conversationDict["WRONGBAGGED"] = "Oof. No. Someone left these glasses on me earlier today.";
                if (beanState == gameState.WRONGBAGGED || beanState == gameState.ISBAGGED)
                {
                    if (winningNPCGameObject.gameObject.name == gameObjectName)
                    {
                        conversationDict["WRONGBAGGED"] = "*vomit sounds*";
                    }
                    else if (wrongNPCGameObject.gameObject.name == gameObjectName)
                    {
                        conversationDict["WRONGBAGGED"] = "Oof. No. Someone left these glasses on me earlier today.";
                    }
                    else
                    {
                        conversationDict["WRONGBAGGED"] = "Bro, don't ask me. I'm just a Fire Hydrant.";
                    }
                }
                return;
            }
            else {
                conversationDict["ISCOOL"] = "Why are you talking to me? I'm a fire hydrant.";
                conversationDict["BEANGOHINT"] = "Why are you talking to me? I'm a fire hydrant.";
                conversationDict["ISNOTCOOL"] = "Bean Man! Looks like you lost your glasses some how! Here, take this";
                conversationDict["ISBAGGED"] = "Oh thank god. You look so much better";
                conversationDict["WRONGGUESS"] = "Oof. No. Someone left these glasses on me earlier today.";
            }
        }

        // Conversation laoding for Granny Smith
        if (gameObjectName == "Granny Smith") {

            conversationDict["ISCOOL"] = "My main man Bean Man! Beango isn't ready yet, go mingle with other peeps";
            conversationDict["BEANGOHINT"] = "Are you ready for Beango?";
            conversationDict["ISNOTCOOL"] = "Bean Man! Looks like you lost your glasses some how! Here, take this";
            conversationDict["ISBAGGED"] = "Beanman! Someone in town has your glasses! Try to talk to who you think took your glasses!";

            if (beanState == gameState.WRONGBAGGED || beanState == gameState.ISBAGGED)
            {
                if (winningNPCGameObject.gameObject.name == "Slim Sausage")
                {
                    conversationDict["WRONGBAGGED"] = "Hey Beanman... I think Slim Sausage might have your glasses.";
                }
                if (winningNPCGameObject.gameObject.name == "Fire Hydrant")
                {
                    conversationDict["WRONGBAGGED"] = "I don't know how, but I think Fire Hydrant took your glasses.";
                }
                if (winningNPCGameObject.gameObject.name == "Chickpea Deputy")
                {
                    conversationDict["WRONGBAGGED"] = "Beanman. There's a sort of evil aura coming from where Chickpea is... Did he take your glasses?";
                }
                if (winningNPCGameObject.gameObject.name == "Peanut Twins")
                {
                    conversationDict["WRONGBAGGED"] = "Hey pal, I think Peanut Twin took your glasses.";
                }
                if (winningNPCGameObject.gameObject.name == "Birthday Cake")
                {
                    conversationDict["WRONGBAGGED"] = "I'm sorry Bean... I drank the punch";
                }
                if (winningNPCGameObject.gameObject.name == "Lina Bean")
                {
                    conversationDict["WRONGBAGGED"] = "You won't like this Bean... But Lina is wearing a new pair of glasses.";
                }
                if (winningNPCGameObject.gameObject.name == "GreenBen")
                {
                    conversationDict["WRONGBAGGED"] = "GreenBen has some sick new shades!";
                }
            }
            return;
        }

        if (gameObjectName == "Traffic Cone")
        {
            conversationDict["ISCOOL"] = "Please don't tell Birthday cake that I'm here.";
            conversationDict["BEANGOHINT"] = "Please don't tell Birthday cake that I'm here.";
            conversationDict["ISNOTCOOL"] = "Please don't tell Birthday cake that I'm here.";
            conversationDict["ISBAGGED"] = "Please don't tell Birthday cake that I'm here.";
            conversationDict["WRONGBAGGED"] = "I can't believe you've done this.";
            return;
        }

        if (gameObjectName == "Paid Actor" || gameObjectName == "Dead Guy")
        {
            if (endingsManager.endingsSeenList.Count == 3)
            {
                conversationDict["ISNOTCOOL"] = "Don't worry I'm fine. Ninja Officer Bush actually paid me to be here.";
            }
            if (endingsManager.endingsSeenList.Count == 4)
            {
                conversationDict["ISNOTCOOL"] = "I think that Baked Bush just needs this as a distraction. He's going through some stuff.";
            }
            if (endingsManager.endingsSeenList.Count == 5)
            {
                conversationDict["ISNOTCOOL"] = "He's paying me in Bean buckz.";
            }
            if (endingsManager.endingsSeenList.Count == 6)
            {
                conversationDict["ISNOTCOOL"] = "I'm going to put all of the Bean buckz I make into BeanCoin";
            }
            if (endingsManager.endingsSeenList.Count == 7)
            {
                conversationDict["ISNOTCOOL"] = "Ninja Officer Bush is good for the money right? He said he'd pay me after.";
            }
            if (endingsManager.endingsSeenList.Count == 7)
            {
                conversationDict["ISNOTCOOL"] = "At the end of this all, he's going to say he tricked you all along.";
            }
            if (endingsManager.endingsSeenList.Count >= 8)
            {
                conversationDict["ISNOTCOOL"] = "I told you.";
            }
            return;
        }

        if (gameObjectName == "Raccoon")
        {
            conversationDict["ISCOOL"] = "I'm really overstaying my welcome.";
            conversationDict["BEANGOHINT"] = "Eh, I'm good on beango";
            conversationDict["ISNOTCOOL"] = "I'm just here for the bit.";
            conversationDict["ISBAGGED"] = "I got some trash here if you want some.";
            conversationDict["WRONGBAGGED"] = "I don't get paid enough for this.";
            return;
        }

        if (gameObjectName == "Block o' Cheese")
        {
            conversationDict["ISCOOL"] = "It sure is tough to be as stinky as I am.";

            if (endingsManager.endingsSeenList.Count > 0)
            {
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BeanManUncoolEnding ||
                    endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BeanManLeavesTown ||
                    endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BeanManLeavesBagged)
                {
                    conversationDict["ISCOOL"] = "Are the rumors true? Is there worse smelling cheese than me?";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.LinaBeanEnding)
                {
                    conversationDict["ISCOOL"] = "The good news is that Lina couldn't smell me when she was that tall! I think I have a shot with her";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BeanManWinEnding)
                {
                    conversationDict["ISCOOL"] = "Maybe if I had your glasses they could tolerate my stink";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.ChickPeaEnding)
                {
                    conversationDict["ISCOOL"] = "Chickpea granted me a wish! I messed up though and wished to be even stinkier.";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.FireHydrantEnding)
                {
                    conversationDict["ISCOOL"] = "Fire Hydrant gave me a bath! I'm way less stinky now for sure.";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.GreenBenEnding)
                {
                    conversationDict["ISCOOL"] = "Green is a stinky color. So why does everyone love GreenBen?";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BirthdayCakeEnding)
                {
                    conversationDict["ISCOOL"] = "Birthday Cake said I was too smelly to join their club";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.GrannySmithEnding)
                {
                    conversationDict["ISCOOL"] = "Oh Beango? Yeah I didn't even really want to go anyway.";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.SlimSausageWinning)
                {
                    conversationDict["ISCOOL"] = "Maybe people would tolerate me if I had a voice like Slim.";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.PeanutTwinEnding)
                {
                    conversationDict["ISCOOL"] = "Who was that butter guy?";
                }
            }
            conversationDict["BEANGOHINT"] = "Hey you should get on to Beango."; // This state should push into Beango
            conversationDict["ISNOTCOOL"] = "I stink, you're hideous, we make the perfect pair!";

            if (beanState == gameState.ISNOTCOOL)
            {
                if (endingsManager.endingsSeenList.Count == 0)
                {
                    conversationDict["ISNOTCOOL"] = "I stink, you're hideous, we make the perfect pair!";
                }
                if (endingsManager.endingsSeenList.Count == 1)
                {
                    conversationDict["ISNOTCOOL"] = "Look! I even got new gross things to keep us company!";
                }
                if (endingsManager.endingsSeenList.Count == 2)
                {
                    conversationDict["ISNOTCOOL"] = "No for real though, will you join? I think we could be a cool new club";
                }
                if (endingsManager.endingsSeenList.Count == 3)
                {
                    conversationDict["ISNOTCOOL"] = "Look! I got new smelly and gross things!";
                }
                if (endingsManager.endingsSeenList.Count == 4)
                {
                    conversationDict["ISNOTCOOL"] = "You know what! We don't even need you in this only smelly boys club!";
                }
                if (endingsManager.endingsSeenList.Count == 5)
                {
                    conversationDict["ISNOTCOOL"] = "Ah yes, I see you want to join now that I've rejected you and we have countless members!";
                }
                if (endingsManager.endingsSeenList.Count == 6)
                {
                    conversationDict["ISNOTCOOL"] = "I wanted to introduce you to the president of our club, a real Raccoon!";
                }
                if (endingsManager.endingsSeenList.Count == 7)
                {
                    conversationDict["ISNOTCOOL"] = "Okay, Raccoon left and stole my money. But the club goes on!";
                }
                if (endingsManager.endingsSeenList.Count >= 8)
                {
                    conversationDict["ISNOTCOOL"] = "What's that? All of these smelly things are detracting people?";
                }
            }

            conversationDict["ISBAGGED"] = "We can still team up though right?";

            if (beanState == gameState.ISBAGGED)
            {
                if (winningNPCGameObject.gameObject.name == "Slim Sausage")
                {
                    conversationDict["ISBAGGED"] = "Your glasses? I wouldn't know, but I did hear Slim getting home late last night.";
                }
                if (winningNPCGameObject.gameObject.name == "Fire Hydrant")
                {
                    conversationDict["ISBAGGED"] = "*weasel noises*";
                }
                if (winningNPCGameObject.gameObject.name == "Chickpea Deputy")
                {
                    conversationDict["ISBAGGED"] = "I wouldn't know where your glasses are. No one will talk to me. Because I smell bad.";
                }
                if (winningNPCGameObject.gameObject.name == "Peanut Twins")
                {
                    conversationDict["ISBAGGED"] = "Where did butter go?";
                }
                if (winningNPCGameObject.gameObject.name == "Birthday Cake")
                {
                    conversationDict["ISBAGGED"] = "Being smelly isn't that bad. It has it's perks! I get plenty of alone time!";
                }
                if (winningNPCGameObject.gameObject.name == "Lina Bean")
                {
                    conversationDict["ISBAGGED"] = "I overheard Lina saying she was getting ready to go on some sort of trip.";
                }
                if (winningNPCGameObject.gameObject.name == "GreenBen")
                {
                    conversationDict["ISBAGGED"] = "Take a bath? What? Do I look like I'm made of water?";
                }
            }

            conversationDict["WRONGBAGGED"] = "Who's the smelly one now?";
            if (beanState == gameState.WRONGBAGGED || beanState == gameState.ISBAGGED)
            {
                if (winningNPCGameObject.gameObject.name == "Slim Sausage")
                {
                    conversationDict["WRONGBAGGED"] = "I smell a new album coming soon for Slim!";
                }
                if (winningNPCGameObject.gameObject.name == "Fire Hydrant")
                {
                    conversationDict["WRONGBAGGED"] = "Do you think Fire Hydrant would give me a bath?";
                }
                if (winningNPCGameObject.gameObject.name == "Chickpea Deputy")
                {
                    conversationDict["WRONGBAGGED"] = "Beanman. It seems Chickpea Deputy took your glasses.";
                }
                if (winningNPCGameObject.gameObject.name == "Peanut Twins")
                {
                    conversationDict["WRONGBAGGED"] = "Hey pal, I think Peanut Twin took your glasses.";
                }
                if (winningNPCGameObject.gameObject.name == "Birthday Cake")
                {
                    conversationDict["WRONGBAGGED"] = "Hey Beanman. I hear that Birthday Cake has a shiny new pair of glasses.";
                }
                if (winningNPCGameObject.gameObject.name == "Lina Bean")
                {
                    conversationDict["WRONGBAGGED"] = "You won't like this Bean... But Lina is wearing a new pair of glasses.";
                }
                if (winningNPCGameObject.gameObject.name == "GreenBen")
                {
                    conversationDict["WRONGBAGGED"] = "GreenBen has some sick new shades!";
                }
            }
            return;
        }


        if (gameObjectName == "Poparazzi Corn")
        {
            conversationDict["ISCOOL"] = "BEAN MAN! BeanPeople magazine needs to know about your love life!";

            if (endingsManager.endingsSeenList.Count > 0)
            {
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BeanManUncoolEnding ||
                    endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BeanManLeavesTown ||
                    endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BeanManLeavesBagged)
                {
                    conversationDict["ISCOOL"] = "BEAN MAN! What's the scoop on you leaving town? We heard you were trying to skip bail!";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.LinaBeanEnding)
                {
                    conversationDict["ISCOOL"] = "LINA! Is it true that you are really just edamame?";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BeanManWinEnding)
                {
                    conversationDict["ISCOOL"] = "BEAN MAN! How is it you found your glasses again?";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.ChickPeaEnding)
                {
                    conversationDict["ISCOOL"] = "CHICKPEA! What do you have to say to the public as the first legume ever to harness wizard powers?";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.FireHydrantEnding)
                {
                    conversationDict["ISCOOL"] = "Remind me again why we're interviewing a fire hydrant?";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.GreenBenEnding)
                {
                    conversationDict["ISCOOL"] = "GREENBEN! The people need to know the secret to your cool!";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BirthdayCakeEnding)
                {
                    conversationDict["ISCOOL"] = "Hello Birthday Cake. You should join Birthday Cakes super awesome club.";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.GrannySmithEnding)
                {
                    conversationDict["ISCOOL"] = "GRANNY! Is it true that you stole the glasses so you could have greater chances of winning Beango?";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.SlimSausageWinning)
                {
                    conversationDict["ISCOOL"] = "SLIM! Is it true your song Spam was really inspired by a fan who wrote to you?";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.PeanutTwinEnding)
                {
                    conversationDict["ISCOOL"] = "PEANUT TWIN! The people need to know the secret to your DELICIOUS new product!";
                }
            }
            conversationDict["BEANGOHINT"] = "Listen boys, we have to make it to Beango and watch out for celebrities"; // This state should push into Beango
            conversationDict["ISNOTCOOL"] = "Hey Beanman. We're busy. Could you go away?";
            conversationDict["ISBAGGED"] = "You lost your glasses? We wouldn't know, but come talk to us when you have a better story on this!";


            conversationDict["WRONGBAGGED"] = "Wow! You really guessed the wrong glasses? What a scoop!";
            if (beanState == gameState.WRONGBAGGED || beanState == gameState.ISBAGGED)
            {
                if (winningNPCGameObject.gameObject.name == "Slim Sausage")
                {
                    conversationDict["WRONGBAGGED"] = "What's this? Slim Sausage has a new album coming out? We have to get over there boys!";
                }
                if (winningNPCGameObject.gameObject.name == "Fire Hydrant")
                {
                    conversationDict["WRONGBAGGED"] = "How are we supposed to interview a Fire Hydrant?";
                }
                if (winningNPCGameObject.gameObject.name == "Chickpea Deputy")
                {
                    conversationDict["WRONGBAGGED"] = "The deputy in town?? Chickpea? Is cool? What a story!";
                }
                if (winningNPCGameObject.gameObject.name == "Peanut Twins")
                {
                    conversationDict["WRONGBAGGED"] = "I hear Peanut Twins have a new genius product! Let's get over there boys!";
                }
                if (winningNPCGameObject.gameObject.name == "Birthday Cake")
                {
                    conversationDict["WRONGBAGGED"] = "Birthday Cake has some delicious punch! Let's go over there and take a break boys!";
                }
                if (winningNPCGameObject.gameObject.name == "Lina Bean")
                {
                    conversationDict["WRONGBAGGED"] = "Birthday Cake has some delicious punch! Let's go over there and take a break boys!";
                }
                if (winningNPCGameObject.gameObject.name == "GreenBen")
                {
                    conversationDict["WRONGBAGGED"] = "Well, tragic news would get more clicks, but GreenBen feeling better does sound like a great story!";
                }
            }
            return;
        }


        if (gameObjectName == "Corn Lady")
        {
            conversationDict["ISCOOL"] = "For some stupid reason, the writers seem to think that ears of corn hibernate";
            
            conversationDict["BEANGOHINT"] = "Yes, I will be awake for Beango.";
            conversationDict["ISNOTCOOL"] = "";
            conversationDict["ISBAGGED"] = "I'm all ears";
            conversationDict["WRONGBAGGED"] = "Looks like you aren't as cool as we thought.";
            return;
        }
        
        if (gameObjectName == "Black Eyed Pea")
        {
            conversationDict["ISCOOL"] = "Whatsup Beanman! If you aren't sure what to do next, hit 'Q' to check out your quests!";

            if (endingsManager.endingsSeenList.Count > 0)
            {
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BeanManUncoolEnding ||
                    endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BeanManLeavesTown ||
                    endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BeanManLeavesBagged)
                {
                    conversationDict["ISCOOL"] = "I was thinking about leaving town myself. Maybe do some touring.";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.LinaBeanEnding)
                {
                    conversationDict["ISCOOL"] = "I'm pretty sure Blue Oyster Cult just made a new song about Lina.";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BeanManWinEnding)
                {
                    conversationDict["ISCOOL"] = "Oh thank god you got your glasses back. I can be seen with ya again.";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.ChickPeaEnding)
                {
                    conversationDict["ISCOOL"] = "Despite what everyone else says, I think Chickpea actually looked pretty cool with those shades!";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.FireHydrantEnding)
                {
                    conversationDict["ISCOOL"] = "I don't understand still how that Fire Hydrant took your glasses. He hasn't even moved.";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.GreenBenEnding)
                {
                    conversationDict["ISCOOL"] = "GreenBen deserves all of the attention he's getting. What a good guy.";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BirthdayCakeEnding)
                {
                    conversationDict["ISCOOL"] = "You won't catch me with one of THOSE hats.";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.GrannySmithEnding)
                {
                    conversationDict["ISCOOL"] = "Granny had someone else playing at Beango last night. Not cool man.";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.SlimSausageWinning)
                {
                    conversationDict["ISCOOL"] = "I just went on tour with Slim! It was a blast, you should've been there!";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.PeanutTwinEnding)
                {
                    conversationDict["ISCOOL"] = "Are we really going to ignore what just happened with Butter and the Peanut Twins?";
                }
            }
            conversationDict["BEANGOHINT"] = "I'll see you at Beango Bean! I'll be on the drums!"; // This state should push into Beango
            conversationDict["ISNOTCOOL"] = "Sorry Bean. I just can't do this.";

            if (beanState == gameState.ISNOTCOOL)
            {
                if (endingsManager.endingsSeenList.Count == 0)
                {
                    conversationDict["ISNOTCOOL"] = "Sorry Bean, I'm going to have to cancel our jam session.";
                }
                if (endingsManager.endingsSeenList.Count == 1)
                {
                    conversationDict["ISNOTCOOL"] = "Okay, maybe if... you had a different censor bar or something?";
                }
                if (endingsManager.endingsSeenList.Count == 2)
                {
                    conversationDict["ISNOTCOOL"] = "Sorry. The new bar just isn't working";
                }
                if (endingsManager.endingsSeenList.Count == 3)
                {
                    conversationDict["ISNOTCOOL"] = "I appreciate that you're trying still. But it's just not working.";
                }
                if (endingsManager.endingsSeenList.Count == 4)
                {
                    conversationDict["ISNOTCOOL"] = "Hey look. I'm glad you're back to trying the good ole original censor bar.";
                }
                if (endingsManager.endingsSeenList.Count == 5)
                {
                    conversationDict["ISNOTCOOL"] = "Oh a jam session? Yeah, I'm busy tonight, but maybe another time?";
                }
                if (endingsManager.endingsSeenList.Count == 6)
                {
                    conversationDict["ISNOTCOOL"] = "Oh no, you don't need to change your censor bar again. That's okay";
                }
                if (endingsManager.endingsSeenList.Count == 7)
                {
                    conversationDict["ISNOTCOOL"] = "You've gotta stop bugging me about this.";
                }
                if (endingsManager.endingsSeenList.Count >= 8)
                {
                    conversationDict["ISNOTCOOL"] = "I don't care if you found all of the endings. We're not jamming";
                }
            }

            conversationDict["ISBAGGED"] = "Eh, the paper bag will have to do.";

            if (beanState == gameState.ISBAGGED)
            {
                if (winningNPCGameObject.gameObject.name == "Slim Sausage")
                {
                    conversationDict["ISBAGGED"] = "I got a sneak listen of Slims new album. In short, it's fire.";
                }
                if (winningNPCGameObject.gameObject.name == "Fire Hydrant")
                {
                    conversationDict["ISBAGGED"] = "Hey Bean, yeah it makes me sick that you lost your glasses.";
                }
                if (winningNPCGameObject.gameObject.name == "Chickpea Deputy")
                {
                    conversationDict["ISBAGGED"] = "Chickpea has been acting weird lately.";
                }
                if (winningNPCGameObject.gameObject.name == "Peanut Twins")
                {
                    conversationDict["ISBAGGED"] = "Shouldn't Peanut Twin #2 be more worried that his brother being gone?";
                }
                if (winningNPCGameObject.gameObject.name == "Birthday Cake")
                {
                    conversationDict["ISBAGGED"] = "Birthday Cake offered me some punch, but I just don't get a good vibe from him.";
                }
                if (winningNPCGameObject.gameObject.name == "Lina Bean")
                {
                    conversationDict["ISBAGGED"] = "The bag is... okay";
                }
                if (winningNPCGameObject.gameObject.name == "GreenBen")
                {
                    conversationDict["ISBAGGED"] = "I'd be feeling better too if I had sick glasses like GreenBen has!";
                }
            }

            if (beanState == gameState.WRONGBAGGED || beanState == gameState.ISBAGGED)
            {
                if (winningNPCGameObject.gameObject.name == "Slim Sausage")
                {
                    conversationDict["WRONGBAGGED"] = "Slim Sausage is releasing his new album!";
                }
                if (winningNPCGameObject.gameObject.name == "Fire Hydrant")
                {
                    conversationDict["WRONGBAGGED"] = "That Fire Hydrant took your glasses?";
                }
                if (winningNPCGameObject.gameObject.name == "Chickpea Deputy")
                {
                    conversationDict["WRONGBAGGED"] = "Something feels wrong... there's a magical force radiating Chickpea Deputy.";
                }
                if (winningNPCGameObject.gameObject.name == "Peanut Twins")
                {
                    conversationDict["WRONGBAGGED"] = "Bean. We have to find Butter and Peanut Twin #1, they're both missing!";
                }
                if (winningNPCGameObject.gameObject.name == "Birthday Cake")
                {
                    conversationDict["WRONGBAGGED"] = "Everyone is raving about Birthday Cakes club... don't they realize it's a cult?";
                }
                if (winningNPCGameObject.gameObject.name == "Lina Bean")
                {
                    conversationDict["WRONGBAGGED"] = "Did you know about Lina leaving for Tokyo today?";
                }
                if (winningNPCGameObject.gameObject.name == "GreenBen")
                {
                    conversationDict["WRONGBAGGED"] = "GreenBen looks so much better with his new glasses!";
                }
            }
            return;
        }

        if (gameObjectName == "Cory")
        {
            conversationDict["ISCOOL"] = "Oh my god! Beanman! You're talking... to me?";

            if (endingsManager.endingsSeenList.Count > 0)
            {
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BeanManUncoolEnding ||
                    endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BeanManLeavesTown ||
                    endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BeanManLeavesBagged)
                {
                    conversationDict["ISCOOL"] = "Wow! You must have a lot of time on your hands!";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.LinaBeanEnding)
                {
                    conversationDict["ISCOOL"] = "I hear that Lina really helped the people in Japan!";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BeanManWinEnding)
                {
                    conversationDict["ISCOOL"] = "I want to be part of your adventures! Maybe I'll steal your glasses next time...";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.ChickPeaEnding)
                {
                    conversationDict["ISCOOL"] = "I found these cool glasses earlier today. I gave them to chickpea because I figured he'd give them back to the owner!";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.FireHydrantEnding)
                {
                    conversationDict["ISCOOL"] = "Psssst. The fire hydrant... it talks.";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.GreenBenEnding)
                {
                    conversationDict["ISCOOL"] = "Beanman! I didn't see lina hanging out with greenben. Not one time.";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BirthdayCakeEnding)
                {
                    conversationDict["ISCOOL"] = "Hello Beanman. You should join Birthday Cakes super awesome club.";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.GrannySmithEnding)
                {
                    conversationDict["ISCOOL"] = "Really cool of you to just let Granny use your glasses!";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.SlimSausageWinning)
                {
                    conversationDict["ISCOOL"] = "Slim Sausage's album was a slim dunk!";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.PeanutTwinEnding)
                {
                    conversationDict["ISCOOL"] = "I'm so glad that peanut twin #2 hired butter! What great friends they are!";
                }
            }
            conversationDict["BEANGOHINT"] = "Aren't you supposed to be at Beango?"; // This state should push into Beango
            conversationDict["ISNOTCOOL"] = "*sobs*";

            if (beanState == gameState.ISNOTCOOL)
            {
                if (endingsManager.endingsSeenList.Count == 0)
                {
                    conversationDict["ISNOTCOOL"] = "Oh my. You're really ugly.";
                }
                if (endingsManager.endingsSeenList.Count == 1)
                {
                    conversationDict["ISNOTCOOL"] = "Please. *sniffle* I just can't look at you right now.";
                }
                if (endingsManager.endingsSeenList.Count == 2)
                {
                    conversationDict["ISNOTCOOL"] = "*sobs*";
                }
                if (endingsManager.endingsSeenList.Count == 3)
                {
                    conversationDict["ISNOTCOOL"] = "*intense crying*";
                }
                if (endingsManager.endingsSeenList.Count == 4)
                {
                    conversationDict["ISNOTCOOL"] = "*hysterical crying*";
                }
                if (endingsManager.endingsSeenList.Count == 5)
                {
                    conversationDict["ISNOTCOOL"] = "*so much crying that you forget why you were even crying in the first place*";
                }
                if (endingsManager.endingsSeenList.Count == 6)
                {
                    conversationDict["ISNOTCOOL"] = "*horse noises*";
                }
                if (endingsManager.endingsSeenList.Count == 7)
                {
                    conversationDict["ISNOTCOOL"] = "I'm supposed to be dead";
                }
                if (endingsManager.endingsSeenList.Count >= 8)
                {
                    conversationDict["ISNOTCOOL"] = "I'm supposed to be dead";
                }
            }

            conversationDict["ISBAGGED"] = "Try talking to someone who you think has your glasses!";

            if (beanState == gameState.ISBAGGED)
            {
                if (winningNPCGameObject.gameObject.name == "Slim Sausage")
                {
                    conversationDict["ISBAGGED"] = "I've been hearing loud music coming from Slims house lately.";
                }
                if (winningNPCGameObject.gameObject.name == "Fire Hydrant")
                {
                    conversationDict["ISBAGGED"] = "*big horse noises*";
                }
                if (winningNPCGameObject.gameObject.name == "Chickpea Deputy")
                {
                    conversationDict["ISBAGGED"] = "The writers added me because they wanted 'Cory in the House' somewhere, so they just went with 'Cory is a Mouse'";
                }
                if (winningNPCGameObject.gameObject.name == "Peanut Twins")
                {
                    conversationDict["ISBAGGED"] = "You seem to have a really tough time finding your glasses Bean.";
                }
                if (winningNPCGameObject.gameObject.name == "Birthday Cake")
                {
                    conversationDict["ISBAGGED"] = "I think I just don't love milk because it's not SPICY enough. So I like to dump a bucket of hot sauce in there.";
                }
                if (winningNPCGameObject.gameObject.name == "Lina Bean")
                {
                    conversationDict["ISBAGGED"] = "Is it just me? Or does literally everybody ignore Baked Bush's existence?";
                }
                if (winningNPCGameObject.gameObject.name == "GreenBen")
                {
                    conversationDict["ISBAGGED"] = "Did I miss the memo on this glasses thing?";
                }
            }

            if (beanState == gameState.WRONGBAGGED || beanState == gameState.ISBAGGED)
            {
                if (winningNPCGameObject.gameObject.name == "Slim Sausage")
                {
                    conversationDict["WRONGBAGGED"] = "I found some really neat glasses, I figured they'd help Slim in recording his new album!";
                }
                if (winningNPCGameObject.gameObject.name == "Fire Hydrant")
                {
                    conversationDict["WRONGBAGGED"] = "I'm so glad I put those glasses on that Fire Hydrant. It's hilarious!";
                }
                if (winningNPCGameObject.gameObject.name == "Chickpea Deputy")
                {
                    conversationDict["WRONGBAGGED"] = "I found these cool glasses earlier today. I gave them to chickpea because I figured he'd give them back to the owner!";
                }
                if (winningNPCGameObject.gameObject.name == "Peanut Twins")
                {
                    conversationDict["WRONGBAGGED"] = "Peanut Twin #2 always seemed like he was down about being the second twin. So I gave him some glasses I found!";
                }
                if (winningNPCGameObject.gameObject.name == "Birthday Cake")
                {
                    conversationDict["WRONGBAGGED"] = "Birthday Cake gave me some punch in exchange for some glasses I found!";
                }
                if (winningNPCGameObject.gameObject.name == "Lina Bean")
                {
                    conversationDict["WRONGBAGGED"] = "She seemed hesitant to take them... but Lina really loved the glasses I found!";
                }
                if (winningNPCGameObject.gameObject.name == "GreenBen")
                {
                    conversationDict["WRONGBAGGED"] = "I think GreenBen is feeling a lot better ever since I gave him those cool shades!";
                }
            }
            return;
        }
        if (gameObjectName == "Baked Bush" || gameObjectName == "Ninja Officer Bush")
        {
            conversationDict["ISCOOL"] = "When my pizza gets too hot, I run it through water to cool it down.";

            if (endingsManager.endingsSeenList.Count > 0)
            {
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BeanManUncoolEnding ||
                    endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BeanManLeavesTown ||
                    endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BeanManLeavesBagged)
                {
                    conversationDict["ISCOOL"] = "Have you ever considered that this is all a simulation?";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.LinaBeanEnding)
                {
                    conversationDict["ISCOOL"] = "I'm disapointed in Lina. She TOTALLY ripped off Godzilla.";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BeanManWinEnding)
                {
                    conversationDict["ISCOOL"] = "I was just goofing around talking to that fire hydrant, and it started talking back to me!";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.ChickPeaEnding)
                {
                    conversationDict["ISCOOL"] = "Isn't it kind of weird how we're all standing in the same place everytime you talk to us?";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.FireHydrantEnding)
                {
                    conversationDict["ISCOOL"] = "Why didn't some just remove the glasses from Fire Hydrant? He's defenseless, there's nothing he could do about it.";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.GreenBenEnding)
                {
                    conversationDict["ISCOOL"] = "Since people eat food, should we eat people?";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BirthdayCakeEnding)
                {
                    conversationDict["ISCOOL"] = "Hello Beanman. You should join Birthday Cakes super awesome club.";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.GrannySmithEnding)
                {
                    conversationDict["ISCOOL"] = "I had a dream last night that I was a horse and Granny Smith rode on my back into the sunset";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.SlimSausageWinning)
                {
                    conversationDict["ISCOOL"] = "Now my new toilet I bought is talking to me too.";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.PeanutTwinEnding)
                {
                    conversationDict["ISCOOL"] = "This stuff really gets stuck on the roof of your mouth.";
                }
            }
            conversationDict["BEANGOHINT"] = "Beango sounds fun, but so do video games."; 
            conversationDict["ISNOTCOOL"] = "Please leave me alone or I WILL call the cops.";


            if (beanState == gameState.ISNOTCOOL)
            {
                if (endingsManager.endingsSeenList.Count == 0)
                {
                    conversationDict["ISNOTCOOL"] = "Please leave me alone or I WILL call the cops.";
                }
                if (endingsManager.endingsSeenList.Count == 1)
                {
                    conversationDict["ISNOTCOOL"] = "Okay that's it, I'm calling them.";
                }
                if (endingsManager.endingsSeenList.Count == 2)
                {
                    conversationDict["ISNOTCOOL"] = "Hi there. Ninja Officer Bush here. I'm not afraid to throw ninja stars on sight";
                }
                if (endingsManager.endingsSeenList.Count == 3)
                {
                    //Have a body on the ground next to Baked Bush, talk to the body and it's just a dude who was hired by Officer Bush
                    conversationDict["ISNOTCOOL"] = "I'm sorry. I didn't... I didn't mean to hurt anyone.";
                }
                if (endingsManager.endingsSeenList.Count == 4)
                {
                    conversationDict["ISNOTCOOL"] = "What has my life become? All I wanted was to be a good ninja cop who protected people.";
                }
                if (endingsManager.endingsSeenList.Count == 5)
                {
                    conversationDict["ISNOTCOOL"] = "I wanted to protect... But I only damaged the ones I love around me.";
                }
                if (endingsManager.endingsSeenList.Count == 6)
                {
                    conversationDict["ISNOTCOOL"] = "I think it's time that I hangup the ole badge and headband.";
                }
                if (endingsManager.endingsSeenList.Count == 7)
                {
                    conversationDict["ISNOTCOOL"] = "I'm retired now. It was a good run. But my mistakes haunt me every day.";
                }
                if (endingsManager.endingsSeenList.Count >= 8)
                {
                    conversationDict["ISNOTCOOL"] = "Hey Bean. Faked ya! I wasn't actually a ninja cop.";
                }
            }


            conversationDict["ISBAGGED"] = "Okay fine, cuff me boys.";
            if (beanState == gameState.ISBAGGED)
            {
                if (winningNPCGameObject.gameObject.name == "Slim Sausage")
                {
                    conversationDict["ISBAGGED"] = "My toilet started talking to me last night and said it was hungry. So I bought a new toilet.";
                }
                if (winningNPCGameObject.gameObject.name == "Fire Hydrant")
                {
                    conversationDict["ISBAGGED"] = "Your glasses? I lost mine too! I'm pretending that I'm on an adventure where I have to get my cool back.";
                }
                if (winningNPCGameObject.gameObject.name == "Chickpea Deputy")
                {
                    conversationDict["ISBAGGED"] = "I tried to sneak into Beango the other night. It was actually pretty easy.";
                }
                if (winningNPCGameObject.gameObject.name == "Peanut Twins")
                {
                    conversationDict["ISBAGGED"] = "I have a theory that food isn't supposed to talk. If I'm correct, then this game makes no sense at all.";
                }
                if (winningNPCGameObject.gameObject.name == "Birthday Cake")
                {
                    conversationDict["ISBAGGED"] = "Birthday Cake is making punch!";
                }
                if (winningNPCGameObject.gameObject.name == "Lina Bean")
                {
                    conversationDict["ISBAGGED"] = "Did you notice how no body in this town has teeth? Well, except for Granny. But those are fake.";
                }
                if (winningNPCGameObject.gameObject.name == "GreenBen")
                {
                    conversationDict["ISBAGGED"] = "Have you ever had Bean milk? To clarify, it's milk for beans. Not milk made out of beans.";
                }
            }

            if (beanState == gameState.WRONGBAGGED || beanState == gameState.ISBAGGED)
            {
                if (winningNPCGameObject.gameObject.name == "Slim Sausage")
                {
                    conversationDict["WRONGBAGGED"] = "This weekend, I'm just staying in my room, and jamming to Slims new album.";
                }
                if (winningNPCGameObject.gameObject.name == "Fire Hydrant")
                {
                    conversationDict["WRONGBAGGED"] = "So the fire hydrant stole your glasses too?";
                }
                if (winningNPCGameObject.gameObject.name == "Chickpea Deputy")
                {
                    conversationDict["WRONGBAGGED"] = "Deputys' aren't supposed to act evil right?";
                }
                if (winningNPCGameObject.gameObject.name == "Peanut Twins")
                {
                    conversationDict["WRONGBAGGED"] = "I've heard something about this new... Peanut Butter? Sounds interesting.";
                }
                if (winningNPCGameObject.gameObject.name == "Birthday Cake")
                {
                    conversationDict["WRONGBAGGED"] = "Man, I can't get enough of this punch.";
                }
                if (winningNPCGameObject.gameObject.name == "Lina Bean")
                {
                    conversationDict["WRONGBAGGED"] = "Lina Leaves for Tokyo... That should be the sequel to this game.";
                }
                if (winningNPCGameObject.gameObject.name == "GreenBen")
                {
                    conversationDict["WRONGBAGGED"] = "I had a dream last night that I was a horse and GreenBen rode on my back into the sunset";
                }
            }
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
                    conversationDict["ISCOOL"] = "Race car beds are underrated. You're a cool guy, do you have a race car bed too?";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BeanManWinEnding)
                {
                    conversationDict["ISCOOL"] = "Your glasses are back! Yay!";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.ChickPeaEnding)
                {
                    conversationDict["ISCOOL"] = "I was pretending to be a deputy just like Chickpea!";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.FireHydrantEnding)
                {
                    conversationDict["ISCOOL"] = "I didn't know fire hydrants can talk";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.GreenBenEnding)
                {
                    conversationDict["ISCOOL"] = "I sure am glad to see Green Ben feeling better!";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BirthdayCakeEnding)
                {
                    conversationDict["ISCOOL"] = "Hello Beanman. You should join Birthday Cakes super awesome club.";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.GrannySmithEnding)
                {
                    conversationDict["ISCOOL"] = "Granny let me watch them play Beango from outside!";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.SlimSausageWinning)
                {
                    conversationDict["ISCOOL"] = "Slims album really buttered me up!";
                }
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.PeanutTwinEnding)
                {
                    conversationDict["ISCOOL"] = "I really shouldn't be here";
                }
            }
            conversationDict["BEANGOHINT"] = "I wish I was old enough to go to Beango!!"; // This state should push into Beango
            conversationDict["ISNOTCOOL"] = "Yeah, I need to go";

            if (beanState == gameState.ISNOTCOOL)
            {
                if (endingsManager.endingsSeenList.Count == 0)
                {
                    conversationDict["ISNOTCOOL"] = "Yeah, I need to go";
                }
                if (endingsManager.endingsSeenList.Count == 1)
                {
                    conversationDict["ISNOTCOOL"] = "Okay, you can stop talking to me now.";
                }
                if (endingsManager.endingsSeenList.Count == 2)
                {
                    conversationDict["ISNOTCOOL"] = "I would rather melt than be here talking to you.";
                }
                if (endingsManager.endingsSeenList.Count == 3)
                {
                    conversationDict["ISNOTCOOL"] = "I would rather god grab me and use me in a delicious cake than talk to you.";
                }
                if (endingsManager.endingsSeenList.Count == 4)
                {
                    conversationDict["ISNOTCOOL"] = "I would rather be betrayed by my best friend than be here with you.";
                }
                if (endingsManager.endingsSeenList.Count == 5)
                {
                    conversationDict["ISNOTCOOL"] = "I'd rather have someone use me to make money than be here with you.";
                }
                if (endingsManager.endingsSeenList.Count == 6)
                {
                    conversationDict["ISNOTCOOL"] = "I'd rather be hanging out with Block o' Cheese than hang here with you.";
                }
                if (endingsManager.endingsSeenList.Count == 7)
                {
                    conversationDict["ISNOTCOOL"] = "Please just leave me alone.";
                }
                if (endingsManager.endingsSeenList.Count == 8)
                {
                    conversationDict["ISNOTCOOL"] = "If I'm here. Then the devs may have messed something up.";
                }
                if (endingsManager.endingsSeenList.Count == 9)
                {
                    conversationDict["ISNOTCOOL"] = "At this point, I really shouldn't be alive.";
                }
            }


            conversationDict["ISBAGGED"] = "Okay I can deal with this.";
            if (beanState == gameState.ISBAGGED)
            {
                if (winningNPCGameObject.gameObject.name == "Slim Sausage")
                {
                    conversationDict["ISBAGGED"] = "People don't know this, but I'm actually a slip n' slide master.";
                }
                if (winningNPCGameObject.gameObject.name == "Fire Hydrant")
                {
                    conversationDict["ISBAGGED"] = "I think that bag looks good on you Bean. The glasses seem outdated anyway.";
                }
                if (winningNPCGameObject.gameObject.name == "Chickpea Deputy")
                {
                    conversationDict["ISBAGGED"] = "The developers made it so I can only smile. You can imagine how much my face hurts right now.";
                }
                if (winningNPCGameObject.gameObject.name == "Peanut Twins")
                {
                    conversationDict["ISBAGGED"] = "If I'm talking and alive, then this is DEFINITELY a bug that the programmers missed.";
                }
                if (winningNPCGameObject.gameObject.name == "Birthday Cake")
                {
                    conversationDict["ISBAGGED"] = "Birthday cake asked me to wear a Traffic Cone hat. I'm honestly considering it!";
                }
                if (winningNPCGameObject.gameObject.name == "Lina Bean")
                {
                    conversationDict["ISBAGGED"] = "Did Lina get taller? Or did I get shorter? I think I'm having a crisis";
                }
                if (winningNPCGameObject.gameObject.name == "GreenBen")
                {
                    conversationDict["ISBAGGED"] = "It's rather sunny out today.";
                }
            }

            if (beanState == gameState.WRONGBAGGED || beanState == gameState.ISBAGGED)
            {
                if (winningNPCGameObject.gameObject.name == "Slim Sausage")
                {
                    conversationDict["WRONGBAGGED"] = "I think I'm a little too young to understand Slims music. But I'm going to listen to his new album!";
                }
                if (winningNPCGameObject.gameObject.name == "Fire Hydrant")
                {
                    conversationDict["WRONGBAGGED"] = "Why would you think " + wrongNPCGameObject.gameObject.name + " took your glasses?";
                }
                if (winningNPCGameObject.gameObject.name == "Chickpea Deputy")
                {
                    conversationDict["WRONGBAGGED"] = "Chickpea gave me some weird magic beans. Kind of creepy since he is one.";
                }
                if (winningNPCGameObject.gameObject.name == "Peanut Twins")
                {
                    conversationDict["WRONGBAGGED"] = "I'm not supposed to be alive.";
                }
                if (winningNPCGameObject.gameObject.name == "Birthday Cake")
                {
                    conversationDict["WRONGBAGGED"] = "I just signed up for Birthday Cakes club!";
                }
                if (winningNPCGameObject.gameObject.name == "Lina Bean")
                {
                    conversationDict["WRONGBAGGED"] = "I'm just nervous that Lina might be a little too tall for Tokyo.";
                }
                if (winningNPCGameObject.gameObject.name == "GreenBen")
                {
                    conversationDict["WRONGBAGGED"] = "GreenBen gave me a high five today! He's looking great!";
                }
            }
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


            if (beanState == gameState.ISNOTCOOL)
            {
                if (endingsManager.endingsSeenList.Count == 0)
                {
                    conversationDict["ISNOTCOOL"] = "I'd lock you up if I didn't just have leg day";
                }
                if (endingsManager.endingsSeenList.Count == 1)
                {
                    conversationDict["ISNOTCOOL"] = "Sorry, I can't hear you over the sound of my abs chiseling into eachother.";
                }
                if (endingsManager.endingsSeenList.Count == 2)
                {
                    conversationDict["ISNOTCOOL"] = "I'll fight you. I don't care.";
                }
                if (endingsManager.endingsSeenList.Count == 3)
                {
                    conversationDict["ISNOTCOOL"] = "I'm not overcompensating. What? That puny body I had before? That was the old me.";
                }
                if (endingsManager.endingsSeenList.Count == 4)
                {
                    conversationDict["ISNOTCOOL"] = "Don't look at my mustache like that. It's cool. My mom says she loves it.";
                }
                if (endingsManager.endingsSeenList.Count == 5)
                {
                    conversationDict["ISNOTCOOL"] = "Look, it doesn't matter if I have a race car bed and drink a warm glass of milk before bed. I'm ripped bro";
                }
                if (endingsManager.endingsSeenList.Count == 6)
                {
                    conversationDict["ISNOTCOOL"] = "";
                }
                if (endingsManager.endingsSeenList.Count == 7)
                {
                    conversationDict["ISNOTCOOL"] = "";
                }
                if (endingsManager.endingsSeenList.Count >= 8)
                {
                    conversationDict["ISNOTCOOL"] = "";
                }
            }

            conversationDict["ISBAGGED"] = "What do you want?";

            if (endingsManager.endingsSeenList.Count == 1)
            {
                conversationDict["ISCOOL"] = "Huh, I didn't know that ears of corn hibernate.";
            }
            if (beanState == gameState.ISBAGGED)
            {
                if (winningNPCGameObject.gameObject.name == "Slim Sausage")
                {
                    conversationDict["ISBAGGED"] = "I've been thinking about retiring from being the deputy so I can be a showtune writer.";
                }
                if (winningNPCGameObject.gameObject.name == "Fire Hydrant")
                {
                    conversationDict["ISBAGGED"] = "What do you want weakling?";
                }
                if (winningNPCGameObject.gameObject.name == "Chickpea Deputy")
                {
                    conversationDict["ISBAGGED"] = "*suspicious noises*";
                }
                if (winningNPCGameObject.gameObject.name == "Peanut Twins")
                {
                    conversationDict["ISBAGGED"] = "I think I should maybe arrest that sewer hole. It creeps me out.";
                }
                if (winningNPCGameObject.gameObject.name == "Birthday Cake")
                {
                    conversationDict["ISBAGGED"] = "Heh, sick bag BRO";
                }
                if (winningNPCGameObject.gameObject.name == "Lina Bean")
                {
                    conversationDict["ISBAGGED"] = "I've been hitting the gym EXTRA hard lately.";
                }
                if (winningNPCGameObject.gameObject.name == "GreenBen")
                {
                    conversationDict["ISBAGGED"] = "Birthday Cake said he has pin the tail on the donkey. I'll be honest. I don't trust it.";
                }
            }


            conversationDict["BEANGOHINT"] = "Oh boy Beango? Gosh, I wish I was cool enough to join in!";
            conversationDict["WRONGGUESS"] = "Seriously? Do you want me to lock you up? I bought these at the Bean Topic outlet.";

            if (beanState == gameState.WRONGBAGGED || beanState == gameState.ISBAGGED)
            {
                if (winningNPCGameObject.gameObject.name == gameObjectName)
                {
                    conversationDict["WRONGBAGGED"] = "YOU FOOL!";
                }
                else if (wrongNPCGameObject.gameObject.name == gameObjectName)
                {
                    conversationDict["WRONGBAGGED"] = "Seriously? Do you want me to lock you up? I bought these at the Bean Topic outlet.";
                }
                else
                {
                    conversationDict["WRONGBAGGED"] = "Did I hear a rumor that you were casting slander across the great people of Beantown?";
                }
            }
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

            conversationDict["ISNOTCOOL"] = "Oof, yeah no amount of lemons is going to help you.";

            if (endingsManager.endingsSeenList.Count > 1)
            {
                conversationDict["ISNOTCOOL"] = "Well at least I'm not you.";
            }
            conversationDict["ISBAGGED"] = "I was happier when you weren't like this.";
            conversationDict["BEANGOHINT"] = "You should probably get going to Beango";

            conversationDict["WRONGBAGGED"] = "I'll get the lemons ready.";
            return;
        }

        // Conversation loading for Lina Bean
        if (gameObjectName == "Lina Bean" || gameObjectName == "Lina Edamame")
        {
            conversationDict["ISCOOL"] = "Bean Man! Are we still on for the movies this weekend?!";

            if (endingsManager.endingsSeenList.Count > 0)
            {
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BeanManWinEnding)
                {
                    conversationDict["ISCOOL"] = "I'm sorry for what I said Bean. Maybe we could still go to the movies this weekend?";
                }
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

            if (beanState == gameState.ISNOTCOOL)
            {
                if (endingsManager.endingsSeenList.Count == 0)
                {
                    conversationDict["ISNOTCOOL"] = "*Swipes Left*";
                }
                if (endingsManager.endingsSeenList.Count == 1)
                {
                    conversationDict["ISNOTCOOL"] = "";
                }
                if (endingsManager.endingsSeenList.Count == 2)
                {
                    conversationDict["ISNOTCOOL"] = "";
                }
                if (endingsManager.endingsSeenList.Count == 3)
                {
                    conversationDict["ISNOTCOOL"] = "";
                }
                if (endingsManager.endingsSeenList.Count == 4)
                {
                    conversationDict["ISNOTCOOL"] = "";
                }
                if (endingsManager.endingsSeenList.Count == 5)
                {
                    conversationDict["ISNOTCOOL"] = "";
                }
                if (endingsManager.endingsSeenList.Count == 6)
                {
                    conversationDict["ISNOTCOOL"] = "";
                }
                if (endingsManager.endingsSeenList.Count == 7)
                {
                    conversationDict["ISNOTCOOL"] = "";
                }
                if (endingsManager.endingsSeenList.Count >= 8)
                {
                    conversationDict["ISNOTCOOL"] = "";
                }
            }

            conversationDict["ISBAGGED"] = "Working out WON'T get my attention. *Wink*";

            if (beanState == gameState.ISBAGGED)
            {
                if (winningNPCGameObject.gameObject.name == "Slim Sausage")
                {
                    conversationDict["ISBAGGED"] = "Oh hey Bean, didn't you see you down there.";
                }
                if (winningNPCGameObject.gameObject.name == "Fire Hydrant")
                {
                    conversationDict["ISBAGGED"] = "*monkey noises*";
                }
                if (winningNPCGameObject.gameObject.name == "Chickpea Deputy")
                {
                    conversationDict["ISBAGGED"] = "Working out WON'T get my attention. *Wink*";
                }
                if (winningNPCGameObject.gameObject.name == "Peanut Twins")
                {
                    conversationDict["ISBAGGED"] = "Go run along little boy... Oh, Beanman. It's you.";
                }
                if (winningNPCGameObject.gameObject.name == "Birthday Cake")
                {
                    conversationDict["ISBAGGED"] = "I hear that traffic cones are starting to come in style!";
                }
                if (winningNPCGameObject.gameObject.name == "Lina Bean")
                {
                    conversationDict["ISBAGGED"] = "Oh Bean! I didn't recognize you with the bag on your head.";
                }
                if (winningNPCGameObject.gameObject.name == "GreenBen")
                {
                    conversationDict["ISBAGGED"] = "Birthday Cake really needs better ideas. Pin the tail on the donkey doesn't sound very fun.";
                }
            }

            conversationDict["WRONGGUESS"] = "How DARE you accuse me. We're off for the movies this weekend!";

            if (beanState == gameState.WRONGBAGGED || beanState == gameState.ISBAGGED)
            {
                if (winningNPCGameObject.gameObject.name == gameObjectName)
                {
                    conversationDict["WRONGBAGGED"] = "I'm so sorry Bean.";
                }
                else if (wrongNPCGameObject.gameObject.name == gameObjectName)
                {
                    conversationDict["WRONGBAGGED"] = "How DARE you accuse me. We're off for the movies this weekend!";
                }
                else {
                    conversationDict["WRONGBAGGED"] = "Bean, why did you think " + wrongNPCGameObject.gameObject.name  + " took the glasses?";
                }
            }
            return;
        }

        // Conversation laoding for Birthday Cake
        if (gameObjectName == "Birthday Cake" || gameObjectName == "Party Cake")
        {
            conversationDict["ISCOOL"] = "I'm not hoarding traffic cones! Also, would you be interested in joining my cul- I mean club?";

            if (endingsManager.endingsSeenList.Count > 0)
            {
                if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BeanManUncoolEnding ||
                endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BeanManLeavesTown ||
                endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BeanManLeavesBagged)
                {
                    conversationDict["ISCOOL"] = "Yeah okay, you got me. This is just a cult.";
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
            conversationDict["ISNOTCOOL"] = "Oh, yeah we don't really need you to join us. I'm a little busy prepping over here anyway.";


            if (beanState == gameState.ISNOTCOOL)
            {
                if (endingsManager.endingsSeenList.Count == 0)
                {
                    conversationDict["ISNOTCOOL"] = "Oh, yeah we don't really need you to join us. I'm a little busy prepping over here anyway.";
                }
                if (endingsManager.endingsSeenList.Count == 1)
                {
                    conversationDict["ISNOTCOOL"] = "I wish I had more room so I could put more decorations for my club meeting";
                }
                if (endingsManager.endingsSeenList.Count == 2)
                {
                    conversationDict["ISNOTCOOL"] = "The extra room just makes sense, because I'm going to have A LOT of people here.";
                }
                if (endingsManager.endingsSeenList.Count == 3)
                {
                    conversationDict["ISNOTCOOL"] = "Don't worry! People will definitely show up!";
                }
                if (endingsManager.endingsSeenList.Count == 4)
                {
                    conversationDict["ISNOTCOOL"] = "I have balloons! How could anyone possibly resist balloons?";
                }
                if (endingsManager.endingsSeenList.Count == 5)
                {
                    conversationDict["ISNOTCOOL"] = "I don't like how I can see your eyes judging me. You should really find those glasses.";
                }
                if (endingsManager.endingsSeenList.Count == 6)
                {
                    conversationDict["ISNOTCOOL"] = "I know. I'm a birthday cake. Not necessarily a party cake.";
                }
                if (endingsManager.endingsSeenList.Count == 7)
                {
                    conversationDict["ISNOTCOOL"] = "I've changed my name to party cake! Now it all makes sense.";
                }
                if (endingsManager.endingsSeenList.Count >= 8)
                {
                    conversationDict["ISNOTCOOL"] = "Yeah okay, you got me. This is just a cult.";
                }
            }

            conversationDict["ISBAGGED"] = "Hello sir! Are you interested in learning about a fun new religion?";

            if (beanState == gameState.ISBAGGED)
            {
                if (winningNPCGameObject.gameObject.name == "Slim Sausage")
                {
                    conversationDict["ISBAGGED"] = "Personally, I think getting your cool back is overrated.";
                }
                if (winningNPCGameObject.gameObject.name == "Fire Hydrant")
                {
                    conversationDict["ISBAGGED"] = "Okay fine. It's a cult. You caught me.";
                }
                if (winningNPCGameObject.gameObject.name == "Chickpea Deputy")
                {
                    conversationDict["ISBAGGED"] = "I'll help you get your glasses, IF you join my club :)";
                }
                if (winningNPCGameObject.gameObject.name == "Peanut Twins")
                {
                    conversationDict["ISBAGGED"] = "Beanman. Son. We need to talk. I think you should be my dad.";
                }
                if (winningNPCGameObject.gameObject.name == "Birthday Cake")
                {
                    conversationDict["ISBAGGED"] = "Hello sir! Are you interested in learning about a fun new religion?";
                }
                if (winningNPCGameObject.gameObject.name == "Lina Bean")
                {
                    conversationDict["ISBAGGED"] = "Oh hey there. I like your bag";
                }
                if (winningNPCGameObject.gameObject.name == "GreenBen")
                {
                    conversationDict["ISBAGGED"] = "Want to come hang at my club this weekend? There will be pin the tail on the Donkey";
                }
            }

            conversationDict["BEANGOHINT"] = "Maybe if I go to beango I can get new members...";
            conversationDict["WRONGGUESS"] = "I WISH I had your glasses. Maybe everyone would actually join my club.";

            if (beanState == gameState.WRONGBAGGED || beanState == gameState.ISBAGGED)
            {
                if (wrongNPCGameObject.gameObject.name == gameObjectName)
                {
                    conversationDict["WRONGBAGGED"] = "I WISH I had your glasses. Maybe everyone would actually join my club.";
                }
                else
                {
                    if (winningNPCGameObject.gameObject.name == "Slim Sausage")
                    {
                        conversationDict["WRONGBAGGED"] = "I'm going to play Slims new album at my future cul... I mean, club meetings.";
                    }
                    if (winningNPCGameObject.gameObject.name == "Fire Hydrant")
                    {
                        conversationDict["WRONGBAGGED"] = "So... Do Fire Hydrants need to eat to survive?";
                    }
                    if (winningNPCGameObject.gameObject.name == "Chickpea Deputy")
                    {
                        conversationDict["WRONGBAGGED"] = "I hear Chickpea has a big speech coming up soon!";
                    }
                    if (winningNPCGameObject.gameObject.name == "Peanut Twins")
                    {
                        conversationDict["WRONGBAGGED"] = "I don't want to brag, but I gave Peanut Twins the idea for their new product.";
                    }
                    if (winningNPCGameObject.gameObject.name == "Birthday Cake")
                    {
                        conversationDict["WRONGBAGGED"] = "Interested in some punch Beanman?";
                    }
                    if (winningNPCGameObject.gameObject.name == "Lina Bean")
                    {
                        conversationDict["WRONGBAGGED"] = "So Lina's going to Tokyo... Hmm I wonder if I could hitch a ride.";
                    }
                    if (winningNPCGameObject.gameObject.name == "GreenBen")
                    {
                        conversationDict["WRONGBAGGED"] = "Oh! Yeah GreenBen is feeling better because of my club! Yep. That's it.";
                    }
                }
            }
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
                    conversationDict["ISCOOL"] = "GreenBen gave us a piggy back ride today!";
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
            conversationDict["ISNOTCOOL"] = "Oh hey Bean Boy, I'm pretty busy. Maybe go play with that stick?";

            if (beanState == gameState.ISNOTCOOL)
            {
                if (endingsManager.endingsSeenList.Count == 0)
                {
                    conversationDict["ISNOTCOOL"] = "Oh hey Bean Boy, I'm pretty busy. Maybe go play with that stick?";
                }
                if (endingsManager.endingsSeenList.Count == 1)
                {
                    conversationDict["ISNOTCOOL"] = "Oh hey Bean Boy, I'm pretty busy. Maybe go play with that carton of milk?";
                }
                if (endingsManager.endingsSeenList.Count == 2)
                {
                    conversationDict["ISNOTCOOL"] = "Oh hey Bean Boy, I'm pretty busy. Maybe go play with that carton of can of tuna?";
                }
                if (endingsManager.endingsSeenList.Count == 3)
                {
                    conversationDict["ISNOTCOOL"] = "Oh hey Bean Boy, I'm pretty busy. Maybe go play with that Krabby Patty Secret formuler?";
                }
                if (endingsManager.endingsSeenList.Count == 4)
                {
                    conversationDict["ISNOTCOOL"] = "Oh hey Bean Boy, I'm pretty busy. Maybe go talk with that horse?";
                }
                if (endingsManager.endingsSeenList.Count == 5)
                {
                    conversationDict["ISNOTCOOL"] = "Oh hey Bean Boy, I'm pretty busy. Maybe go play with that Michael Jackson?";
                }
                if (endingsManager.endingsSeenList.Count == 6)
                {
                    conversationDict["ISNOTCOOL"] = "Oh hey Bean Boy, I'm pretty busy. Maybe go play with that Screaming Hairy Armadillo?";
                }
                if (endingsManager.endingsSeenList.Count == 7)
                {
                    conversationDict["ISNOTCOOL"] = "Oh hey Bean Boy, I'm pretty busy. Maybe go talk with that really friendly guy who will 100% absolutely talk to you?";
                }
                if (endingsManager.endingsSeenList.Count >= 8)
                {
                    conversationDict["ISNOTCOOL"] = "Oh hey Bean Boy, I'm pretty busy. Maybe go play with those glasses?";
                }
            }


            conversationDict["ISBAGGED"] = "What is it son, I'm very busy.";

            if (beanState == gameState.ISBAGGED)
            {
                if (winningNPCGameObject.gameObject.name == "Slim Sausage")
                {
                    conversationDict["ISBAGGED"] = "I've been trying to come up with ways to make a million dollars. It's tough though.";
                }
                if (winningNPCGameObject.gameObject.name == "Fire Hydrant")
                {
                    conversationDict["ISBAGGED"] = "Who me? No it has to be Fire Hydrant who has the glasses!";
                }
                if (winningNPCGameObject.gameObject.name == "Chickpea Deputy")
                {
                    conversationDict["ISBAGGED"] = "You lost your glasses? You should report it to Chickpea";
                }
                if (winningNPCGameObject.gameObject.name == "Peanut Twins")
                {
                    conversationDict["ISBAGGED"] = "Butter? Yeah, I haven't heard from him lately either.";
                }
                if (winningNPCGameObject.gameObject.name == "Birthday Cake")
                {
                    conversationDict["ISBAGGED"] = "I've been thinking about buying a new hat.";
                }
                if (winningNPCGameObject.gameObject.name == "Lina Bean")
                {
                    conversationDict["ISBAGGED"] = "I've really been wanting to travel lately.";
                }
                if (winningNPCGameObject.gameObject.name == "GreenBen")
                {
                    conversationDict["ISBAGGED"] = "After talking to Lemonade Stand, I never realized the sacrifices it takes to make money";
                }
            }

            conversationDict["BEANGOHINT"] = "When we're older, we want to go to Beango with you!";
            conversationDict["WRONGGUESS"] = "These are my glasses! We bought them yesterday. You can't take these from me!";

            if (beanState == gameState.WRONGBAGGED || beanState == gameState.ISBAGGED)
            {
                if (wrongNPCGameObject.gameObject.name == gameObjectName)
                {
                    conversationDict["WRONGBAGGED"] = "These are my glasses! I bought them yesterday. You can't take these from me!";
                }
                else
                {
                    if (winningNPCGameObject.gameObject.name == "Slim Sausage")
                    {
                        conversationDict["WRONGBAGGED"] = "I need to try listening to Slims old music before the new album drops!";
                    }
                    if (winningNPCGameObject.gameObject.name == "Fire Hydrant")
                    {
                        conversationDict["WRONGBAGGED"] = "I just saw Fire Hydrant trying to run away from town!";
                    }
                    if (winningNPCGameObject.gameObject.name == "Chickpea Deputy")
                    {
                        conversationDict["WRONGBAGGED"] = "Chickpea seems different lately.";
                    }
                    if (winningNPCGameObject.gameObject.name == "Peanut Twins")
                    {
                        conversationDict["WRONGBAGGED"] = "Who needs friends when you have money!";
                    }
                    if (winningNPCGameObject.gameObject.name == "Birthday Cake")
                    {
                        conversationDict["WRONGBAGGED"] = "I can't get ENOUGH of this punch.";
                    }
                    if (winningNPCGameObject.gameObject.name == "Lina Bean")
                    {
                        conversationDict["WRONGBAGGED"] = "Did Lina get leg implants?";
                    }
                    if (winningNPCGameObject.gameObject.name == "GreenBen")
                    {
                        conversationDict["WRONGBAGGED"] = "I'm nervous to ask, but do you think GreenBen would give us a piggy back ride?";
                    }
                }
            }
            return;
        }

        // Conversation laoding for Greenben
        if (gameObjectName == "GreenBen" || gameObjectName == "Jason")
        {
            conversationDict["ISCOOL"] = "Is that you Bean Man? I really hope I can feel better soon.";
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

            if (beanState == gameState.ISNOTCOOL)
            {
                if (endingsManager.endingsSeenList.Count == 0)
                {
                    conversationDict["ISNOTCOOL"] = "Ouch, what happened to you?";
                }
                if (endingsManager.endingsSeenList.Count == 1)
                {
                    conversationDict["ISNOTCOOL"] = "Oo ouch Ouchy, happened to you what?";
                }
                if (endingsManager.endingsSeenList.Count == 2)
                {
                    conversationDict["ISNOTCOOL"] = "Orph, to you what happened?";
                }
                if (endingsManager.endingsSeenList.Count == 3)
                {
                    conversationDict["ISNOTCOOL"] = "Happened do to you what oh?";
                }
                if (endingsManager.endingsSeenList.Count == 4)
                {
                    conversationDict["ISNOTCOOL"] = "Do to happened you what oof?";
                }
                if (endingsManager.endingsSeenList.Count == 5)
                {
                    conversationDict["ISNOTCOOL"] = "Happened what gooby goo goo";
                }
                if (endingsManager.endingsSeenList.Count == 6)
                {
                    conversationDict["ISNOTCOOL"] = "Ouchy glooby gooby goo";
                }
                if (endingsManager.endingsSeenList.Count == 7)
                {
                    conversationDict["ISNOTCOOL"] = "*chimp noises*";
                }
                if (endingsManager.endingsSeenList.Count >= 8)
                {
                    conversationDict["ISNOTCOOL"] = "I think the writers just had no idea what to write for me.";
                }
            }

            conversationDict["ISBAGGED"] = "Oh you look much better now!";

            if (beanState == gameState.ISBAGGED)
            {
                if (winningNPCGameObject.gameObject.name == "Slim Sausage")
                {
                    conversationDict["ISBAGGED"] = "It is a little weird that everyone got glasses RIGHT when you lost yours.";
                }
                if (winningNPCGameObject.gameObject.name == "Fire Hydrant")
                {
                    conversationDict["ISBAGGED"] = "I'm so glad I feel better now!";
                }
                if (winningNPCGameObject.gameObject.name == "Chickpea Deputy")
                {
                    conversationDict["ISBAGGED"] = "What would you do if you could have wizard powers?";
                }
                if (winningNPCGameObject.gameObject.name == "Peanut Twins")
                {
                    conversationDict["ISBAGGED"] = "Man, I'm hungry.";
                }
                if (winningNPCGameObject.gameObject.name == "Birthday Cake")
                {
                    conversationDict["ISBAGGED"] = "Man, I'm thirsty.";
                }
                if (winningNPCGameObject.gameObject.name == "Lina Bean")
                {
                    conversationDict["ISBAGGED"] = "Believe it or not, I'm the runt in my family.";
                }
                if (winningNPCGameObject.gameObject.name == "GreenBen")
                {
                    conversationDict["ISBAGGED"] = "Hey Bean. Yeah it sucks that you lost your glasses.";
                }
            }

            conversationDict["BEANGOHINT"] = "Beango sounds great but I think I need to get some rest.";
            conversationDict["WRONGGUESS"] = "Really? I got these glasses so I could stay in the sun longer. What got into you?";

            if (beanState == gameState.WRONGBAGGED || beanState == gameState.ISBAGGED)
            {
                if (wrongNPCGameObject.gameObject.name == gameObjectName)
                {
                    conversationDict["WRONGBAGGED"] = "Really? I got these glasses so I could stay in the sun longer. What got into you?";
                }
                else
                {
                    if (winningNPCGameObject.gameObject.name == "Slim Sausage")
                    {
                        conversationDict["WRONGBAGGED"] = "I'm going to jam out to Slims new album today. I can't wait!";
                    }
                    if (winningNPCGameObject.gameObject.name == "Fire Hydrant")
                    {
                        conversationDict["WRONGBAGGED"] = "I hear that Fire Hydrant grows a pair of legs when no one is looking.";
                    }
                    if (winningNPCGameObject.gameObject.name == "Chickpea Deputy")
                    {
                        conversationDict["WRONGBAGGED"] = "Does Chickpea look... sexier to you?";
                    }
                    if (winningNPCGameObject.gameObject.name == "Peanut Twins")
                    {
                        conversationDict["WRONGBAGGED"] = "I got a sneak taste of Peanut Twins new product!";
                    }
                    if (winningNPCGameObject.gameObject.name == "Birthday Cake")
                    {
                        conversationDict["WRONGBAGGED"] = "I can just chug this punch all day long.";
                    }
                    if (winningNPCGameObject.gameObject.name == "Lina Bean")
                    {
                        conversationDict["WRONGBAGGED"] = "I liked Lina better when she was shorter.";
                    }
                    if (winningNPCGameObject.gameObject.name == "GreenBen")
                    {
                        conversationDict["WRONGBAGGED"] = "*sweats profusely*";
                    }
                }
            }
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

            if (beanState == gameState.ISNOTCOOL)
            {
                if (endingsManager.endingsSeenList.Count == 0)
                {
                    conversationDict["ISNOTCOOL"] = "Hey Bean, I'm sorry you're just not looking that slick anymore.";
                }
                if (endingsManager.endingsSeenList.Count == 1)
                {
                    conversationDict["ISNOTCOOL"] = "See, if you were like me, you would be nicely packaged and fit a nice mold.";
                }
                if (endingsManager.endingsSeenList.Count == 2)
                {
                    conversationDict["ISNOTCOOL"] = "All of my friends were sausage patties growing up, and they didn't do nearly as well as I have.";
                }
                if (endingsManager.endingsSeenList.Count == 3)
                {
                    conversationDict["ISNOTCOOL"] = "The pressure of being nicely packaged and cased like me just breeds success naturally.";
                }
                if (endingsManager.endingsSeenList.Count == 4)
                {
                    conversationDict["ISNOTCOOL"] = "My ";
                }
                if (endingsManager.endingsSeenList.Count == 5)
                {
                    conversationDict["ISNOTCOOL"] = "";
                }
                if (endingsManager.endingsSeenList.Count == 6)
                {
                    conversationDict["ISNOTCOOL"] = "";
                }
                if (endingsManager.endingsSeenList.Count == 7)
                {
                    conversationDict["ISNOTCOOL"] = "";
                }
                if (endingsManager.endingsSeenList.Count >= 8)
                {
                    conversationDict["ISNOTCOOL"] = "";
                }
            }

            conversationDict["ISBAGGED"] = "I'm feeling REAL good lately.";

            if (beanState == gameState.ISBAGGED)
            {
                if (winningNPCGameObject.gameObject.name == "Slim Sausage")
                {
                    conversationDict["ISBAGGED"] = "I've got some exciting news coming soon!";
                }
                if (winningNPCGameObject.gameObject.name == "Fire Hydrant")
                {
                    conversationDict["ISBAGGED"] = "It's been a slow day today. I didn't get to record at all.";
                }
                if (winningNPCGameObject.gameObject.name == "Chickpea Deputy")
                {
                    conversationDict["ISBAGGED"] = "Do these glasses make me look slimmer?";
                }
                if (winningNPCGameObject.gameObject.name == "Peanut Twins")
                {
                    conversationDict["ISBAGGED"] = "Just a little style tip, what if you wore a cardboard box instead of a bag?";
                }
                if (winningNPCGameObject.gameObject.name == "Birthday Cake")
                {
                    conversationDict["ISBAGGED"] = "I just wrote the sickest version of the happy birthday song. I think everyone is really going to dig it.";
                }
                if (winningNPCGameObject.gameObject.name == "Lina Bean")
                {
                    conversationDict["ISBAGGED"] = "You should put Lina Beans face on that bag. It would make you look more attractive AND it wouldn't be creepy at all!";
                }
                if (winningNPCGameObject.gameObject.name == "GreenBen")
                {
                    conversationDict["ISBAGGED"] = "I'm thinking of making a really sad album next. Would you be interested in being on the front cover?";
                }
            }

            conversationDict["BEANGOHINT"] = "I'm rooting for you at Beango tonight my main man Bean Man!";
            conversationDict["WRONGGUESS"] = "Not cool Bean. You know I wear these glasses sometimes.";

            if (beanState == gameState.WRONGBAGGED || beanState == gameState.ISBAGGED)
            {
                if (wrongNPCGameObject.gameObject.name == gameObjectName)
                {
                    conversationDict["WRONGBAGGED"] = "Not cool Bean.You know I wear these glasses sometimes.";
                }
                else
                {
                    if (winningNPCGameObject.gameObject.name == "Slim Sausage")
                    {
                        conversationDict["WRONGBAGGED"] = "Oh, these glasses?";
                    }
                    if (winningNPCGameObject.gameObject.name == "Fire Hydrant")
                    {
                        conversationDict["WRONGBAGGED"] = "I think Fire Hydrant might be allergic to Beans";
                    }
                    if (winningNPCGameObject.gameObject.name == "Chickpea Deputy")
                    {
                        conversationDict["WRONGBAGGED"] = "I wouldn't mess with the Deputy as of late. I'm not vibing with his style.";
                    }
                    if (winningNPCGameObject.gameObject.name == "Peanut Twins")
                    {
                        conversationDict["WRONGBAGGED"] = "I'm a little upset that Peanut Twins didn't ask me to help with their new product.";
                    }
                    if (winningNPCGameObject.gameObject.name == "Birthday Cake")
                    {
                        conversationDict["WRONGBAGGED"] = "What's this about Birthday Cake's club? Maybe they'll let me perform at the next meeting.";
                    }
                    if (winningNPCGameObject.gameObject.name == "Lina Bean")
                    {
                        conversationDict["WRONGBAGGED"] = "Does it bother you that Lina is taller than you Bean?";
                    }
                    if (winningNPCGameObject.gameObject.name == "GreenBen")
                    {
                        conversationDict["WRONGBAGGED"] = "I've been listening to GreenBens Sound Cloud. His voice is kind of fire.";
                    }
                }
            }
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

            if (winningNPC.gameObject.name == "Slim Sausage" || winningNPC.gameObject.name == "GreenBen")
            {
                if (count == 0)
                {
                    Debug.Log("This is the FIRST winner " + winningNPC);
                    newWinnerFound = true;
                    break;
                }
            }
            bool hasEnding = false;
            hasEnding = false;

            Debug.Log("This is the current trying winning NPC " + winningNPC.gameObject.name);
            if (count > 0)
            {
                foreach (GameObject ending in endingsManager.endingsSeenList)
                {

                    if (ending.gameObject.name.Contains(winningNPC.gameObject.name))
                    {
                        Debug.Log("There was a match in endings for " + winningNPC.gameObject.name);
                        hasEnding = true;
                        break;
                    }
                }
                if (hasEnding == false)
                {
                    Debug.Log("This is the winner " + winningNPC.gameObject.name);
                    newWinnerFound = true;
                    break;
                }
            }
        }
        winningNPC.spriteRenderer.sprite = winningNPC.glassesList[0];
        winningNPC.isWinner = true;
        copyNPC.Remove(winningNPC);

        winningNPCGameObject = winningNPC.gameObject;

        NPC peanutTwinsNPC = peanutTwins.gameObject.GetComponent<NPC>();

        if (winningNPC.gameObject.name == "Peanut Twins")
        {
            Debug.Log("PEANUT TWIN ENDING");
            peanutTwinsNPC.glasses.SetActive(true);
            butter.KillButter();
            peanutTwins.KillPeanutTwin();
        }
        else
        {
            if (!endingsManager.endingsSeenList.Contains(_actManager.PeanutTwinEnding))
            {
                peanutTwinsNPC.glasses.SetActive(true);
                Debug.Log("PEANUT TWIN LIVES");
                peanutTwins.PeanutTwinLives();
            }
            else {
                peanutTwinsNPC.glasses.SetActive(true);
                butter.KillButter();
                peanutTwins.KillPeanutTwin();
            }
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
        ExclamationPointSet();
    }

    public void ExclamationPointSet()
    {
        foreach (NPC NPC in everyNPCList)
        {
            NPC.SetExclamation();
        }
    }

    public void IsNotCool() {
        if (endingsManager.endingsSeenList.Count >= 3)
        {
            PaidActor.gameObject.name = "Dead Guy";
            if (endingsManager.endingsSeenList.Count >= 4)
            {
                PaidActor.gameObject.name = "Paid Actor";
            }
            PaidActor.SetActive(true);
        }


        foreach (HouseStateLogic house in listHouses)
        {
            house.NotCoolState();
        }
        beanState = gameState.ISNOTCOOL;

        RandomizeGlasses();
        stick.ResetStick();
        blockCheeseLogic.UpdateTrashSequence(false);

        Debug.Log("CHEESE PIZZA IS NOT COOL");

        _playerController.MoveToStart();


        foreach (NPC character in _playerController.scriptNPCList)
        {
            if (character.gameObject.name == "Granny Smith")
            {
                character.AddExclamation();
            }
            character.ShowGlasses();
        }
        foreach (NPC character in trafficConeNPCs)
        {
            character.RemoveTrafficCone();
        }
        _playerController.NoGlasses();

        if (endingsManager.endingsSeenList.Count == 2 || endingsManager.endingsSeenList.Count >= 6)
        {
            censorBar.UpdateToRedCensorBar();
        }
        else if (endingsManager.endingsSeenList.Count == 3)
        {
            censorBar.UpdateToRainbowCensorBar();
        }
        else
        {
            censorBar.UpdateToBlackCensorBar();
        }
        //BEAN MAN SPRITE CHANGE
    }

    public void IsBagged()
    {
        PaidActor.SetActive(false);
        Debug.Log("CHEESE PIZZA IS BAGGED");
        foreach (HouseStateLogic house in listHouses) {
            house.BaggedState();
        }

        beanState = gameState.ISBAGGED;
        _actManager.IsNotCoolMusicEvent.setParameterByName("Bagged Transition", 1);
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
        PaidActor.SetActive(false);
        _playerController._responseBox.isActive = false;
        _playerController._dialogueBox.isActive = false;
        _playerController._canTalkBox.isActive = false;
        _playerController.ResetBeanSpawnPosition();
        uILogic.UpdateEndingsCount(endingsManager.endingsSeenList.Count);
        foreach (HouseStateLogic house in listHouses)
        {
            house.CoolState();
        }

        if (_playerController.scriptNPCList.Count > 1)
        {
            foreach (NPC npc in _playerController.scriptNPCList)
            {
                npc.ResetCoolStateNPC();
            }
        }

        if (_playerController.scriptNPCList.Count > 1) {
            foreach (NPC npc in trafficConeNPCs)
            {
                npc.ResetCoolStateNPC();
                if (endingsManager.endingsSeenList.Contains(_actManager.BirthdayCakeEnding))
                {
                    if (npc.gameObject.tag != "InactiveNPC" && npc.gameObject.name != "Traffic Cone")
                    {
                        Debug.Log("Traffic Cone activating");
                        trafficCone.gameObject.tag = "SideNPC";
                        npc.ShowTrafficCone();
                    }
                    if (npc.gameObject.name == "Traffic Cone")
                    {
                        npc.RemoveTrafficCone();
                    }
                    if (endingsManager.endingsSeenList.Count > 7 && npc.gameObject.name == "Cory")
                    {
                        npc.RemoveTrafficCone();
                    }
                    if (endingsManager.endingsSeenList.Contains(_actManager.PeanutTwinEnding) && npc.gameObject.name == "Butter")
                    {
                        npc.RemoveTrafficCone();
                    }
                }
            }
        }

        _playerController.Glasses();
        _playerController.Bag.transform.position = _playerController.m_BagStartPosition.transform.position;
        _playerController.finishedBagMove = false;
        _playerController.bagMoving = true;
        _playerController.MoveToStart();

        _lemonadeStand.SetLemonadeStandSanity();
        _fireHydrantvomit.hasVommittedThisRound = false;
        if (_playerController.scriptNPCList.Count > 1)
        {

            if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.FireHydrantEnding)
            {
                _trafficConeCollection.FireHydrantPile();
            }
            else
            {
                _trafficConeCollection.TrafficConePile();
            }
            if (endingsManager.endingsSeenList.Contains(_actManager.BirthdayCakeEnding))
            {
                trafficCone.gameObject.tag = "SideNPC";
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
                peanutTwins.PeanutTwinLives();
                butter.ButterLives();
            }
        }
        if (endingsManager.endingsSeenList.Contains(_actManager.PeanutTwinEnding) || endingsManager.endingsSeenList.Contains(_actManager.ChickPeaEnding))
        {

            dynamicMusic.ChangeLastWinnerMusic(winningNPCGameObject);
        }
        else
        {
            dynamicMusic.ChangeLastWinnerMusic(null);
        }

        if (endingsManager.endingsSeenList.Count >= 8)
        {
            lastEndingConesMove.MoveTrafficConesToEnd();
        }
        else
        {
            lastEndingConesMove.MoveTrafficConesToStart();
        }

        if (endingsManager.endingsSeenList.Contains(_actManager.GreenBenEnding))
        {
            greenBen.GreenBenHealthy();
        }
        else
        {
            greenBen.GreenBenSick();
        }

        if (endingsManager.endingsSeenList.Count > 7)
        {
            coryLogic.CoryDead();
        }
        else
        {
            coryLogic.CoryLives();
        }

        if (endingsManager.endingsSeenList.Contains(_actManager.ChickPeaEnding))
        {
            houseDestroy.DestroyHouses();
            ChickpeaDebris.SetActive(true);
        }
        else
        {
            houseDestroy.FixHouses();
            ChickpeaDebris.SetActive(false);
        }

        if (endingsManager.endingsSeenList.Contains(_actManager.LinaBeanEnding))
        {
            LinaBean.gameObject.name = "Edamame Bean";
        }

        if (endingsManager.endingsSeenList.Count >= 7)
        {
            BirthdayCake.gameObject.name = "Party Cake";
        }

        stick.ResetStick();
        blockCheeseLogic.UpdateTrashSequence(true);
        _hideyHole.NewPeeperSet();
        poparazziCorn.ChangeLocation();
        chickPeaLogic.ChickPeaWizardMode();
        cornLady.CornLadyHibernate();

        recentEndingPlayed = true;

        _UILogic.MainMenu.SetActive(true);

        NPC winningTransition = default;

        foreach (NPC npc in transitionsList)
        {
            npc.transitionWinner = false;
        }
        foreach (NPC npc in completedTransitionsList)
        {
            npc.transitionWinner = false;
        }
        foreach (NPC npc in transitionsList)
        {
            if (endingsManager.endingsSeenList.Count == 0)
            {
                if (npc.gameObject.name == "Granny Smith")
                {
                    winningTransition = npc;
                }
            }
            else if (endingsManager.endingsSeenList.Count == 8)
            {
                break;
            }
            else
            {
                bool foundTransition = false;
                while (!foundTransition)
                {
                    NPC winningNPC;
                    int number = Random.Range(0, completedTransitionsList.Count);
                    winningNPC = transitionsList[Random.Range(0, transitionsList.Count)];
                    if (!completedTransitionsList.Contains(winningNPC))
                    {
                        Debug.Log("Winning Transition " + npc.gameObject.name);
                        winningTransition = npc;
                        foundTransition = true;
                        break;
                    }
                    else
                    {
                        winningTransition = npc;
                        foundTransition = true;
                        break;
                    }
                }
                break;
            }
        }
        if (winningTransition != null && endingsManager.endingsSeenList.Count == completedTransitionsList.Count)
        {
            completedTransitionsList.Add(winningTransition);
            transitionsList.Remove(winningTransition);
        }
        if (transitionsList.Count == 0)
        {
            NPC winningNPC;
            int number = Random.Range(0, completedTransitionsList.Count);
            winningNPC = completedTransitionsList[number];
            winningNPC.transitionWinner = true;
            Debug.Log("TRANSITION COUNT 0 " + winningNPC.gameObject.name);
        }
        else if (endingsManager.endingsSeenList.Count == 8)
        {
            
        }else
        {
            completedTransitionsList[completedTransitionsList.Count - 1].transitionWinner = true;
        }

        saveLoading.Save();
    }
}