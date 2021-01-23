using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceDetector : MonoBehaviour
{
    public string surfaceName;
    public bool isWet;

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

        if (collision.gameObject.tag == "Wet")
        {
            isWet = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)

    {
        if (collision.gameObject.tag == "Dirt" || collision.gameObject.tag == "Concrete")
        {
            surfaceName = "Concrete";
        }
        if (collision.gameObject.tag == "Wet")
        {
            isWet = false;
        }
    }
}
