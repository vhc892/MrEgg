using UnityEngine;
using UnityEngine.EventSystems;

public class MovingDoor : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float detectionRadius = 5f;
    public float moveSpeed = 3f;
    private Transform player;
    private bool isMouseHold = false;

    private void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
    }

    private void Update()
    {
        if (player == null) return;
        //distance 
        float sqrDistance = (transform.position - player.position).sqrMagnitude;
        if (sqrDistance <= detectionRadius * detectionRadius )
        {
            MoveDoor();
        }

        if (isMouseHold && moveSpeed > 0)
        {
            moveSpeed = Mathf.Max(0, moveSpeed - 2f * Time.deltaTime);
            Debug.Log(moveSpeed);
        }
    }

    private void MoveDoor()
    {
        transform.Translate(moveSpeed * Time.deltaTime, 0f, 0f);
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
