using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelPointer : MonoBehaviour
{
    private Collider2D col;
    public Transform pivot; // The center of the circle
    public float radius = 5f; // Radius of the circle
    private Vector2 initialMousePosition; // For tracking the initial mouse position
    private bool isDragging = false; // To track if dragging is active

    public bool getKey = false;

    private void Start()
    {
        col = GetComponent<Collider2D>();
        col.enabled = false;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Convert mouse position to world position
            Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Check if the mouse is near the object
            if (Vector2.Distance(mouseWorldPosition, transform.position) < 0.5f) // Adjust tolerance as needed
            {
                initialMousePosition = mouseWorldPosition;
                isDragging = true;
            }
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            DragAlongCircle();
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }

    void DragAlongCircle()
    {
        // Convert mouse position to world space
        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate the direction from the pivot to the mouse
        Vector2 direction = mouseWorldPosition - (Vector2)pivot.position;
        direction.Normalize();

        // Set the object's position along the circle
        Vector2 newPosition = (Vector2)pivot.position + direction * radius;
        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);

        // Rotate the object to point toward the pivot
        Vector2 toPivot = (Vector2)pivot.position - newPosition; // Direction to the pivot
        float angle = Mathf.Atan2(toPivot.y, toPivot.x) * Mathf.Rad2Deg + 90; // Calculate angle
        transform.rotation = Quaternion.Euler(0, 0, angle); // Apply rotation
    }

    void OnWheelSpin()
    {
        col.enabled = true;
    }
    void OnWheelStop()
    {
        col.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "WheelKeyZone")
        {
            getKey = true;
        }
        else if (collision.gameObject.tag == "WheelRandomZone")
        {
            getKey = false;
        }
    }

    private void OnEnable()
    {
        LuckyWheel.onSpinWheel += OnWheelSpin;
        LuckyWheel.onStopWheel += OnWheelStop;
    }

    private void OnDisable()
    {
        LuckyWheel.onSpinWheel -= OnWheelSpin;
        LuckyWheel.onStopWheel -= OnWheelStop;
    }
}
