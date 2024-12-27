using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Balloon : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] GameObject lineTarget;
    [SerializeField] float upForce = 5;
    [SerializeField] private KnockEffect knockEffectPrefab;

    Rigidbody2D rb;
    LineRenderer lineRenderer;

    private void Awake()
    {
        AddPhysics2DRaycaster();
        rb = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void FixedUpdate()
    {
        rb.AddForce(Vector2.up * upForce, ForceMode2D.Force);
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, lineTarget.transform.position);
    }

    private void AddPhysics2DRaycaster()
    {
        Physics2DRaycaster physicsRaycaster = FindObjectOfType<Physics2DRaycaster>();
        if (physicsRaycaster == null)
        {
            Camera.main.gameObject.AddComponent<Physics2DRaycaster>();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        eventData.pointerCurrentRaycast.gameObject.SetActive(false);
        Vector3 position = transform.position;
        eventData.pointerCurrentRaycast.gameObject.SetActive(false);
        if (knockEffectPrefab != null)
        {
            KnockEffect knockEffect = Instantiate(knockEffectPrefab, position, Quaternion.identity);
            knockEffect.PlayKnockAnimation();
        }
    }
}
