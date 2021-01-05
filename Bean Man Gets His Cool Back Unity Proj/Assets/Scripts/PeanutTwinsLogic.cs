using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeanutTwinsLogic : MonoBehaviour
{
    public Animator PeanutTwinsGlasses;

    public void KillPeanutTwin() {
        PeanutTwinsGlasses.Play("PTNoGlasses");
    }
    public void PeanutTwinLives() {
        PeanutTwinsGlasses.Play("PTGlasses");
    }
}
