using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoryLogic : MonoBehaviour
{

    public GameObject coryDead;
    public GameObject coryAlive;

    public Animator animator;
    // Start is called before the first frame update
    
    public void CoryDead()
    {

        this.gameObject.tag = "InactiveNPC";
        coryDead.SetActive(true);
        coryAlive.SetActive(false);
    }


    public void CoryLives()
    {
        this.gameObject.tag = "SideNPC";
        coryDead.SetActive(false);
        coryAlive.SetActive(true);
    }
}
