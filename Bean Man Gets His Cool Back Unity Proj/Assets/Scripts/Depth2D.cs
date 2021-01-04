using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Depth2D : MonoBehaviour
{
    public GameObject m_Character;
    public GameObject m_trafficCone;
    public GameObject m_leftGlasses;
    public GameObject m_rightGlasses;

    public PlayerController m_player;

    public GameObject m_frontCensorBar;
    public GameObject m_backCensorBar;

    public Vector3 characterPosition;
    public Vector3 thisPosition;
    public Vector3 propPosition;
    public Vector3 censorPosition;

    public bool lookingDown = true;

    private void Start()
    {
        Vector3 thisPosition =  new Vector3(this.transform.position.x, this.transform.position.y, m_Character.transform.position.z);
        Vector3 propPosition = new Vector3(this.transform.position.x, this.transform.position.y, m_Character.transform.position.z);
        //Vector3 censorPosition = new Vector3(this.transform.position.x, this.transform.position.y, censorPosition.transform.position.z);
    }

    void Update()
    {
        thisPosition.z = this.transform.position.y / 1000;

        m_Character.transform.position = new Vector3(m_Character.transform.position.x, m_Character.transform.position.y, thisPosition.z);

        if (m_player != null) {
            if (m_player.movementDirection.y < 0 && lookingDown == false)
            {
                lookingDown = true;
                m_frontCensorBar.SetActive(true);
                m_backCensorBar.SetActive(false);
                //m_censorBar.transform.position = new Vector3(m_censorBar.transform.position.x, m_censorBar.transform.position.y, -0.00001f);
            }
            else if (m_player.movementDirection.y > 0 && lookingDown == true)
            {
                lookingDown = false;
                m_frontCensorBar.SetActive(false);
                m_backCensorBar.SetActive(true);
                //m_censorBar.transform.position = new Vector3(m_censorBar.transform.position.x, m_censorBar.transform.position.y, 0.00001f);
            }
        }
    }
}