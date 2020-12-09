using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickLogic : MonoBehaviour
{
    public PlayerController _playerController;
    public Animator StickAnimator;
    public GameState _gameState;

    public UIController _canTalkBox;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameState.beanState == GameState.gameState.ISNOTCOOL && Input.GetKeyDown(KeyCode.Space) && _playerController.thisCharacter.gameObject.name == "Stick") {
            StickAnimator.Play("StickRunAway");
        }
    }
}
