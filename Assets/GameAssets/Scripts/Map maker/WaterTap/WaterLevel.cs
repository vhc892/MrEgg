using UnityEngine;

public class WaterLevel : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float resetPositionX = 10f;
    [SerializeField] private float startPositionX = 0f;

    private float startPosX;

    void Start()
    {
        startPosX = startPositionX;
        transform.position = new Vector3(startPosX, transform.position.y, transform.position.z);
    }

    void Update()
    {
        float newPosX = Mathf.Repeat(Time.time * moveSpeed, resetPositionX);
        transform.position = new Vector3(startPosX + newPosX, transform.position.y, transform.position.z);
    }
}
