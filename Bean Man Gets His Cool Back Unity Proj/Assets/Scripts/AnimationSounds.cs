using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSounds : MonoBehaviour
{
    public SurfaceDetector _surfaceDetector;
    public FireHydrantVomit fireHydrantVomit;
    public PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    void Playsound(string sound)
    {
        FMODUnity.RuntimeManager.PlayOneShot(sound, this.transform.position);
    }

    public void VomitEnd()
    {
        playerController.isVommiting = false;
    }

    void SetPuddleSize() {
        if (fireHydrantVomit != null) {
            fireHydrantVomit.AddVomitPuddle();
        }
    }

    void PlayFootstep(string sound)
    {
        FMODUnity.RuntimeManager.PlayOneShot(sound + " " + _surfaceDetector.surfaceName, this.transform.position);

        if (_surfaceDetector.isWet)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Footsteps Wet", this.transform.position);
        }
    }
}
