using UnityEngine;
using UnityEngine.EventSystems;

public class DragObject : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isDragging = false;
    private Vector3 offset;
    private Camera mainCamera;
    

    public bool allowDragX = true;
    public bool allowDragY = true;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isDragging) return;
        if (mainCamera == null) return;

        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - new Vector3(mousePosition.x, transform.position.y, transform.position.z);
        isDragging = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
    }

    private void LateUpdate()
    {
        if (isDragging && mainCamera != null)
        {
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            float newX = allowDragX ? mousePosition.x + offset.x : transform.position.x;
            float newY = allowDragY ? mousePosition.y + offset.y : transform.position.y;
            transform.position = new Vector3(newX, newY, transform.position.z);
        }
    }
}
