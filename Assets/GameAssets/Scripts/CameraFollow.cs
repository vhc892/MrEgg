using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Camera Settings")]
    public float followSpeed = 2f;
    //public float yOffset = 0f;
    public Transform target;
    public bool isCameraFollow;

    private Vector3 startPosition = new Vector3(0f, 2f, -10f);

    private void Start()
    {

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
