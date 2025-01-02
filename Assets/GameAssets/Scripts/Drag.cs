using Spine.Unity;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isDragging = false;
    private Vector3 offset;
    private Camera mainCamera;
    private Rigidbody2D rb;
    Vector3 oldPos;
    Vector3 newPos;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isDragging) return;
        if (mainCamera == null) return;

        rb.gravityScale = 0;

        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        oldPos = mousePosition;
        offset = (transform.position - new Vector3(mousePosition.x, mousePosition.y, transform.position.z)).normalized;
        isDragging = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        rb.gravityScale = 5;
        isDragging = false;
    }

    private void LateUpdate()
    {
        if (isDragging && mainCamera != null)
        {

            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            newPos = mousePosition - oldPos;
            rb.position += (Vector2)newPos;
            oldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            //lerp
            //transform.position = new Vector3(mousePosition.x + offset.x, mousePosition.y + offset.y, transform.position.z);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("trigger");
    }
}
