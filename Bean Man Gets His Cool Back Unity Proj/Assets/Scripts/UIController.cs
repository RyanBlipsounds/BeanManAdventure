using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject StartPoint;
    public GameObject EndPoint;
    public float speed = 1.0f;

    public GameObject ResponseBox;
    public GameObject DialogueBox;
    public GameObject CanTalkBox;

    public bool isActive = false;
    public bool hasMoved = false;

    // Start is called before the first frame update
    void Start()
    {

        transform.position = StartPoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            transform.position = Vector2.MoveTowards(transform.position, EndPoint.transform.position, Time.deltaTime * speed);
        }
        else {
            transform.position = Vector2.MoveTowards(transform.position, StartPoint.transform.position, Time.deltaTime * speed);
        }
    }
}