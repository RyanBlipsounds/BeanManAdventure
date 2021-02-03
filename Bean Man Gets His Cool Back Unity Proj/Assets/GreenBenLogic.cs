using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBenLogic : MonoBehaviour
{
    public Animator animator;

    public void GreenBenSick()
    {
        animator.Play("GreenBenSick");
    }

    public void GreenBenHealthy()
    {
        animator.Play("GreenBenHealthy");
        Debug.Log("GREEN BEN");
    }
}
