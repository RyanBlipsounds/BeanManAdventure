using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHydrantVomit : MonoBehaviour
{
    public Animator FireHydrantAnimator;

    public PlayerController playerController;
    public GameState gameState;

    public bool fireHydrantActivated = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if (playerController.thisCharacter.gameObject.name == "Fire Hydrant" && gameState.beanState == GameState.gameState.ISNOTCOOL && playerController.isVommiting == true) {
            FireHydrantAnimator.Play("FireHydrantVomit");
        }
    }

    public void VomitEnd() {
        playerController.isVommiting = false;
    }
}
