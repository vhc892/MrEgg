using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorChild : MonoBehaviour
{
    private FinishDoor finishDoor;

    private void Awake()
    {
        finishDoor = GetComponentInParent<FinishDoor>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            finishDoor.OnFinish();
        }
    }
}
