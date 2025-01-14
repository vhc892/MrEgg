using Helper;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetUpLevel : MonoBehaviour
{
    public Transform playerStartPosition;
    public TextMeshPro hintText;
    private LevelData data;

    private void Awake()
    {
        //hintText = GetComponentInChildren<TextMeshPro>();
        playerStartPosition = transform.Find("StartPos");
    }

    public void SetLevelData(LevelData data)
    {
        this.data = data;
        playerStartPosition.position = data.startPos;
    }
}
