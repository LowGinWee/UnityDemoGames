using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public GameObject floor;
    public Sprite[] spriteFloor;
    public GameObject playArea;
    public int mapWidth;
    public int mapHeight;


    // Start is called before the first frame update
    void Start()
    {
        mapWidth = CONSTANTS.MAP_WIDTH;
        mapHeight = CONSTANTS.MAP_HEIGHT;
        SpawnMap();
    }

    public void CreateLevel()
    {
    }

    public void NewLevel()
    {
    }


    private void SpawnMap()
    {
        Vector2 pos = new Vector2(0, 0);
        for (int i = 0; i < mapWidth; i++)
        {
            for (int j = 0; j < mapHeight; j++)
            {
                pos = new Vector2(i, j); ;
                InstantiatePanel(playArea.transform, floor, pos, spriteFloor[Random.Range(0, spriteFloor.Length)]);
            }
        }
    }

    private void InstantiatePanel(Transform parent, GameObject panel, Vector2 pos, Sprite sprite)
    {
        GameObject i = Instantiate(panel);
        i.GetComponent<SpriteRenderer>().sprite = sprite;
        i.transform.position = pos;
        i.transform.SetParent(parent);
    }

}
