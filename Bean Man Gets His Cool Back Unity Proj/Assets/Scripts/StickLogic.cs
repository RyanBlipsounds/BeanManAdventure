using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickLogic : MonoBehaviour
{
    public PlayerController _playerController;
    public Animator StickAnimator;
    public GameState _gameState;

    public List<GameObject> allObjects = new List<GameObject>();

    public GameObject thisSprite;
    public GameObject startPosition;
    public QuestList questList;

    public EndingsManager _endingsManager;
    public UIController _canTalkBox;

    public void ResetStick()
    {
        if (_endingsManager.endingsSeenList.Count > allObjects.Count)
        {
            return;
        }
        if (_endingsManager.endingsSeenList.Count > 0 && _gameState.beanState == GameState.gameState.ISNOTCOOL)
        {
            foreach(GameObject objects in allObjects){
                objects.tag = "InactiveNPC";
                objects.SetActive(false);
                objects.transform.position = startPosition.transform.position;
            }
            allObjects[_endingsManager.endingsSeenList.Count].SetActive(true);
            allObjects[_endingsManager.endingsSeenList.Count].gameObject.tag = "SideNPC";
            StickAnimator = allObjects[_endingsManager.endingsSeenList.Count].GetComponent<Animator>();
        }
        else
        {
            foreach (GameObject objects in allObjects)
            {
                objects.tag = "InactiveNPC";
                objects.SetActive(false);
                objects.transform.position = startPosition.transform.position;
            }
            allObjects[0].SetActive(true);
            StickAnimator = allObjects[_endingsManager.endingsSeenList.Count].GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameState.beanState != GameState.gameState.ISNOTCOOL)
        {
            allObjects[_endingsManager.endingsSeenList.Count].gameObject.tag = "InactiveNPC";
            return;
        }
        else {
            allObjects[_endingsManager.endingsSeenList.Count].gameObject.tag = "SideNPC";
        }
        if (_gameState.beanState == GameState.gameState.ISNOTCOOL && Input.GetKeyDown(KeyCode.Space) && 
            _playerController.thisCharacter.gameObject.name == allObjects[_endingsManager.endingsSeenList.Count].gameObject.name) {
            StickAnimator.Play("StickRunAway");
        }
    }
}