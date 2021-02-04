using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionActor : MonoBehaviour
{
    public float timer;
    public List<TransitionSlide> slide = new List<TransitionSlide>();
    public List<float> transitionTimes = new List<float>();

    public int count = 0;
    // Start is called before the first frame update
    void OnEnable()
    {
        timer = 0;
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (count < slide.Count)
        {
            timer += Time.deltaTime;
        }
        else
        {
            count = 0;
        }

        if (timer > transitionTimes[count])
        {
            slide[count].gameObject.SetActive(true);
            count++;
        }
    }
}
