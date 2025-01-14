using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Camera Settings")]
    public float followSpeed = 2f;
    //public float yOffset = 0f;
    public Transform target;
    public bool isCameraFollow;
    private bool canFollow;

    private Vector3 startPosition = new Vector3(0f, 2f, -10f);

    private void Start()
    {
    }

    private void Update()
    {
        if (!isCameraFollow || target == null) return;

        if (target.position.x >= 0f)
        {
            canFollow = true;
        }
        
        if (canFollow)
        {
            CameraFollowing();
        }
    }

    public void ResetPosition()
    {
        transform.position = startPosition;
        canFollow = false;
    }
    public void CameraFollowing()
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
    
    private void OnEnable()
    {
        GameEvents.onLevelStart += ResetPosition;
        GameEvents.onLevelRestart += ResetPosition;
    }
    private void OnDisable()
    {
        GameEvents.onLevelStart -= ResetPosition;
        GameEvents.onLevelRestart -= ResetPosition;

    }
}
