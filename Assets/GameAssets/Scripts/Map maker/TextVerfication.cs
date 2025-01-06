using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextVerfication : MonoBehaviour
{
    [SerializeField] FinishDoor finishDoor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            finishDoor.transform.DOMove(finishDoor.transform.position + Vector3.up * 2, 1f);
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
