using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharController : MonoBehaviour
{
    [Header("Player Movement")]
    private Rigidbody2D rb;
    private PlayerInput playerInput;
    private PlayerMovement playerMovementInput;

    private Vector2 direction;
    private Vector2 startPosition;

    //SPEED
    [SerializeField] private float movementSpeed = 6;


    //JUMPING
    private bool isGrounded;
    private bool isFalling;

    private float timeJump;
    private float maxHeight = 4f;
    private float jumpForce;

    private int maxJumpCount = 2;
    private int jumpCount;
    public bool canJumpManyTimes;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();

        playerMovementInput = new PlayerMovement();
        playerMovementInput.Enable();
        playerMovementInput.PlayerMove.Jump.performed += Jump;
    }

    private void Start()
    {
        OnStart();
        startPosition = transform.position;
    }

    void OnStart()
    {
        Application.targetFrameRate = 60;
        jumpCount = maxJumpCount;
        jumpForce = Mathf.Sqrt(maxHeight * (Physics2D.gravity.y * rb.gravityScale) * -2) * rb.mass;
    }
    private void OnDisable()
    {
        playerMovementInput.PlayerMove.Jump.performed -= Jump;
    }

    private void Update()
    {
        CheckJump();
    }

    private void FixedUpdate()
    {
        Vector2 inputVector = playerMovementInput.PlayerMove.Left_Right_Movement.ReadValue<Vector2>();
        rb.velocity = new Vector2(inputVector.x * movementSpeed, rb.velocity.y);
    }

    void Jump(InputAction.CallbackContext context)
    {
        isGrounded = false;
        isFalling = false;
        rb.velocity = Vector2.zero;

        Debug.Log("Jump Count: " + jumpCount);
        jumpCount--;

        if (context.performed)
        {
            //rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void CheckJump()
    {
        if (!isGrounded)
        {
            if (jumpCount > 0)
            {
                playerMovementInput.PlayerMove.Jump.performed += Jump;
            }
            else
            {
                playerMovementInput.PlayerMove.Jump.performed -= Jump;
            }
        }
        if (Mathf.Abs(rb.velocity.y) < 0.01f )
        {
            isGrounded = true;
            jumpCount = maxJumpCount;
            playerMovementInput.PlayerMove.Jump.performed += Jump;
            if (!canJumpManyTimes)
            {
                jumpCount = 1;
            }
        }
    }

    bool CheckFall()
    {
        if (rb.velocity.y < 0)
        {
            isFalling = true;
        }
        else
        {
            isFalling = false;
        }
        return isFalling;
    }
    public void ResetPosition()
    {
        transform.position = startPosition;
    }
}
