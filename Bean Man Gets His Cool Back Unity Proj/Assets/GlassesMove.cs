using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassesMove : MonoBehaviour
{
    public Rigidbody2D rb;
    public float m_Speed = 1f;
    public float UITmp = 0;

    public float timeToFade = 0;

    public GameObject characterName;

    public GameState gameState;

    public bool hideUI = false;

    public SpriteRenderer leftSprite;
    public SpriteRenderer rightSprite;
    // Start is called before the first frame update

    public void Update()
    {
        if (gameState.beanState == GameState.gameState.WRONGBAGGED)
        {
            MoveGlasses();
        }
        else {
            ResetGlasses();
        }
    }

    public void MoveGlasses()
    {
        if (characterName.gameObject.name == gameState.winningNPCGameObject.gameObject.name)
        {
            return;
        }
        rb.velocity = transform.up * m_Speed;

        UITmp = Mathf.Lerp(1, 0, timeToFade * 1);
        
        leftSprite.color = new Color(255, 255, 255, UITmp);
        rightSprite.color = new Color(255, 255, 255, UITmp);
        
        timeToFade += Time.deltaTime;
    }
    public void ResetGlasses()
    {
        timeToFade = 0;
        UITmp = 0;

        leftSprite.color = new Color(255, 255, 255, 1);
        rightSprite.color = new Color(255, 255, 255, 1);

        this.transform.localPosition = new Vector3(0, 0);
    }
}
