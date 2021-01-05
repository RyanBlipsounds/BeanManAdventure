using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizePeeper : MonoBehaviour
{

    public SpriteRenderer PeeperSprite;

    public List<Sprite> Peepers = new List<Sprite>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        PeeperSprite.sprite = Peepers[Random.Range(0, Peepers.Count)];
    }
}
