using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum PLAYER_STATE
    {
        IDLE,
        MOVE,
        ATTACK,
        DEAD
    }
    public PLAYER_STATE playerState;
    private Vector2 targetPos;
    private Vector2 currentPos;
    private Animator animator;
    public UserInterface userInterface;
    public int keyNum;
    public int health;
    public GameController gameController;
    public GameObject diedUI;
    // Start is called before the first frame update
    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();

    }

    public void SpawnPlayer()
    {
        health = CONSTANTS.HEALTH;
        keyNum = 0;
        playerState = PLAYER_STATE.IDLE;
        targetPos = new Vector2(0, 0.7f);
        currentPos = targetPos;
        this.gameObject.transform.position = targetPos;
        userInterface.CreateHpBar();
        StartCoroutine(Timer());
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            playerState = PLAYER_STATE.DEAD;
            GameController.state = GameController.STATE.LOSE;
            diedUI.SetActive(true);
        } else if (currentPos == targetPos && playerState != PLAYER_STATE.MOVE)
        {
            MoveUser();
        }
        Debug.Log(playerState);
    }

    private void MoveUser()
    {
        Vector2 add = new Vector2((Input.GetKey("left") ? -1 : 0) + (Input.GetKey("right") ? 1 : 0), (Input.GetKey("up") ? 1 : 0) + (Input.GetKey("down") ? -1 : 0));
        targetPos += add;
        if (CheckBoundary(targetPos) && currentPos!= targetPos)
        {
            if (add.x == -1)
            {
                this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (add.x == 1)
            {
                this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            StartCoroutine(MoveToPosition(3.0f));
        }
        else
        {
            targetPos = currentPos;
        }
    }

    private bool CheckBoundary(Vector2 targetPos)
    {
        if (targetPos.x < 0 || targetPos.x >= gameController.mapWidth || targetPos.y < 0 || targetPos.y >= gameController.mapHeight)
        {
            return false;
        }
        return true;
    }

    private IEnumerator MoveToPosition(float timeToMove)
    {
        playerState = PLAYER_STATE.MOVE;
        animator.SetTrigger("Move");
        var t = 0.0f;
        do
        {
            currentPos = this.gameObject.transform.position;
            t += Time.deltaTime / timeToMove;
            this.transform.position = Vector2.MoveTowards(this.gameObject.transform.position, targetPos, t);
            yield return null;
        } while (t < 1 && currentPos != targetPos);
        playerState = PLAYER_STATE.IDLE;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "zombie")
        {
            animator.SetTrigger("Hit");
            health -= 1;
            userInterface.MinusHealth();
            Debug.Log("Your health: " + health);
        } else if (col.gameObject.name == "flask")
        {
            if (health < CONSTANTS.HEALTH - 1)
            {
                health += 2;
                userInterface.PlusHealth();
            }
            else
            {
                health = CONSTANTS.HEALTH;
                userInterface.PlusHealth();
            }
            Destroy(col.gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKey(KeyCode.Space) && other.gameObject.name == "chest")
        {
            keyNum += 1;
            userInterface.KeyPickedEnableBox();
            other.gameObject.name = "chestOpened";
            other.gameObject.GetComponentInChildren<Animator>().SetTrigger("chestOpen");
            
        } else if (Input.GetKey(KeyCode.Space) && other.gameObject.name == "exit")
        {
            userInterface.ExitBox(keyNum == 3 ? true : false);
            if (keyNum == 3)
            {
                StopAllCoroutines();
                GameController.state = GameController.STATE.PAUSE;
            }
        }
    }

    public IEnumerator Timer()
    {
        while (playerState != PLAYER_STATE.DEAD)
        {
            yield return new WaitForSecondsRealtime(5);
            health = health - 1;
            animator.SetTrigger("Hit");
            userInterface.MinusHealth();
            Debug.Log(health);
        }
    }

}
