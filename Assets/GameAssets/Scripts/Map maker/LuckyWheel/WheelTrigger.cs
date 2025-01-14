using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelTrigger : MonoBehaviour
{
    Collider2D col;
    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }
    void DisableCol()
    {
        col.enabled = false;
    }
    void EnableCol()
    {
        col.enabled = true;
    }
    private void OnEnable()
    {
        LuckyWheel.onSpinWheel += DisableCol;
        LuckyWheel.onStopWheel += EnableCol;
    }
    private void OnDisable()
    {
        LuckyWheel.onSpinWheel -= DisableCol;
        LuckyWheel.onStopWheel -= EnableCol;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerChild")
        {
            LuckyWheel.SpinWheel();
        }
    }
}
