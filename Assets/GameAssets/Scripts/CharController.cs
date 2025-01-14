
using DG.Tweening;
using Helper;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;


public class CharController : MonoBehaviour
{
    public Rigidbody2D RGBD => rb;

    private Rigidbody2D rb;
    private Collider2D col;
    private CharChildCtrl childCtrl;
    private PlayerMovement playerMovementInput;
    private SkeletonAnimation anim;
    private Tween sequence;

    [SerializeField] private LayerMask boxLayer;
    [SerializeField] RescueBalloon balloon;

    public Drag drag;
    public Vector2 startPosition;
    public Vector2 endPosition;

    [Header("Player Movement")]
    Vector2 inputVector;
    bool isFinished = false;
    float animCD = 0.5f;

    //MOVING
    public float movementSpeed = 5;
    bool isMovementPressed = false;
    bool isPushing = false;
    bool isResueing = false;

    //JUMPING

    private float lastYposition;
    private bool isGrounded;
    public bool canJumpManyTimes;
    private bool isJumpPressed;

    private float timeJump;
    private float maxHeight = 4f;
    private float jumpForce;

    private int maxJumpCount = 20;
    private int jumpCount;

    


    //STRING CACHING
    string[] groundTags = { "Ground", "Box", "Player" };
    string[] runAnimation = { "run", "run2", "run3" } ;
    int currentRunAnimation = 0;
    string idleAnimation = "idle2";
    string jumpUpAnimation = "jump_up";
    string jumpDownAnimation = "jump_down";
    string pushAnimation = "push";
    string dieAnimation = "die";

    string jumpSound = "Jump";
    string jump2Sound = "Jump2";
    string pushSound = "PushingBox";
    string walkSound = "Walking";

    private Dictionary<string, float> walkSoundIntervals = new Dictionary<string, float>
    {
        { "run", 0.4f },
        { "run2", 0.33f },
        { "run3", 0.25f }
    };
    private float lastWalkSoundTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        childCtrl = GetComponentInChildren<CharChildCtrl>();
        anim = GetComponentInChildren<SkeletonAnimation>();

