using UnityEngine;

public class InfinityGround : MonoBehaviour
{
    private float groundWidth;
    private Camera mainCamera;
    public float overlapAmount = 0.1f;

    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            groundWidth = spriteRenderer.bounds.size.x;
        }
        mainCamera = Camera.main;
    }

    void Update()
    {
        float cameraLeftEdge = mainCamera.transform.position.x - mainCamera.orthographicSize * mainCamera.aspect;
        float cameraRightEdge = mainCamera.transform.position.x + mainCamera.orthographicSize * mainCamera.aspect;

        if (transform.position.x + groundWidth / 2 < cameraLeftEdge)
        {
            Vector3 newPosition = transform.position;
            newPosition.x += (groundWidth  - overlapAmount)*3;
            transform.position = newPosition;
        }

        if (transform.position.x - groundWidth / 2 > cameraRightEdge)
        {
            Vector3 newPosition = transform.position;
            newPosition.x -= (groundWidth  - overlapAmount)*3;
            transform.position = newPosition;
        }
    }
}
