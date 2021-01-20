using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicMusic : MonoBehaviour
{
    public GameObject CharacterProximity;
    public float distanceToCake;
    public GameState gameState;
    public float distanceMult = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        distanceToCake = 1;
    }

    public void ChangeWinnerMusic()
    {
        //Maybe change the gameobject it's tied to based on the winner?
    }

    // Update is called once per frame
    void Update()
    {
        distanceToCake = Vector3.Distance(this.gameObject.transform.position, CharacterProximity.gameObject.transform.position) * distanceMult;

        if (distanceToCake <= 3)
        {
            gameState.StartMusic.setParameterByName("BirthdayCakeProx", distanceToCake);
        }
        else {
            gameState.StartMusic.setParameterByName("BirthdayCakeProx", 1);
        }
    }
}
