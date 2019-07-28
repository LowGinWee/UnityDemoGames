using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum PLAYER_STATES
    {
        IDLE,
        MOVING,
        DEAD
    }

    public static PLAYER_STATES playerState;

    private Vector2 targetPos;
    private Vector2 currentPos;


    private Animator animator;
    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
        playerState = PLAYER_STATES.IDLE;
        targetPos = new Vector2(0, 0.7f);
        currentPos = targetPos;
        this.gameObject.transform.position = targetPos;
    }

    void Update()
    {
        if (playerState == PLAYER_STATES.IDLE) MoveUser();
    }

    private void MoveUser()
    {
        Vector2 add = new Vector2((Input.GetKey("left") ? -1 : 0) + (Input.GetKey("right") ? 1 : 0), (Input.GetKey("up") ? 1 : 0) + (Input.GetKey("down") ? -1 : 0));
        targetPos += add;

        if (add.x == -1)
        {
            this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (add.x == 1)
        {
            this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        if (targetPos != currentPos && CheckBoundary(targetPos))
        {
            animator.SetTrigger("Moving");
            StartCoroutine(MoveToPosition(1.0f));
        } else
        {
            targetPos = currentPos;
        }
    }

    private IEnumerator MoveToPosition(float timeToMove)
    {
        playerState = PLAYER_STATES.MOVING;
        float t = 0.0f;

        do
        {
            currentPos = this.gameObject.transform.position;
            t += Time.deltaTime / timeToMove;
            this.gameObject.transform.position = Vector2.MoveTowards(this.gameObject.transform.position, targetPos, t);
            yield return null;
        } while (t < 1 && currentPos != targetPos);
        playerState = PLAYER_STATES.IDLE;
    }

    private bool CheckBoundary(Vector2 targetPos)
    {
        if (targetPos.x < 0 || targetPos.x >= GameController.mapWidth || targetPos.y < 0 || targetPos.y >= GameController.mapHeight)
        {
            return false;
        }
        return true;
    }
}


