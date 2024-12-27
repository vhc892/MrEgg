using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharChildCtrl : MonoBehaviour
{
    [HideInInspector] public Collider2D col;
    CharController charController;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        charController = GetComponentInParent<CharController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            charController.ReturnToStartPosition();

        }
    }
}
