using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private float speed = 10.0f;
    public Rigidbody rb;
    public int score = 0;
    public int playerHp = 5;

    void Start()
    {
        Debug.Log("Hello Word!");
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Do Something here");
        }

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.name == "Coin")
        {
            score += 1;
            Destroy(other.gameObject);
        }

        if (other.name == "Obstacle")
        {
            playerHp -= 1;
        }
        Debug.Log("You have collided with " + other.gameObject.name + "\n"
            + "Your Score is: " + score + " Your Hp is: " + playerHp);
    }
}


