using System.Collections;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private FinishDoor door;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float stopDistance = 0.1f;

    private bool moveToDoor = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            moveToDoor = true;
            StartCoroutine(MoveToDoor());
        }
    }

    private IEnumerator MoveToDoor()
    {
        while (moveToDoor)
        {
            float sqrDistance = (door.transform.position - transform.position).sqrMagnitude;
            if (sqrDistance < stopDistance * stopDistance)
            {
                door.UnlockDoor();
                Destroy(gameObject);
                yield break;
            }

            transform.position = Vector2.MoveTowards(transform.position, door.transform.position, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
