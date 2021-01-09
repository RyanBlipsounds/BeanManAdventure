using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemonadeStand : MonoBehaviour
{
    public List<GameObject> LemonadeStandSanity = new List<GameObject>();

    public EndingsManager _endingsManager;

    // Start is called before the first frame update
    public void SetLemonadeStandSanity() {
        Debug.Log(_endingsManager.endingsSeenList.Count + "  +  " + LemonadeStandSanity.Count);
        if (_endingsManager.endingsSeenList.Count >= LemonadeStandSanity.Count)
        {
            LemonadeStandSanity[LemonadeStandSanity.Count - 2].SetActive(false);
            LemonadeStandSanity[LemonadeStandSanity.Count -1].SetActive(true);
            return;
        }
        if (_endingsManager.endingsSeenList.Count > 0)
        {
            LemonadeStandSanity[_endingsManager.endingsSeenList.Count].SetActive(true);
            LemonadeStandSanity[_endingsManager.endingsSeenList.Count - 1].SetActive(false);
        }
    }

    void Start()
    {
        LemonadeStandSanity[0].SetActive(true);
    }
}
