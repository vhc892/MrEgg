using Spine.Unity;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.LowLevel;

public class MovingDoor : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float detectionRadius = 5f;
    private float moveSpeed;
    private CharController player;
    private bool isMouseHold = false;
    private float startMoveSpeed;
    private bool moveDoor;
    private Vector3 lastPlayerPosition;
    private SkeletonAnimation anim;
    private Rigidbody2D rb;
    private bool stop;

    private enum DoorState { Idle, Run, Open, Idle2 }
    private DoorState currentDoorState;

    private void Awake()
    {
        anim = GetComponentInChildren<SkeletonAnimation>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        player = GameManager.Instance.player;
        moveSpeed = GameManager.Instance.player.movementSpeed;
        startMoveSpeed = moveSpeed;
        lastPlayerPosition = player.transform.position;

        //SetDoorAnimation(DoorState.Idle);
        currentDoorState = DoorState.Idle;
        anim.AnimationState.SetAnimation(0, "idle", true);
        Debug.Log(currentDoorState);
    }

    private void Update()
    {
        if (currentDoorState == DoorState.Open) return;
        if (player == null) return;

        float sqrDistance = (transform.position - player.transform.position).sqrMagnitude;
        if (sqrDistance <= detectionRadius * detectionRadius && !moveDoor)
        {
            moveDoor = true;
            SetDoorAnimation(DoorState.Run);
        }

        if (moveDoor)
        {
            //float playerMovementDelta = Mathf.Abs(player.transform.position.x - lastPlayerPosition.x);
            float playerMovementDelta = Mathf.Abs(player.RGBD.velocity.x);
            if (playerMovementDelta > 0)
            {
                if (currentDoorState != DoorState.Run)
                {
                    Debug.Log("run");
                    SetDoorAnimation(DoorState.Run);
                }
                
                if (player.RGBD.velocity.x > 0)
                {
                    DoorMoveRight();
                    FlipDoor(false); // Face right
                }
                else if (player.RGBD.velocity.x < 0)
                {
                    DoorMoveLeft();
                    FlipDoor(true); // Face left
                }
                //rb.velocity = player.RGBD.velocity;
                //if (player.position.x > lastPlayerPosition.x)
                //{
                //    DoorMoveRight();
                //    FlipDoor(false); // Face right
                //}
                //else if (player.position.x < lastPlayerPosition.x)
                //{
                //    DoorMoveLeft();
                //    FlipDoor(true); // Face left
                //}
            }
            else
            {
                if (currentDoorState != DoorState.Idle2)
                {
                    SetDoorAnimation(DoorState.Idle2);
                }
            }
        }

        lastPlayerPosition = player.transform.position;

        if (isMouseHold && moveSpeed > 0)
        {
            moveSpeed = Mathf.Max(0, moveSpeed - 0.5f);
        }
        if (!isMouseHold && stop == false)
        {
            moveSpeed = startMoveSpeed;
        }
    }
   
    private void DoorMoveRight()
    {
        transform.Translate(moveSpeed * Time.deltaTime, 0f, 0f);
    }

    private void DoorMoveLeft()
    {
        transform.Translate(-moveSpeed * Time.deltaTime, 0f, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            moveSpeed = 0f;
            SetDoorAnimation(DoorState.Open);
            GameEvents.LevelFinish();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (GameConfig.Instance.CurrentLevel.Equals(24))
        {
            return;
        }
        isMouseHold = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isMouseHold = false;
    }

    private void SetDoorAnimation(DoorState newState)
    {
        if (currentDoorState == newState)
        {
            return;
        }
        currentDoorState = newState;

        switch (newState)
        {
            case DoorState.Idle:
                anim.AnimationState.SetAnimation(0, "idle", true);
                break;
            case DoorState.Run:
                anim.AnimationState.SetAnimation(0, "run", true);
                break;
            case DoorState.Open:
                anim.AnimationState.SetAnimation(0, "open", false);
                break;
            case DoorState.Idle2:
                anim.AnimationState.SetAnimation(0, "idle2", true);
                break;
        }
    }

    private void FlipDoor(bool faceLeft)
    {
        Vector3 scale = transform.localScale;
        scale.x = faceLeft ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
        transform.localScale = scale;
    }
    private void Stopped()
    {
        stop = true;
        moveSpeed = 0f;
    }

    private void Resume()
    {
        if (GameConfig.Instance.CurrentLevel.Equals(24))
        {
            return;
        }
        moveSpeed = startMoveSpeed;
        stop = false;
    }
    private void OnEnable()
    {
        GameEvents.onLevelResume += Resume;
        GameEvents.onLevelPause += Stopped;
    }

    private void OnDisable()
    {
        GameEvents.onLevelResume -= Resume;
        GameEvents.onLevelPause -= Stopped;
    }
}