using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHydrantVomit : MonoBehaviour
{
    public Animator FireHydrantAnimator;

    public PlayerController playerController;
    public GameState gameState;

    public SpriteRenderer map;

    public Sprite vomitMap;

    public List<GameObject> vomitList = new List<GameObject>();
    public GameObject currentVomit;
    public int vomitCount = 0;

    public bool fireHydrantActivated = false;
    public bool hasVommittedThisRound = false;

    public GameObject VomitColliders;

    public QuestList questList;
    public VomitScreen vomitScreen;

    public void Start()
    {
        //THE VERY FIRST VOMIT (0), SHOULD HAVE NO SPRITE ATTACHED
        if (vomitCount < vomitList.Count)
        {
            currentVomit = vomitList[vomitCount];
        }
        else
        {
            currentVomit = vomitList[vomitList.Count - 1];
        }
    }
    public void AddVomitPuddle()
    {
        SetVomitCount();
        hasVommittedThisRound = true;
        //VomitMap();
        if (vomitCount >= vomitList.Count)
        {
            vomitScreen.gameObject.SetActive(true);
        }
    }

    public void VomitMap()
    {
        if (map.sprite == vomitMap) {
            return;
        }

        if (vomitCount >= vomitList.Count) {
            questList.CompleteQuestItem("Soak the Town in Vomit");
            questList.ActivateQuestItem("Lose to Fire");
            map.sprite = vomitMap;
            VomitColliders.SetActive(true);
        }
    }

    public void SetVomitCount()
    {
        vomitCount++;
        currentVomit.SetActive(false);
        if (vomitCount < vomitList.Count)
        {
            currentVomit = vomitList[vomitCount];
        }
        else {
            currentVomit = vomitList[vomitList.Count - 1];
        }
        currentVomit.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.thisCharacter != null) {
            if (playerController.thisCharacter.gameObject.name == "Fire Hydrant" && gameState.beanState == GameState.gameState.ISNOTCOOL && playerController.isVommiting == true)
            {
                Debug.Log("Start vomit");
                FireHydrantAnimator.Play("FireHydrantVomit");
                questList.CompleteQuestItem("Fire Hydrant Vomit");
                questList.ActivateQuestItem("Soak the Town in Vomit");

            }
        }
    }
}
