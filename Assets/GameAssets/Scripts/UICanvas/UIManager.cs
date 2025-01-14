using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper;
using TMPro;
using Hapiga.Core.Runtime.Singleton;
using Hapiga.UI;

public class UIManager : UIManagerSingleton<UIManager>
{
    public LevelDisplay levelDisplay;
    public IngameUI ingameUI;
    public MenuUI menuUI;
    public LevelUI levelUI;

    //private int lightBulbCount;
    //[SerializeField] GameObject lightBulbFrame;
    //[SerializeField] TextMeshProUGUI lightBulbText;


    public override void Init()
    {
        base.Init();
    }
    public void ShowLevel()
    {
        AudioManager.Instance.PlaySFX("SelectButton");
        levelDisplay.Show();
        levelDisplay.OnStart();
    }

    public void ShowStartUI()
    {
        menuUI?.SetInfor();
        ingameUI?.Hide();
        levelDisplay.Hide();
    }
    public void HideLevel()
    {
        AudioManager.Instance.PlaySFX("SelectButton");
        levelDisplay.Hide();
    }

    public void OnLevelLoaded()
    {
        //startMenu?.Hide(true);
        GameEvents.onLevelFinish += FinishLevel;
        menuUI.Hide();
        HideLevel();
        ingameUI?.Show();
        ingameUI?.OnStart();
        AudioManager.Instance.IngameMusic();
    }

    public void BackToMainMenu()
    {
        GameEvents.onLevelFinish -= FinishLevel;
        ingameUI?.Hide();
        menuUI.SetInfor();
        levelUI?.SetInfor();
        GameEvents.ReturnToMenu();
        AudioManager.Instance.MenuMusic();
    }

    public void FinishLevel()
    {
        ingameUI?.OnLevelCompleted();
    }

    private void OnEnable()
    {
        GameEvents.onLevelStart += OnLevelLoaded;
        GameEvents.onLevelFinish += FinishLevel;
    }

    private void OnDisable()
    {
        GameEvents.onLevelStart -= OnLevelLoaded;
        GameEvents.onLevelFinish -= FinishLevel;
    }
}
