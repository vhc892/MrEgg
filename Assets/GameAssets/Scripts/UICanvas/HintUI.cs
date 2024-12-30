using System.Collections;
using System.Collections.Generic;
using Hapiga.UI;
using TMPro;
using UnityEngine;

public class HintUI : BaseUI
{

    public TextMeshProUGUI askingText;
    public TextMeshProUGUI rewardHintText;

    public void Yes()
    {
        askingText.gameObject.SetActive(false);
        rewardHintText.gameObject.SetActive(true);
    }
    public void TurnOffHint()
    {
        askingText.gameObject.SetActive(true);
        rewardHintText.gameObject.SetActive(false);
    }

    public void GetHintText(int level)
    {
        rewardHintText.text = LevelManager.Instance.levelData.levelPrefabs[level].rewardHint;
    }
    public void SetInfor()
    {
        base.Show();
    }
    public void Close()
    {
        base.Hide();
    }
}
