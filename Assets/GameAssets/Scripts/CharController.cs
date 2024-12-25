using Spine.Unity;
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
    [SerializeField] private SkeletonAnimation anim;
    [SerializeField] private LayerMask boxLayer;

    private Vector2 direction;
    private Vector2 startPosition;

    bool isFinished = false;

    //SPEED
    [SerializeField] private float movementSpeed = 6;
    bool isRunning = false;


    //JUMPING
    private bool isGrounded;
    private bool isFalling;
    public bool canJumpManyTimes;

    private float timeJump;
    private float maxHeight = 4f;
    private float jumpForce;

    private int maxJumpCount = 2;
    private int jumpCount;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        anim = GetComponentInChildren<SkeletonAnimation>();
    }

    private void Start()
    {
        startPosition = transform.position;
        Application.targetFrameRate = 60;
        jumpCount = maxJumpCount;
        jumpForce = Mathf.Sqrt(maxHeight * (Physics2D.gravity.y * rb.gravityScale) * -2) * rb.mass;
    }

    void OnStart()
    {
        rb.isKinematic = false;
        isFinished = false; 
        transform.position = startPosition;
    }

    private void Update()
    {
        CheckJump();
    }

    private void FixedUpdate()
    {
        //MOVE
        Vector2 inputVector = playerMovementInput.PlayerMove.Left_Right_Movement.ReadValue<Vector2>();
        rb.velocity = new Vector2(inputVector.x * movementSpeed, rb.velocity.y);
    }

    void Idle(InputAction.CallbackContext context)
    {
        anim.AnimationState.SetAnimation(0, "idle", true);
        isRunning = false;
    }
    void Move(InputAction.CallbackContext context)
    {
        anim.AnimationState.SetAnimation(0, "run", true);

        isRunning = true;
        Vector2 inputVector = playerMovementInput.PlayerMove.Left_Right_Movement.ReadValue<Vector2>();
        transform.rotation = Quaternion.Euler(0, inputVector.x > 0 ? 0 : 180, 0);
    }
    void Jump(InputAction.CallbackContext context)
    {
        isGrounded = false;
        isFalling = false;
        rb.velocity = Vector2.zero;

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
        if (Mathf.Abs(rb.velocity.y) < 0.01f)
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
    
    private void OnFinishshLevel()
    {
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
        isFinished = true;
        anim.AnimationState.SetAnimation(0, "idle", true);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Box")
        {
            if (IsBoxToSide(Vector2.left) || IsBoxToSide(Vector2.right))
            {
                anim.AnimationState.SetAnimation(0, "push", true);
            }
        }
    }
    private bool IsBoxToSide(Vector2 direction)
    {
        float rayDistance = 1.5f;
        Vector2 rayOrigin = new Vector2(transform.position.x, transform.position.y + 0.1f);

        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, direction, rayDistance, boxLayer);

        if (hit.collider != null)
        {
            Debug.Log($"Raycast hit: {hit.collider.gameObject.name}");

            if (hit.collider.CompareTag("Box"))
            {
                return true;
            }
        }
        return false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Box")
        {
            Vector2 inputVector = playerMovementInput.PlayerMove.Left_Right_Movement.ReadValue<Vector2>();

            if (Mathf.Abs(inputVector.x) > 0.1f)
            {
                anim.AnimationState.SetAnimation(0, "run", true);
            }
            else
            {
                anim.AnimationState.SetAnimation(0, "idle", true);
            }
        }
    }
    private void OnDrawGizmos()
    {
        float rayDistance = 1.5f;
        Vector2 rayOriginLeft = new Vector2(transform.position.x, transform.position.y + 0.1f);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(rayOriginLeft, rayOriginLeft + Vector2.left * rayDistance);

        Vector2 rayOriginRight = new Vector2(transform.position.x, transform.position.y + 0.1f);
        Gizmos.DrawLine(rayOriginRight, rayOriginRight + Vector2.right * rayDistance);
    }

    private void OnEnable()
    {
        playerMovementInput = new PlayerMovement();
        playerMovementInput.Enable();
        playerMovementInput.PlayerMove.Jump.performed += Jump;
        playerMovementInput.PlayerMove.Left_Right_Movement.performed += Move;
        playerMovementInput.PlayerMove.Left_Right_Movement.canceled += Idle;

        GameEvents.onLevelStart += OnStart;

        GameEvents.onLevelFinish += OnFinishshLevel;
    }
    private void OnDestroy()
    {
        playerMovementInput.PlayerMove.Jump.performed -= Jump;
        playerMovementInput.PlayerMove.Left_Right_Movement.performed -= Move;
        playerMovementInput.PlayerMove.Left_Right_Movement.canceled -= Idle;

        GameEvents.onLevelStart -= OnStart;

        GameEvents.onLevelFinish -= OnFinishshLevel;
    }
}
