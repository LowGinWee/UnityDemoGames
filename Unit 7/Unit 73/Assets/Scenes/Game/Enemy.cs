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
        StartCoroutine(Movement());
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Movement()
    {
        while (GameController.gameState == GameController.GAMESTATE.START)
        {
            int movement = Random.Range(0, 4);
            Vector2 currentPos = this.gameObject.transform.position;
            if (movement == 0 && currentPos.y < CONSTANTS.MAP_HEIGHT - 1)
            {
                currentPos += Vector2.up;
            }
            else if (movement == 1 && currentPos.y > 0)
            {
                currentPos += Vector2.down;
            }
            else if (movement == 2 && currentPos.x > 0)
            {
                currentPos += Vector2.left;
            }
            else if (movement == 3 && currentPos.x < CONSTANTS.MAP_WIDTH - 1)
            {
                currentPos += Vector2.right;
            }
            this.gameObject.transform.position = currentPos; 

            yield return new WaitForSeconds(CONSTANTS.ENEMY_SPEED);

        }
    }
}
