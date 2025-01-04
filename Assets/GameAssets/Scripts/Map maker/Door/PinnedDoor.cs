using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PinnedDoor : MonoBehaviour
{
    [SerializeField] List<Pin> pinPos;
    Rigidbody2D rb;

    private float pushForce = 1;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        foreach (var pin in pinPos)
        {
            pin.OnPinPress += Pin_OnPinPress;
        }
    }

    private void Pin_OnPinPress(object sender, Pin.PinPressEventArgs e)
    {
        rb.AddForce(((Vector2)transform.position - e.position) * pushForce, ForceMode2D.Impulse);
    }
}
