using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float FollowSpeed = 2f;
    public float yOffset = 1f;
    public Transform target;
    public bool isCameraFollow;
    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }
    private void Update()
    {
        if (isCameraFollow)
        {
            if (target.position.x >= 0f)
            {
                Vector3 newPos = new Vector3(target.position.x, transform.position.y, -10f);
                transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
            }
        }
    }
    public void ResetPosition()
    {
        transform.position = startPosition;
    }
}