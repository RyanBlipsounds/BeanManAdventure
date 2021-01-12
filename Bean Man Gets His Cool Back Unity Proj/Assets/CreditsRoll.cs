using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsRoll : MonoBehaviour
{
    public Rigidbody2D rb;
    public float m_Speed;

    public SpriteRenderer CreditsSprite;
    public float timeToFade = 0;
    public float creditsTmp = 0;

    public GameObject creditsStartPosition;
    // Start is called before the first frame update
    private void Update()
    {
        rb.velocity = transform.up * m_Speed;


        creditsTmp = Mathf.Lerp(0, 1, timeToFade / 4);

        CreditsSprite.color = new Color(255, 255, 255, creditsTmp);

        timeToFade += Time.deltaTime;

    }

    void OnDisable()
    {
        this.gameObject.transform.position = creditsStartPosition.transform.position;
    }

    // Update is called once per frame
    void OnEnable()
    {
        creditsTmp = 0;
        timeToFade = 0;
        this.gameObject.transform.position = creditsStartPosition.transform.position;
    }
}
