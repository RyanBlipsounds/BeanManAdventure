using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActManager : MonoBehaviour
{
    public bool fadeToBlack;
    public SpriteRenderer BlackScreen;
    public Color ColorValue;

    public GameState m_gameState;

    public float timeToFade = 0;


    void Start()
    {
        BlackScreen = GameObject.Find("BlackScreen").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        LoadNewAct();

        if (m_gameState.beanState == GameState.gameState.IsCool)
        {
            Debug.Log("BEEAN");
        }
        else if (m_gameState.beanState == GameState.gameState.IsNotCool)
        {
            Debug.Log("MEN");
        }
        else if (m_gameState.beanState == GameState.gameState.IsBagged)
        {
            Debug.Log("UNITE!!");
        }
    }

    //In Load New Act, it should probably take dictionary arguments 
    public void LoadNewAct()
    {
        float tmp;

        timeToFade += Time.deltaTime;

        tmp = Mathf.Lerp(1, 0, timeToFade / 2);

        Debug.Log(tmp);

        BlackScreen.color = new Color(0, 0, 0, tmp);

        if (tmp == 0) {

        }
    }

}
