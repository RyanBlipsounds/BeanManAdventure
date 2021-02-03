using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicMusic : MonoBehaviour
{
    public GameObject CharacterProximity;
    public GameObject BirthdayCake;
    public float distanceToCake;
    public GameState gameState;
    public float distanceMult = 1f;

    public PlayerController playerController;

    
    // Start is called before the first frame update
    void Start()
    {
        distanceToCake = 1;
    }

    public void ChangeWinnerMusic()
    {
        CharacterProximity = gameState.winningNPCGameObject;
    }

    public void ChangeLastWinnerMusic(GameObject winner)
    {
        if (winner == null)
        {
            CharacterProximity = BirthdayCake;
        }
        else
        {
            CharacterProximity = winner;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState.beanState == GameState.gameState.WRONGBAGGED)
        {
            distanceMult = 0.14f;
        }
        else
        {
            distanceMult = 0.54f;
        }

        distanceToCake = Vector3.Distance(this.gameObject.transform.position, CharacterProximity.gameObject.transform.position) * distanceMult;

        if (distanceToCake <= 3)
        {
            playerController.WrongMusicEvent.setParameterByName("BirthdayCakeProx", distanceToCake);
            playerController.WrongBeatEvent.setParameterByName("BirthdayCakeProx", distanceToCake);
            gameState.StartMusic.setParameterByName("BirthdayCakeProx", distanceToCake);
        }
        else {
            playerController.WrongMusicEvent.setParameterByName("BirthdayCakeProx", distanceToCake);
            playerController.WrongBeatEvent.setParameterByName("BirthdayCakeProx", distanceToCake);
            gameState.StartMusic.setParameterByName("BirthdayCakeProx", 1);
        }

    }
}
