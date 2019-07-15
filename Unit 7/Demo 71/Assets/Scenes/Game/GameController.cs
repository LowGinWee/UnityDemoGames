using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public GameObject floor;
    public Sprite[] spriteFloor;
    public GameObject playArea;

    public GameObject enemy;

    public static int mapWidth;
    public static int mapHeight;
    public int enemyCount;

    public List<Vector2> obstaclePos = new List<Vector2>();

    public GameObject key;
    public GameObject flask;

    void Start()
    {
        obstaclePos = new List<Vector2>();

        mapWidth = CONSTANTS.MAP_WIDTH;
        mapHeight = CONSTANTS.MAP_HEIGHT;
        enemyCount = CONSTANTS.ENEMY_COUNT;

        SpawnMap();
        SpawnObject(key, 3);
        SpawnObject(flask, 2);
        SpawnEnemy(enemyCount);
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


    private void SpawnObject(GameObject cloneThis, int num)
    {
        for (int i = 0; i < num; i++)
        {
            Vector2 pos;
            do
            {
                pos = new Vector2(Random.Range(1, mapWidth), Random.Range(1, mapHeight));
            } while (!CheckObstaclePos(pos));
            GameObject o = Instantiate(cloneThis);
            o.transform.position = pos;
            o.SetActive(true);
            o.name = cloneThis.name;
            o.transform.SetParent(playArea.transform);
            obstaclePos.Add(pos);
        }
    }

    private bool CheckObstaclePos(Vector2 pos)
    {
        foreach (Vector2 i in obstaclePos)
        {
            if (i == pos)
            {
                return false;
            }
        }
        return true;
    }

    private void SpawnEnemy(int num)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject e = Instantiate(enemy);
            e.SetActive(true);
            e.transform.SetParent(playArea.transform);
            Enemy.TYPE type = (Enemy.TYPE)Random.Range(0, 3);
            e.GetComponent<Enemy>().SetEnemy(new Vector2(Random.Range(0, mapWidth), Random.Range(0, mapHeight)), type);
        }
    }
}
