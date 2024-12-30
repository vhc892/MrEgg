using UnityEngine;
using UnityEngine.EventSystems;

public class MovingDoor : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float detectionRadius = 5f;
    public float moveSpeed = 3f;
    private Transform player;
    private bool isMouseHold = false;
    private float startMovespeed;
    private bool moveDoor;
    private Vector3 lastPlayerPosition;

    private void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
        startMovespeed = moveSpeed;
        lastPlayerPosition = player.position;
    }

    private void Update()
    {
        if (player == null) return;

        float sqrDistance = (transform.position - player.position).sqrMagnitude;
        if (sqrDistance <= detectionRadius * detectionRadius && !moveDoor)
        {
            moveDoor = true;
        }

        if (Mathf.Abs(player.position.x - lastPlayerPosition.x) > 0.01f && moveDoor)
        {
            if (player.position.x > lastPlayerPosition.x)
            {
                DoorMoveRight();
            }
            else if (player.position.x < lastPlayerPosition.x)
            {
                DoorMoveLeft();
            }
        }

        lastPlayerPosition = player.position;

        if (isMouseHold && moveSpeed > 0)
        {
            moveSpeed = Mathf.Max(0, moveSpeed - 0.5f);
            Debug.Log(moveSpeed);
        }
        if (!isMouseHold)
        {
            moveSpeed = startMovespeed;
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
}
