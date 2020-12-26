using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickPeaLogic : MonoBehaviour
{
    public Animator ChickpeaNoGlasses;
    public Animator ChickpeaGlasses;

    public EndingsManager endingsManager;
    public ActManager actManager;

    public void ChickPeaWizardMode()
    {
        if (endingsManager.endingsSeenList.Contains(actManager.ChickPeaEnding))
        {
            ChickpeaNoGlasses.Play("WizardChickNoGlasses");
            ChickpeaGlasses.Play("Wizard Anim");
        }
    }
}
