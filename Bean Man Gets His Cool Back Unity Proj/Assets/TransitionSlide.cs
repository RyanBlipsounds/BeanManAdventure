using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionSlide : MonoBehaviour
{
    public float UITmp = 0;
    public float timeToFade = 0;
    public SpriteRenderer sprite;
    public float multiplier = 7;

    public float startNumber = 0;
    public float endNumber = 1;

    public bool fadeIn = true;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (UITmp == 1)
        {
            startNumber = 1;
            endNumber = 0;
            fadeIn = false;
        }
        else if (UITmp == 0 && fadeIn == false)
        {
            this.gameObject.SetActive(false);
        }

        if (fadeIn)
        {
            timeToFade += Time.deltaTime;
        }
        else
        {
            timeToFade -= Time.deltaTime;
        }

        UITmp = Mathf.Lerp(0, 1, timeToFade * multiplier);
        sprite.color = new Color(255, 255, 255, UITmp);
        
    }

    public void OnEnable()
    {
        fadeIn = true;
        startNumber = 0;
        endNumber = 1;
        sprite.color = new Color(255, 255, 255, 0);
        timeToFade = 0;
        UITmp = 0;
    }
}
