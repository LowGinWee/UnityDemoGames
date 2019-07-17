using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum Type
    {
        ZOMBIE,
        DWARF
    }

    public Sprite[] spriteMonster;

    private Type type;
    private Vector2 pos;

    public void SetEnemy(Vector2 pos, Type type)
    {
        this.type = type;
        this.pos = pos;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = spriteMonster[0];
        this.gameObject.name = "zombie";
        StartCoroutine(Movement());
    }    

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = pos;
    }

    IEnumerator Movement()
    {
        while (GameController.state == GameController.STATE.START)
        {
            int movement = Random.Range(0, 4);

            if (movement == 0 && pos.y < CONSTANTS.MAP_HEIGHT - 1)
            {
                pos += Vector2.up;
            }
            else if (movement == 1 && pos.y > 0)
            {
                pos += Vector2.down;
            }
            else if (movement == 2 && pos.x > 0)
            {
                pos += Vector2.left;
            }
            else if (movement == 3 && pos.x < CONSTANTS.MAP_WIDTH - 1)
            {
                pos += Vector2.right;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
