using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDeadLv24 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            UIManager.Instance.ingameUI.Restart();
        }
    }
}
