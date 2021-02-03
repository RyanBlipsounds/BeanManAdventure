using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestNotification : MonoBehaviour
{
    public GameObject StartPoint;
    public GameObject EndPoint;
    public float speed = 1.0f;

    public float goAwayTimer = 5f;

    public bool isActive = false;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {

        if (isActive)
        {
            this.gameObject.transform.position = Vector3.MoveTowards(transform.position, EndPoint.transform.position, Time.deltaTime * speed);
            goAwayTimer -= Time.deltaTime;
            if (goAwayTimer < 0) {
                //isActive = false;
            }
        }
        else
        {
            goAwayTimer = 5.0f;
            this.gameObject.transform.position = Vector3.MoveTowards(transform.position, StartPoint.transform.position, Time.deltaTime * speed);
        }
    }
}