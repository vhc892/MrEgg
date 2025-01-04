using Hapiga.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasswordDoor : MonoBehaviour
{
    [SerializeField] UIPanel passwordPanel;
    FinishDoor finishDoor;

    private void Awake()
    {
        finishDoor = GetComponent<FinishDoor>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!finishDoor.Unlock)
            {
                passwordPanel.Show(true);
            }
        }
    }
}
