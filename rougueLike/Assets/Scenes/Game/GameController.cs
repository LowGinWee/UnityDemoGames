using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public enum STATE
    {
        START,
        PAUSE,
        WIN,
        LOSE
    }

    public GameObject floor;
    public Sprite[] spriteFloor;
    public Sprite[] spriteWall;
    public GameObject[] corner;
    public GameObject playArea;
    public GameObject enemy;
    public GameObject obstacle;
    public GameObject chest;
    public GameObject exit;
    public GameObject flask;
    public Player player;

    public int enemyCount;
    public int obstacleCount;
    public int mapWidth;
    public int mapHeight;
    public int flaskCount;

    public List<Vector2> obstaclePos = new List<Vector2>();

    public static STATE state;
    public int level;


    // Start is called before the first frame update
    void Start()
    {
        enemyCount = CONSTANTS.ENEMY_COUNT;
        obstacleCount = CONSTANTS.OBSTACLE_COUNT;
        mapWidth = CONSTANTS.MAP_WIDTH;
        mapHeight = CONSTANTS.MAP_HEIGHT;
        flaskCount = CONSTANTS.FLASK_COUNT;
        CreateLevel();
        level = 1;
    }

    public void CreateLevel()
    {
        obstaclePos.Clear();
        state = STATE.START;
        foreach (Transform i in playArea.transform)
        {
            Destroy(i.gameObject);
        }
        SpawnMap();
        SpawnEnemy(enemyCount);
        SpawnObject(flask, flaskCount);
        SpawnObject(obstacle, obstacleCount);
        SpawnObject(chest, 3);
        SpawnObject(exit, 1);
        player.StopAllCoroutines();
        player.SpawnPlayer();
    }

    public void NewLevel()
    {
        level++;
        enemyCount++;
        obstacleCount += 2;
        mapWidth += 2;
        mapHeight += 2;
        flaskCount += 1;
        CreateLevel();
    }


    private void SpawnEnemy(int num)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject e = Instantiate(enemy);
            e.SetActive(true);
            e.name = enemy.name;
            e.transform.SetParent(playArea.transform);
            e.GetComponent<Enemy>().SetEnemy(new Vector2(Random.Range(0, mapWidth), Random.Range(0, mapHeight)), Enemy.Type.ZOMBIE);
        }
    }

    private void SpawnObject(GameObject cloneThis, int num)
    {
        for (int i = 0; i < num; i++)
        {
            Vector2 pos;
            do
            {
                pos = new Vector2(Random.Range(1, mapWidth), Random.Range(1, mapHeight));
            } while (!CheckObstaclePos(pos)) ;
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

        InstantiatePanel(playArea.transform, floor, new Vector2(-1, -1), spriteWall[0]);
        InstantiatePanel(playArea.transform, floor, new Vector2(mapWidth, -1), spriteWall[1]);

        for (int i = 0; i < mapWidth; i++)
        {
            InstantiatePanel(playArea.transform, floor, new Vector2(i, -1), spriteWall[2]);
            InstantiatePanel(playArea.transform, floor, new Vector2(i, mapHeight), spriteWall[3]);
        }
        for (int i = 0; i <= mapHeight; i++)
        {
            InstantiatePanel(playArea.transform, floor, new Vector2(-1, i), spriteWall[4]);
            InstantiatePanel(playArea.transform, floor, new Vector2(mapWidth, i), spriteWall[5]);
        }
    }

    private void InstantiatePanel(Transform parent, GameObject panel, Vector2 pos, Sprite sprite)
    {
        GameObject i = Instantiate(panel);
        i.GetComponent<SpriteRenderer>().sprite = sprite;
        i.transform.position = pos;
        i.transform.SetParent(parent);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.state == GameController.STATE.WIN)
        {
            NewLevel();
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
