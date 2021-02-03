using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExclamationPoint : MonoBehaviour
{
    public float UITmp = 0;

    public float timeToFade = 0;
    public bool hideUI = false;
    public GameObject thisCharacter;
    public GameState gameState;

    public Transform shortLina;
    public Transform tallLina;

    public SpriteRenderer sprite;
    // Start is called before the first frame update

    public void Update()
    {
        if (thisCharacter != null)
        {
            if (thisCharacter.name == "Lina Bean")
            {
                if (gameState.beanState == GameState.gameState.ISBAGGED || gameState.beanState == GameState.gameState.ISNOTCOOL || gameState.beanState == GameState.gameState.WRONGBAGGED)
                {
                    this.gameObject.transform.position = tallLina.position;
                }
                else
                {
                    this.gameObject.transform.position = shortLina.position;
                }
            }
        }
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
