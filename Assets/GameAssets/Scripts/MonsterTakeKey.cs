using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTakeKey : MonoBehaviour
{
    public float speed = 2f;
    public bool isHolding;
    public bool canMove;
    private Vector3 targetPosition;

    private void Start()
    {   
        targetPosition = transform.position;
    }

    private void Update()
    {
        if (isHolding && !canMove)
        {
            return;
        }
        if (Vector2.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }

    public void MoveToPosition(Transform target)
    {
        if (targetPosition != target.position)
        {
            targetPosition = target.position;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Key")
        {
            collision.transform.SetParent(transform);
            collision.transform.localPosition = new Vector3(-1, 0, 0);
        }
    }
}
