using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastEndingManager : MonoBehaviour
{
    public GameObject TrafficCones;
    public Transform startPos;
    public Transform endPos;


    public void MoveTrafficConesToEnd()
    {
        TrafficCones.transform.position = endPos.position;
    }
    public void MoveTrafficConesToStart()
    {
        TrafficCones.transform.position = startPos.position;
    }
}
