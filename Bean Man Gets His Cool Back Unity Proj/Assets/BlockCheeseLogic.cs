using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCheeseLogic : MonoBehaviour
{
    public List<GameObject> TrashSequence = new List<GameObject>();
    public EndingsManager endingsManager;
    public GameState gameState;

    public GameObject Raccoon;

    public void UpdateTrashSequence(bool isResetGame)
    {
        int count = 0;
        if (isResetGame)
        {
            count = 1;
        }
        else
        {
            count = 0;
        }

        if (endingsManager.endingsSeenList.Count >= count + 1)
        {
            TrashSequence[0].SetActive(true);
        }
        else
        {
            TrashSequence[0].SetActive(false);
        }

        if (endingsManager.endingsSeenList.Count >= count + 3)
        {
            TrashSequence[1].SetActive(true);
        }
        else
        {
            TrashSequence[1].SetActive(false);
        }


        if (endingsManager.endingsSeenList.Count >= count + 5)
        {
            TrashSequence[2].SetActive(true);
        }
        else
        {
            TrashSequence[2].SetActive(false);
        }

        if (endingsManager.endingsSeenList.Count >= count + 6)
        {
            TrashSequence[3].SetActive(true);
        }
        else
        {
            TrashSequence[3].SetActive(false);
        }
        if (endingsManager.endingsSeenList.Count == 6 && gameState.beanState == GameState.gameState.ISNOTCOOL)
        {
            Raccoon.SetActive(true);
        }
        else
        {
            Raccoon.SetActive(false);
        }

        if (endingsManager.endingsSeenList.Count >= count + 7)
        {
            TrashSequence[4].SetActive(true);
        }
        else
        {
            TrashSequence[4].SetActive(false);
        }

    }
}
