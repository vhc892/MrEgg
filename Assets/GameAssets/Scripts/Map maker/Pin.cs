using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Pin : MonoBehaviour, IPointerDownHandler
{
    LineRenderer lineRenderer;

    [SerializeField] PinnedDoor pinnedDoor;

    public event EventHandler<PinPressEventArgs> OnPinPress;
    public class PinPressEventArgs : EventArgs
    {
        public Vector2 position;
    }
    private void Start()
    {
        AddPhysics2DRaycaster();
        lineRenderer = GetComponent<LineRenderer>();

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
        eventData.pointerCurrentRaycast.gameObject.SetActive(false);
        OnPinPress?.Invoke(this, new PinPressEventArgs { position = eventData.pointerCurrentRaycast.worldPosition });
    }

    private void FixedUpdate()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, pinnedDoor.transform.position);
    }

    private void AddPhysics2DRaycaster()
    {
        Physics2DRaycaster physicsRaycaster = FindObjectOfType<Physics2DRaycaster>();
        if (physicsRaycaster == null)
        {
            Camera.main.gameObject.AddComponent<Physics2DRaycaster>();
        }
    }

}
