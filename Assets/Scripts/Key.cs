using System.Collections;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private FinishDoor door;
    [SerializeField] private float moveSpeed = 5f;

    private bool moveToDoor = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            moveToDoor = true;
        }
    }
    private void Update()
    {
        if (moveToDoor)
        {
            transform.position = Vector3.MoveTowards(transform.position, door.transform.position, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, door.transform.position) < 0.1f)
            {
                door.UnlockDoor();
                Destroy(gameObject);
            }
        }
    }
}
