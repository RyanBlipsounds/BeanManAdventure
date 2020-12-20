using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseStateLogic : MonoBehaviour
{
    public GameObject coolSprite;
    public GameObject beangoSprite;
    public GameObject notCoolSprite;
    public GameObject baggedSprite;

    public void Start()
    {
        if (coolSprite != null) {
            coolSprite.SetActive(true);
        }
        if (beangoSprite != null)
        {
            beangoSprite.SetActive(false);
        }
        if (notCoolSprite != null)
        {
            notCoolSprite.SetActive(false);
        }
        if (baggedSprite != null)
        {
            baggedSprite.SetActive(false);
        }
    }

    public void CoolState() {
        if (this.gameObject.name == "BeanManHouse")
        {
            coolSprite.SetActive(true);
            notCoolSprite.SetActive(false);
            baggedSprite.SetActive(false);
        }
        if (this.gameObject.name == "BeangoHouse")
        {
            coolSprite.SetActive(true);
            beangoSprite.SetActive(false);
        }
    }

    public void BeangoState()
    {
        if (this.gameObject.name == "BeangoHouse")
        {
            coolSprite.SetActive(false);
            beangoSprite.SetActive(true);
        }
    }

    public void NotCoolState()
    {
        if (this.gameObject.name == "BeanManHouse")
        {
            coolSprite.SetActive(false);
            notCoolSprite.SetActive(true);
            baggedSprite.SetActive(false);
        }
        if (this.gameObject.name == "BeangoHouse")
        {
            coolSprite.SetActive(true);
            beangoSprite.SetActive(false);
        }
    }

    public void BaggedState()
    {
        if (this.gameObject.name == "BeanManHouse")
        {
            coolSprite.SetActive(false);
            notCoolSprite.SetActive(false);
            baggedSprite.SetActive(true);
        }
        if (this.gameObject.name == "BeangoHouse")
        {
            coolSprite.SetActive(true);
            beangoSprite.SetActive(false);
        }
    }
}
