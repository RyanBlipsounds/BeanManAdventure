using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterLogic : MonoBehaviour
{
    public GameObject livingButter;
    public GameObject deadButter;
    public GameObject parentButter;

    public bool melted = false;

    private void Start()
    {
        livingButter.SetActive(true);
        deadButter.SetActive(false);
    }

    public void KillButter() {
        this.gameObject.tag = "InactiveNPC";
        melted = true; 
        livingButter.SetActive(false);
        deadButter.SetActive(true);
    }
    public void ButterLives()
    {
        this.gameObject.tag = "SideNPC";
        melted = false;
        livingButter.SetActive(true);
        deadButter.SetActive(false);
    }
}