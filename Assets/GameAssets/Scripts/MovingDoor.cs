using Spine.Unity;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.LowLevel;

public class MovingDoor : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float detectionRadius = 5f;
    public float moveSpeed = 3f;
    private Transform player;
    private bool isMouseHold = false;
    private float startMoveSpeed;
    private bool moveDoor;
    private Vector3 lastPlayerPosition;
    private SkeletonAnimation anim;

    private enum DoorState { Idle, Run, Open, Idle2 }
    private DoorState currentDoorState;

    private void Awake()
    {
        anim = GetComponentInChildren<SkeletonAnimation>();
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
        startMoveSpeed = moveSpeed;
        lastPlayerPosition = player.position;

        //SetDoorAnimation(DoorState.Idle);
        currentDoorState = DoorState.Idle;
        anim.AnimationState.SetAnimation(0, "idle", true);
        Debug.Log(currentDoorState);
    }

    private void Update()
    {
        if (currentDoorState == DoorState.Open) return;
        if (player == null) return;

        float sqrDistance = (transform.position - player.position).sqrMagnitude;
        if (sqrDistance <= detectionRadius * detectionRadius && !moveDoor)
        {
            moveDoor = true;
            SetDoorAnimation(DoorState.Run);
        }

        if (moveDoor)
        {
            float playerMovementDelta = Mathf.Abs(player.position.x - lastPlayerPosition.x);

            if (playerMovementDelta > 0.01f)
            {
                if (currentDoorState != DoorState.Run)
                {
                    SetDoorAnimation(DoorState.Run);
                }

                if (player.position.x > lastPlayerPosition.x)
                {
                    DoorMoveRight();
                    FlipDoor(false); // Face right
                }
                else if (player.position.x < lastPlayerPosition.x)
                {
                    DoorMoveLeft();
                    FlipDoor(true); // Face left
                }
            }
            else
            {
                if (currentDoorState != DoorState.Idle2)
                {
                    SetDoorAnimation(DoorState.Idle2);
                }
            }
        }

        lastPlayerPosition = player.position;

        if (isMouseHold && moveSpeed > 0)
        {
            moveSpeed = Mathf.Max(0, moveSpeed - 0.5f);
        }
        if (!isMouseHold)
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
            UIManager.Instance.ingameUI.LevelCompleted();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
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
}
