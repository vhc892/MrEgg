using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonedDoor : MonoBehaviour
{
    Rigidbody2D rb;
    Collider2D col;
    Collider2D childCol;
    bool falling = false;
    bool isGrounded = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        childCol = transform.GetChild(0).GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (falling)
        {
            childCol.enabled = true; 
            return;
        }
        falling = rb.velocity.y < 0;
    }
}

