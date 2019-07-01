using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    private float speed = 10.0f;
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameController.HP == 0 || GameController.score == 10)
        {
            rb.velocity = Vector3.zero;
        }
        else
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

            rb.AddForce(movement * speed);
        }

    }


    //events
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("hehe" + other.name);
        if (other.name == "Coin")
        {
            GameController.score += 1;
            Destroy(other.gameObject);
        }

        if (other.name == "Obstacle")
        {
            GameController.HP -= 1;
        }
    }

}