        playerMovementInput = new PlayerMovement();
        drag.enabled = false;
    }

    private void Start()
    {
        startPosition = transform.position;
        Application.targetFrameRate = 60;
        jumpForce = Mathf.Sqrt(maxHeight * (Physics2D.gravity.y * rb.gravityScale) * -2) * rb.mass;
    }

    void OnStart()
    {
        drag.enabled = GameConfig.Instance.CurrentLevel == 14;

        PlayerEnable();
        if (canJumpManyTimes) jumpCount = maxJumpCount;
        else jumpCount = 1;
        transform.position = startPosition;
        anim.AnimationState.SetAnimation(0, idleAnimation, true);
        DOTween.KillAll();
    }

    private void Update()
    {
        if (isFinished)
        {
            return;
        }
        CheckJump();
        if (isJumpPressed)
        {
            return;
        }
        if (drag.isDragging) return;
        else if (isPushing)
        {
            if (anim.AnimationName != pushAnimation)
            {
                AnimationCooldown(pushAnimation, true);
            }
        }
        else if (isMovementPressed || inputVector.x != 0)
        {
            if (walkSoundIntervals.TryGetValue(anim.AnimationName, out float currentInterval))
            {
                if (Time.time - lastWalkSoundTime >= currentInterval)
                {
                    AudioManager.Instance.PlaySFX(walkSound);
                    lastWalkSoundTime = Time.time;
                }
            }

            if (anim.AnimationName != runAnimation[currentRunAnimation])
            {
                AnimationCooldown(runAnimation[currentRunAnimation], true);
            }
        }


        else
        //if (!isJumpPressed && !isMovementPressed && isGrounded && !isPushing)
        {
            AnimationCooldown(idleAnimation, true);
        }
    }

    private void FixedUpdate()
    {
        //MOVE
        if (isFinished)
        {
            return;
        }
        inputVector = playerMovementInput.PlayerMove.Left_Right_Movement.ReadValue<Vector2>();
        rb.velocity = new Vector2(inputVector.x * movementSpeed, rb.velocity.y);
    }

    private void AnimationCooldown(string animName, bool loop)
    {
        if (anim.AnimationName != animName)
        {
            anim.AnimationState.SetAnimation(0, animName, loop);
        }
    }
    void Move(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            System.Random rnd = new System.Random();
            currentRunAnimation = rnd.Next(0, runAnimation.Length);

            isMovementPressed = true;
            inputVector = playerMovementInput.PlayerMove.Left_Right_Movement.ReadValue<Vector2>();
            transform.rotation = Quaternion.Euler(0, inputVector.x > 0 ? 0 : 180, 0);
        }
        else if (context.performed && isGrounded && !isPushing)
        {
            anim.AnimationState.SetAnimation(0, runAnimation[currentRunAnimation], true);
        }
        else if (context.canceled)
        {
            AudioManager.Instance.StopSFX(walkSound);
            isMovementPressed = false;
        }
    }
    void Jump(InputAction.CallbackContext context)
    {
        isGrounded = false;
        rb.velocity = Vector2.zero;

        if (context.started)
        {
            isGrounded = false;
            isJumpPressed = true;
            isPushing = false;
            anim.AnimationState.SetAnimation(0, jumpUpAnimation, false);
            anim.AnimationState.AddAnimation(0, jumpDownAnimation, false, 0);
            jumpCount--;
            lastYposition = transform.position.y;
            string selectedJumpSound = UnityEngine.Random.value > 0.5f ? jumpSound : jump2Sound;
            AudioManager.Instance.PlaySFX(selectedJumpSound);
            AudioManager.Instance.StopSFX(walkSound);
        }
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
                playerMovementInput.PlayerMove.Jump.started += Jump;
                playerMovementInput.PlayerMove.Jump.performed += Jump;
            }
            else
            {
                playerMovementInput.PlayerMove.Jump.started -= Jump;
                playerMovementInput.PlayerMove.Jump.performed -= Jump;
            }
        }
    }

    public void ReturnToStartPosition()
    {
        PlayerDisable();
        isResueing = true;
        anim.AnimationState.SetAnimation(0, dieAnimation, false);
        transform.DOMove(transform.position + Vector3.down * 2, 2f).OnComplete(() =>
        {
            Rescue();
        });
    }
    public void Rescue()
    {
        balloon.transform.DOMove(transform.position + Vector3.up * 3, 2f).OnComplete(() =>
        {
            balloon.rescueTarget = gameObject;
            balloon.isRescueing = true;
            balloon.transform.DOMove(startPosition + Vector2.up * 3, 1f).OnComplete(() =>
            {
                balloon.isRescueing = false;
                balloon.transform.DOMove((Vector3)balloon.startPos, 1f);
            });
            transform.DOMove(startPosition, 1f).OnComplete(() => PlayerEnable());
        });
    }

    private void PlayerEnable()
    {
        col.enabled = true;
        childCtrl.col.enabled = true;
        rb.isKinematic = false;
        isFinished = false;
        isResueing = false;

        EnableInput();
    }
    private void PlayerDisable()
    {
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;

        col.enabled = false;
        childCtrl.col.enabled = false;
        isFinished = true;
        isGrounded = true;
        isJumpPressed = false;
        isMovementPressed = false;
        isPushing = false;

        DisableInput();
    }

    private void OnFinishshLevel()
    {
        PlayerDisable();
        anim.AnimationState.SetAnimation(0, idleAnimation, true);
        endPosition = transform.position;
        //balloon.transform.DOMove(balloon.startPos, 1f);
    }

    private void OnPause()
    {

    }

    private void OnResume()
    {
        if (!isResueing)
        {
            anim.AnimationState.SetAnimation(0, jumpDownAnimation, true);
        }
    }

    private void OnRestart()
    {
        OnFinishshLevel();
        if(LevelManager.Instance.levelIndex == Levels.level29)
        {
            LevelManager.Instance.SpawnOnRestart();
        }
        OnStart();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Box")
        {
            if (IsBoxToSide(Vector2.left) || IsBoxToSide(Vector2.right))
            {
                isPushing = true;
                AudioManager.Instance.PlaySFX(pushSound);
                //anim.AnimationState.SetAnimation(0, "push", true);
            }
        }

        if (groundTags.Contains(collision.gameObject.tag))
        {
            if (collision.transform.position.y < transform.position.y && Mathf.Abs(rb.velocity.y) < 2f )
            {
                isJumpPressed = false;
                isGrounded = true;
                jumpCount = maxJumpCount;
                playerMovementInput.PlayerMove.Jump.started += Jump;
                playerMovementInput.PlayerMove.Jump.performed += Jump;
                if (!canJumpManyTimes)
                {
                    jumpCount = 1;
                }
                //if (isMovementPressed)
                //{
                //    anim.AnimationState.SetAnimation(0, "run", true);
                //}
                //else
                //{
                //    anim.AnimationState.SetAnimation(0, "idle", true);
                //}
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
            isPushing = false;
            AudioManager.Instance.StopSFX(pushSound);
            //if (isMovementPressed && !isJumpPressed)
            //{
            //    anim.AnimationState.SetAnimation(0, "run", true);
            //}
            //else
            //{
            //    anim.AnimationState.SetAnimation(0, "idle", true);
            //}
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
        EnableInput();

        GameEvents.onLevelStart += OnStart;
        GameEvents.onLevelFinish += OnFinishshLevel;
        GameEvents.onLevelPause += OnPause;
        GameEvents.onLevelResume += OnResume;
        GameEvents.onLevelRestart += OnRestart;
    }
    private void OnDisable()
    {
        DisableInput();

        GameEvents.onLevelStart -= OnStart;
        GameEvents.onLevelFinish -= OnFinishshLevel;
        GameEvents.onLevelPause -= OnPause;
        GameEvents.onLevelResume -= OnResume;
        GameEvents.onLevelRestart -= OnRestart;
    }

    void EnableInput()
    {
        playerMovementInput.Enable();
        playerMovementInput.PlayerMove.Jump.started += Jump;
        playerMovementInput.PlayerMove.Jump.performed += Jump;

        playerMovementInput.PlayerMove.Left_Right_Movement.started += Move;
        playerMovementInput.PlayerMove.Left_Right_Movement.performed += Move;
        playerMovementInput.PlayerMove.Left_Right_Movement.canceled += Move;
    }
    void DisableInput()
    {
        playerMovementInput.PlayerMove.Jump.started -= Jump;
        playerMovementInput.PlayerMove.Jump.performed -= Jump;

        playerMovementInput.PlayerMove.Left_Right_Movement.started -= Move;
        playerMovementInput.PlayerMove.Left_Right_Movement.performed -= Move;
        playerMovementInput.PlayerMove.Left_Right_Movement.canceled -= Move;
    }

}
