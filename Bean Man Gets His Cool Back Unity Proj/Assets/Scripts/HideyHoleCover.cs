using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideyHoleCover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Manhole Open", this.transform.position);
    }

    private void OnDisable()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Manhole Close", this.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
