using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishText : MonoBehaviour
{
    public void EndLevel()
    {
        UIManager.Instance.ingameUI.LevelCompleted();
    }
}
