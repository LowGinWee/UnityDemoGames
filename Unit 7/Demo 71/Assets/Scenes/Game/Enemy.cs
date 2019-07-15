using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum TYPE
    {
        Zombie, // = 0
        Ogre, // = 1
        Demon // = 2
    }

    public TYPE type;

    public Sprite[] sprites;

    public void SetEnemy(Vector2 pos, TYPE type)
    {
        this.type = type;
        this.gameObject.transform.position = pos;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = sprites[(int)type];
        this.gameObject.name = "Enemy";
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
