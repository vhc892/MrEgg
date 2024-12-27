using Helper;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetUpLevel : MonoBehaviour
{
    public Transform playerStartPosition;
    public TextMeshProUGUI hintText;
    private LevelData data;

    private void Awake()
    {
        hintText = GetComponentInChildren<TextMeshProUGUI>();
        playerStartPosition = transform.Find("StartPos");
    }

    public void SetLevelData(LevelData data)
    {
        this.data = data;
        playerStartPosition.position = data.startPos;
        hintText.text = data.hint;
    }
}
