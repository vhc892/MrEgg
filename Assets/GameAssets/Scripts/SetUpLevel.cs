using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetUpLevel : MonoBehaviour
{
    public Transform playerStartPosition;
    public TextMeshProUGUI hintText;

    public string hintMessage;

    void Start()
    {
        GameManager.Instance.player.transform.position = playerStartPosition.position;
        if(hintText != null)
        {
            hintText.text = hintMessage;
        }
    }
}
