using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonedDoor : MonoBehaviour
{
    Rigidbody2D rb;
    Collider2D col;
    bool falling = false;
    bool isGrounded = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    private void Update()
    {
        //if (falling)
        //{
        //    if (Physics2D.Raycast(transform.position, Vector2.down, 1f, LayerMask.GetMask("Ground")))
        //    {
        //        col.isTrigger = false;
        //        return;
        //    }
        //}
        //falling = rb.velocity.y < 0;
    }
}

