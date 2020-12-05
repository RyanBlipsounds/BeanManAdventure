using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingsManager : MonoBehaviour
{
    public List<GameObject> endingsSeenList = new List<GameObject>();
    public UILogic _UILogic = default;
    public void addEnding(GameObject ending)
    {
        endingsSeenList.Add(ending);
        _UILogic.UpdateEndingsCount(endingsSeenList.Count);
    }
}
