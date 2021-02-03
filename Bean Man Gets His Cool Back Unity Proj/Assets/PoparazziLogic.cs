using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoparazziLogic : MonoBehaviour
{
    public EndingsManager endingsManager;
    public ActManager _actManager;

    public GameObject BeanManPosition;
    public GameObject LinaPosition;
    public GameObject ChickpeaPosition;
    public GameObject FireHydrantPosition;
    public GameObject GreenBenPosition;
    public GameObject BirthdayCakePosition;
    public GameObject GrannySmithPosition;
    public GameObject SlimSausagePosition;
    public GameObject PeanutTwinsPosition;

    public List<SpriteRenderer> PoparazziList = new List<SpriteRenderer>();

    public void Start()
    {
        ChangeLocation();
    }

    public void TurnPopCorn(bool turnLeft) {
        foreach (SpriteRenderer corn in PoparazziList)
        {
            corn.flipX = turnLeft;
        }
    }

    public void ChangeLocation()
    {
        if(endingsManager.endingsSeenList.Count == 0)
        {
            TurnPopCorn(false);
            this.transform.position = BeanManPosition.transform.position;
        }

        if (endingsManager.endingsSeenList.Count > 0)
        {
            if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BeanManUncoolEnding ||
                endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BeanManLeavesTown ||
                endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BeanManLeavesBagged)
            {
                TurnPopCorn(false);
                this.transform.position = BeanManPosition.transform.position;
            }
            if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.LinaBeanEnding)
            {
                TurnPopCorn(false);
                this.transform.position = LinaPosition.transform.position;
            }
            if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BeanManWinEnding)
            {
                TurnPopCorn(false);
                this.transform.position = BeanManPosition.transform.position;
            }
            if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.ChickPeaEnding)
            {
                TurnPopCorn(true);
                this.transform.position = ChickpeaPosition.transform.position;
            }
            if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.FireHydrantEnding)
            {
                TurnPopCorn(true);
                this.transform.position = FireHydrantPosition.transform.position;
            }
            if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.GreenBenEnding)
            {
                TurnPopCorn(true);
                this.transform.position = GreenBenPosition.transform.position;
            }
            if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.BirthdayCakeEnding)
            {
                TurnPopCorn(false);
                this.transform.position = BirthdayCakePosition.transform.position;
            }
            if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.GrannySmithEnding)
            {
                TurnPopCorn(true);
                this.transform.position = GrannySmithPosition.transform.position;
            }
            if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.SlimSausageWinning)
            {
                TurnPopCorn(true);
                this.transform.position = SlimSausagePosition.transform.position;
            }
            if (endingsManager.endingsSeenList[endingsManager.endingsSeenList.Count - 1] == _actManager.PeanutTwinEnding)
            {
                TurnPopCorn(true);
                this.transform.position = PeanutTwinsPosition.transform.position;
            }
        }
    }
}
