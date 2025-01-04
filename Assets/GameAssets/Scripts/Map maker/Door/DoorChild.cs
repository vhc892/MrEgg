using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorChild : MonoBehaviour
{
    public Collider2D col;
    private FinishDoor finishDoor;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        finishDoor = GetComponentInParent<FinishDoor>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerChild")
        {
            finishDoor.OnFinish();
        }
    }
}
