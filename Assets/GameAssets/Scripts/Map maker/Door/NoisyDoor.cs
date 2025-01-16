using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.UI;

public class NoisyDoor : MonoBehaviour
{
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();  
    }
    void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            UIManager.Instance.ingameUI.playerControll[1].GetComponent<OnScreenButton>().enabled = false;
        }
    }
    private void TurnOnPlayerControll()
    {
        UIManager.Instance.ingameUI.playerControll[1].GetComponent<OnScreenButton>().enabled = true;
        rb.GetComponent<BoxCollider2D>().enabled = false;
        Debug.Log("can move");
    }

    private void OnEnable()
    {
        GameEvents.onToggleMusic += TurnOnPlayerControll;
    }
    private void OnDisable()
    {
        GameEvents.onToggleMusic -= TurnOnPlayerControll;
    }
}
