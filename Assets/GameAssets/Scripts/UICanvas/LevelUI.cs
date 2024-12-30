using System.Collections;
using System.Collections.Generic;
using Hapiga.UI;
using UnityEngine;

public class LevelUI : BaseUI
{
    [SerializeField] LevelDisplay levelDisplay;
    public void SetInfor()
    {
        base.Show();
        levelDisplay.OnStart();
    }
    public void Close()
    {
        base.Hide();
    }
}
