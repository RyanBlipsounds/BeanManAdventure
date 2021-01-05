﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceDetector : MonoBehaviour
{
    public string surfaceName; 

    // Start is called before the first frame update
    void Start()
    {
        surfaceName = "Concrete";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Dirt" || collision.gameObject.tag == "Concrete")
        {
            surfaceName = collision.gameObject.tag;
        }
            
    }

    private void OnTriggerExit2D(Collider2D collision)

    {
        if (collision.gameObject.tag == "Dirt" || collision.gameObject.tag == "Concrete")
        {
            surfaceName = "Concrete";
        }

    }



    // Update is called once per frame
    void Update()
    {
        
    }
}