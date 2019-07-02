using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject coin;
    public GameObject player;
    public GameObject obstacle;
    private List<Vector3> objectPositions = new List<Vector3>();

    public PlayerScript playerScript;
    public Text textScore;

    void Start()
    {
        objectPositions.Add(player.transform.position);
        SpawnObstacles(obstacle, 3);
        SpawnObstacles(coin, 10);
    }

    void FixedUpdate()
    {
        textScore.text = "Score: " +  playerScript.score.ToString();
    }

    private bool CheckDist(Vector3 newPos)
    {
        foreach (Vector3 i in objectPositions)
        {
            if (Vector3.Distance(i, newPos) < 2)
            {
                return false;
            }
        }
        return true;
    }

    private Vector3 GeneratePosition()
    {
        Vector3 position;
        do
        {
             position = new Vector3(Random.Range(-4.5f, 4.5f), 0.3f, Random.Range(-4.5f, 4.5f));
        }
        while (!CheckDist(position));
        return position;
    }

    private void SpawnObstacles (GameObject cloneThis, int num)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject clone = Instantiate(cloneThis);
            clone.name = cloneThis.name;
            clone.transform.position = GeneratePosition();
            objectPositions.Add(clone.transform.position);
            clone.transform.SetParent(cloneThis.transform.parent);
            clone.SetActive(true);
        }
    }

}


