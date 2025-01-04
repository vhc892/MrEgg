using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidenDoor : MonoBehaviour
{
    private FinishDoor finishDoor;
    public SpriteRenderer[] door;

    private void Awake()
    {
        finishDoor = GetComponentInParent<FinishDoor>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerChild")
        {
            foreach (SpriteRenderer sprite in door)
            {
                sprite.enabled = true;
            }
        }
    }
}
