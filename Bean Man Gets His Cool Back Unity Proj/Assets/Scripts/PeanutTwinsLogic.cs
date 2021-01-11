using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeanutTwinsLogic : MonoBehaviour
{
    public Animator PeanutTwinsGlasses;
    public Animator PeanutTwinsNoGlasses;

    public void KillPeanutTwin() {
        PeanutTwinsNoGlasses.Play("PTNoGlasses");
        PeanutTwinsGlasses.Play("PTNoGlasses");
    }
    public void PeanutTwinLives() {
        PeanutTwinsNoGlasses.Play("PTGlasses");
        PeanutTwinsGlasses.Play("PTGlasses");
    }
}
