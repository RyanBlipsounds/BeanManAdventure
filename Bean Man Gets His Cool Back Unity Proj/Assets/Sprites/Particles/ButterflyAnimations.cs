using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyAnimations : MonoBehaviour
{
    public Animator animator;

    private int TransitionNum = 33;
    private int IdleBreakNum = 33;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayIdleBreakAnim();
        PlayTransitionAnim();

    }

    private void PlayIdleBreakAnim()
    {
        if (Random.Range(0, 1000) == IdleBreakNum)
        {
            animator.SetTrigger("IdleBreak");
        }
    }

    private void PlayTransitionAnim()
    {
        if (Random.Range(0, 1000) == TransitionNum)
        {
            animator.SetTrigger("Transition");
        }
    }
}
