using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirthdayKnife : MonoBehaviour
{
    public NPC thisNPC;

    public GameObject LeftKnife;
    public GameObject RightKnife;

    public bool beanInRange;

    public PlayerController BeanMan;

    private bool _hasPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    void KnifeIn()
    {
        _hasPlayed = false;

        FMODUnity.RuntimeManager.PlayOneShot("event:/Knife In", this.transform.position);
    }
    // Update is called once per frame
    void Update()
    {
       
        if (beanInRange == true) {
            if (_hasPlayed == true)
            {
                KnifeIn();

            }
            _hasPlayed = false;
            return;

        }
        if (thisNPC.isLeft && BeanMan.GlassesSpriteRenderer.flipX == true)
        {
            LeftKnife.SetActive(false);
            RightKnife.SetActive(false);
            if (_hasPlayed == true)
            {
                KnifeIn();

            }
            _hasPlayed = false;
            return;
        }

        if (!thisNPC.isLeft && BeanMan.GlassesSpriteRenderer.flipX == false)
        {
            LeftKnife.SetActive(false);
            RightKnife.SetActive(false);
            if (_hasPlayed == true)
            {
                KnifeIn();

            }
            _hasPlayed = false;
            return;
        }

        if (thisNPC.isLeft)
        {
            LeftKnife.SetActive(true);
            RightKnife.SetActive(false);
            
        
        }
        if (!thisNPC.isLeft)
        {
            LeftKnife.SetActive(false);
            RightKnife.SetActive(true);
            

            
        } 
        if (!thisNPC.isLeft || thisNPC.isLeft)
        {
            if (_hasPlayed == false)
            {
                _hasPlayed = true;
               
                FMODUnity.RuntimeManager.PlayOneShot("event:/Knife Out", this.transform.position);
                
            }
            

        } 
        
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        beanInRange = true;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        beanInRange = false;
    }
}
