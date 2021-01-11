using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsRoll : MonoBehaviour
{
    public Rigidbody2D rb;
    public float m_Speed;

    public GameObject creditsStartPosition;
    // Start is called before the first frame update
    private void Update()
    {
        rb.velocity = transform.up * m_Speed;
    }

    void OnDisable()
    {
        this.gameObject.transform.position = creditsStartPosition.transform.position;
    }

    // Update is called once per frame
    void OnEnable()
    {
        this.gameObject.transform.position = creditsStartPosition.transform.position;
    }
}
