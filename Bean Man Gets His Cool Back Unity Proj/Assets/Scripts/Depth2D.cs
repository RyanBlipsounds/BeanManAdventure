using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Depth2D : MonoBehaviour
{
    public GameObject m_Character;

    public Vector3 characterPosition;
    public Vector3 thisPosition;

    private void Start()
    {
        Vector3 thisPosition =  new Vector3(this.transform.position.x, this.transform.position.y, m_Character.transform.position.z);
    }

    void Update()
    {
        thisPosition.z = this.transform.position.y / 1000;

        m_Character.transform.position = new Vector3(m_Character.transform.position.x, m_Character.transform.position.y, thisPosition.z);
    }
}