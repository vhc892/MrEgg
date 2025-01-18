using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingWall : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
        private bool isMoving = true;

    void Update()
    {
        if (isMoving)
        {
            transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Ground")
    //    {
    //        isMoving = false;
    //    }
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isMoving = false;
        }
    }
}
