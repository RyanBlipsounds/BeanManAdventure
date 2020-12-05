using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingsManager : MonoBehaviour
{
    public List<NPC> endingsSeenList = new List<NPC>();
    public UILogic _UILogic = default;
    public void addEnding(NPC npc)
    {
        endingsSeenList.Add(npc);
        _UILogic.updateEndingsCount(endingsSeenList.Count);
    }
}
