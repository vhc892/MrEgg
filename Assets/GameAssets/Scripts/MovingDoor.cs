using UnityEngine;
using UnityEngine.EventSystems;

public class MovingDoor : MonoBehaviour , IPointerDownHandler, IPointerUpHandler
{
    public float detectionRadius = 5f;
    public float moveSpeed = 3f;
    private Transform player;
    private bool isPlayerNear = false;
    private bool isMouseHeld = false;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        isPlayerNear = distance <= detectionRadius;

        if (isPlayerNear)
        {
            MoveDoor();
        }
        if (isMouseHeld)
        {
            moveSpeed = Mathf.Max(0, moveSpeed - 0.15f);
        }
    }

    private void MoveDoor()
    {
        transform.position += new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        isMouseHeld = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        isMouseHeld = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.LevelCompleted();
        }
    }
}
