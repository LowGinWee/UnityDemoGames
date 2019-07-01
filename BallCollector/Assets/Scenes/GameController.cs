using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject coin;
    public GameObject obstacle;
    public GameObject player;
    public GameObject objectContainer;
    public Slider HPSlider;
    public Text ScoreText;
    public static int score;
    public static int HP;
    public GameObject restartButton;



    private List<Vector3> positions;
    private Vector3 position;
    // Start is called before the first frame update
    public void Start()
    {
        DeleteAllObjects();
        score = 0;
        HP = 5;
        positions = new List<Vector3>();
        position = new Vector3(0, Constants.Y_HEIGHT, 0);
        player.transform.position = position;
        positions.Add(position); //insert player starting position
        SpawnClone(coin, 10);
        SpawnClone(obstacle, 3);
    }

    private void DeleteAllObjects()
    {
        foreach (Transform child in objectContainer.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void SpawnClone(GameObject cloneThis, int num)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject clone = Instantiate(cloneThis);
            clone.name = cloneThis.name;
            bool minSpawnDist = true;
            while (minSpawnDist)
            {
                minSpawnDist = false;
                position = new Vector3(Random.Range(-Constants.MAX_X_BOUNDARY, Constants.MAX_X_BOUNDARY), Constants.Y_HEIGHT, Random.Range(-Constants.MAX_Z_BOUNDARY, Constants.MAX_Z_BOUNDARY));
                foreach (Vector3 j in positions)
                {
                    float dist = Vector3.Distance(j, position);
                    if (dist < 1)
                    {
                        minSpawnDist = true;
                        break;
                    }
                }
            }
            positions.Add(position);
            clone.transform.position = position;
            clone.transform.SetParent(objectContainer.transform);
            clone.SetActive(true);
        }
    }


    // Update is called once per frame
    void Update()
    {
        ScoreText.text = "Score: " + score;
        HPSlider.value = HP;
        if (HP == 0 || score == 10)
        {
            restartButton.SetActive(true);
        } else
        {
            restartButton.SetActive(false);
        }
    }

    
}
