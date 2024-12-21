using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Camera Settings")]
    public float followSpeed = 2f;
    //public float yOffset = 0f;
    public Transform target;
    public bool isCameraFollow;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        if (!isCameraFollow || target == null) return;

        if (target.position.x >= 0f)
        {
            Vector3 targetPosition = new Vector3(
                Mathf.Lerp(transform.position.x, target.position.x, followSpeed * Time.deltaTime),
                startPosition.y, 
                -10f
            );

            if ((targetPosition - transform.position).sqrMagnitude > 0.001f)
            {
                transform.position = targetPosition;
            }
        }
    }

    public void ResetPosition()
    {
        transform.position = startPosition;
    }
}
