using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficConeCollection : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject _trafficConePile;
    public GameObject _fireHydrantPile;


    public void FireHydrantPile()
    {
        _trafficConePile.SetActive(false);
        _fireHydrantPile.SetActive(true);
    }

    public void TrafficConePile()
    {
        _trafficConePile.SetActive(true);
        _fireHydrantPile.SetActive(false);
    }
}
