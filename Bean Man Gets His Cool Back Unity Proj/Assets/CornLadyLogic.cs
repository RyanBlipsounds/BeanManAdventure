using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornLadyLogic : MonoBehaviour
{

    public GameObject CornLadyAwake;
    public GameObject CornLadyAsleep;
    public GameObject trafficCone;

    public EndingsManager endingsManager;

    private void Start()
    {
        if (endingsManager.endingsSeenList.Count == 0)
        {
            CornLadyAwake.SetActive(true);
            CornLadyAsleep.SetActive(false);
            this.gameObject.tag = "SideNPC";
        }
    }

    public void CornLadyHibernate()
    {
        if (endingsManager.endingsSeenList.Count > 0)
        {
            CornLadyAwake.SetActive(false);
            CornLadyAsleep.SetActive(true);
            this.gameObject.tag = "InactiveNPC";
            trafficCone.SetActive(false);
        }
    }
}
