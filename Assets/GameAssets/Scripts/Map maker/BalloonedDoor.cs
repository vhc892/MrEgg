using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonedDoor : MonoBehaviour
{
    Rigidbody2D rb;
    Collider2D col;
    Collider2D childCol;
    bool isStopped;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        childCol = transform.GetChild(0).GetComponent<Collider2D>();
    }

    private void Stopped()
    {
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
        col.enabled = false;
    }

    private void Resume()
    {
        if (GameConfig.Instance.CurrentLevel.Equals(9))
        {
            return;
        }
        rb.isKinematic = false;
        col.enabled = true;
    }

    private void OnEnable()
    {
        GameEvents.onLevelResume += Resume;
        GameEvents.onLevelPause += Stopped;
    }

    private void OnDisable()
    {
        GameEvents.onLevelResume -= Resume;
        GameEvents.onLevelPause -= Stopped;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Stopped();
        }
    }
}

