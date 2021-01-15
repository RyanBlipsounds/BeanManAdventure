using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExclamationPoint : MonoBehaviour
{
    public float UITmp = 0;

    public float timeToFade = 0;
    public bool hideUI = false;

    public SpriteRenderer sprite;
    // Start is called before the first frame update

    public void Update()
    {
        if (hideUI)
        {
            UITmp = Mathf.Lerp(1, 0, timeToFade * 8);

            sprite.color = new Color(255, 255, 255, UITmp);

            timeToFade += Time.deltaTime;
        }
        if (UITmp == 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void OnEnable()
    {
        sprite.color = new Color(255, 255, 255, 1);
        timeToFade = 0;
        hideUI = false;
        UITmp = 1;
    }


}
