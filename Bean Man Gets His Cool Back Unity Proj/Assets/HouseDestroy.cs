using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseDestroy : MonoBehaviour
{
    public GameObject walkThroughHouse;
    public GameObject dontWalkThroughHouse;

    public List<Sprite> fixedHouseSprites = new List<Sprite>();
    public List<Sprite> destroyedHouseSprites = new List<Sprite>();
    public List<SpriteRenderer> HouseSprites = new List<SpriteRenderer>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void DestroyHouses()
    {
        walkThroughHouse.SetActive(true);
        dontWalkThroughHouse.SetActive(false);

        int count = 0;
        foreach (SpriteRenderer house in HouseSprites)
        {
            house.sprite = destroyedHouseSprites[count];
            count++;
        }
    }

    public void FixHouses()
    {
        walkThroughHouse.SetActive(false);
        dontWalkThroughHouse.SetActive(true);

        int count = 0;
        foreach (SpriteRenderer house in HouseSprites)
        {
            house.sprite = fixedHouseSprites[count];
            count++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
