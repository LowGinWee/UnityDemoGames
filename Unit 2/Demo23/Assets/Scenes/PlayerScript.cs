using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private float speed = 10.0f;
    public Rigidbody rb;
    public int score = 0;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
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
    }
}


