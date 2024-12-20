using UnityEngine;
using UnityEngine.EventSystems;

public class DragObjectOnXAxis : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isDragging = false;
    private Vector3 offset;

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - new Vector3(mousePosition.x, transform.position.y, transform.position.z);
        isDragging = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
    }

    private void Update()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePosition.x + offset.x, transform.position.y, transform.position.z);
        }
    }
}
