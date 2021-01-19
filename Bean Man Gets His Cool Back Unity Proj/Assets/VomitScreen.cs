using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VomitScreen : MonoBehaviour
{
    public Rigidbody2D rb;
    public float m_Speed;

    public SpriteRenderer VomitSprite;
    public SpriteRenderer BlackScreenSprite;

    public FireHydrantVomit vomitMap;

    public float timePassed = 0;

    public float timeToFade = 0;
    public float vomitTmp = 0;
    public float blackScreenTmp = 0;

    public PlayerController playerController;

    public bool deactivating = false;

    public GameObject vomitStartPosition;
    // Start is called before the first frame update
    private void Update()
    {

        timePassed += Time.deltaTime;

        rb.velocity = transform.up * m_Speed * -2;

        blackScreenTmp = Mathf.Lerp(0, 1, timeToFade / 1.5f);

        BlackScreenSprite.color = new Color(255, 255, 255, blackScreenTmp);

        if (timePassed > 5)
        {

            StopVomitTime();
        }

        if (deactivating)
        {
            vomitMap.VomitMap();
            playerController.bagMoving = false;
            timeToFade -= Time.deltaTime;
            if (timeToFade <= 0)
            {
                this.gameObject.SetActive(false);
            }

        }
        else if (!deactivating && blackScreenTmp < 1)
        {
            playerController.bagMoving = true;
            timeToFade += Time.deltaTime;
        }


    }

    void OnDisable()
    {

        BlackScreenSprite.color = new Color(255, 255, 255, 0);
        deactivating = false;
        this.gameObject.transform.position = vomitStartPosition.transform.position;
    }

    public void StopVomitTime()
    {
        VomitSprite.color = new Color(255, 255, 255, 0);
        deactivating = true;
        blackScreenTmp = 0;
    }

    // Update is called once per frame
    void OnEnable()
    {
        timePassed = 0;
        VomitSprite.color = new Color(255, 255, 255, 1);
        deactivating = false;
        blackScreenTmp = 0;
        timeToFade = 0;
        this.gameObject.transform.position = vomitStartPosition.transform.position;
    }
}
