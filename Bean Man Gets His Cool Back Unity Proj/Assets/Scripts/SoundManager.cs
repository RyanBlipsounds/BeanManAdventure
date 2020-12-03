using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour

{
    FMOD.Studio.EventInstance ambience;

    FMOD.Studio.EventInstance music;

    // Start is called before the first frame update
    void Start()
    {
        //FMODUnity.RuntimeManager.PlayOneShot("event:/Test Guy"); 

        ambience = FMODUnity.RuntimeManager.CreateInstance("event:/Wind Ambience");
        ambience.start();

        music = FMODUnity.RuntimeManager.CreateInstance("event:/Music");
        music.start();

  
    }

    // Update is called once per frame
    void Update()
    {
        //playerState.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);


    }
}
