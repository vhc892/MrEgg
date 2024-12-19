using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    private Rigidbody2D rb;
    private bool moveLeft;
    private bool moveRight;
    private float horizontalMove;


    private bool isGrounded;
    private int jumpCount;
    private const int maxJumps = 2;
    private Vector2 startPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveLeft = false;
        moveRight = false;
        startPosition = transform.position;
    }
    public void PointerDownLeft()
    {
        moveLeft = true;
    }
    public void PointerUpLeft()
    {
        moveLeft = false;
    }
    public void PointerDownRight() 
    {
        moveRight = true; 
    }
    public void PointerUpRight()
    {
        moveRight = false;
    }
    void Update()
    {
        MovePlayer();
    }
    private void MovePlayer()
    {
        if(moveLeft)
        {
            horizontalMove = -moveSpeed;
        }
        else if (moveRight)
        {
            horizontalMove = moveSpeed;
        }
        else
        {
            horizontalMove = 0;
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2 (horizontalMove, rb.velocity.y);
    }
    //void Move()
    //{
    //    float moveInput = Input.GetAxis("Horizontal");
    //    rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    //}

    public void Jump()
    {
        if (jumpCount < maxJumps)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            //rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            jumpCount++;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.5f) //detect from bottom
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
    public void ResetPosition()
    {
        transform.position = startPosition;
    }
}
