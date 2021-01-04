using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideyHole : MonoBehaviour
{
    public PlayerController _playerController;

    public EndingsManager _endingsManager;

    public GameObject cover;
    public GameObject peeper;
    public NonNPC npc;

    public List<GameObject> PeeperList = new List<GameObject>();

    public List<GameObject> CompletePeeperList = new List<GameObject>();

    public bool beanInRange = false;

    public void NewPeeperSet() {
        if (PeeperList.Count == CompletePeeperList.Count)
        {
            peeper = CompletePeeperList[Random.Range(0, CompletePeeperList.Count)];
            return;
        }

        if (_endingsManager.endingsSeenList.Count > 1 && PeeperList.Count != CompletePeeperList.Count)
        {
            peeper.SetActive(false);
            bool findingPeeper = true;
            while (findingPeeper == true)
            {
                peeper = CompletePeeperList[Random.Range(0, CompletePeeperList.Count)];
                Debug.Log("Here is the peeper we're trying " + peeper);
                if (!PeeperList.Contains(peeper))
                {
                    Debug.Log("Peeper Found");
                    findingPeeper = false;
                    break;
                }
            }
            Debug.Log("Adding Peeper to List");
            PeeperList.Add(peeper);
            peeper.SetActive(true);
        }
        Debug.Log("Here is the endings count " + _endingsManager.endingsSeenList.Count + " and the Peeper List Count " + PeeperList.Count);
    }
    // Update is called once per frame
    void Update()
    {
        if (_endingsManager.endingsSeenList.Count == 0) {
            cover.SetActive(true);
            peeper.SetActive(false);
            return;
        }

        if (beanInRange == true) {
            cover.SetActive(true);
            peeper.SetActive(false);
            return;
        }

        if (npc.isLeft && _playerController.spriteFlip == false)
        {
            cover.SetActive(false);
            peeper.SetActive(true);
        }
        if (npc.isLeft && _playerController.spriteFlip == true)
        {
            cover.SetActive(true);
            peeper.SetActive(false);
        }
        if (!npc.isLeft && _playerController.spriteFlip == true)
        {
            cover.SetActive(false);
            peeper.SetActive(true);
        }
        if (!npc.isLeft && _playerController.spriteFlip == false)
        {
            cover.SetActive(true);
            peeper.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        beanInRange = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        beanInRange = false;
    }
}
