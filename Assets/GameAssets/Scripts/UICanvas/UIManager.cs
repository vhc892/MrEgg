using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper;
using TMPro;
using Hapiga.Core.Runtime.Singleton;
using Hapiga.UI;

public class UIManager : UIManagerSingleton<UIManager>
{

    public UIPanel startMenu;
    public LevelDisplay levelDisplay;
    public IngameUI ingameUI;
    public MenuUI menuUI;
    public LevelUI levelUI;


    public override void Init()
    {
        base.Init();
        DontDestroyOnLoad(this.gameObject);
    }
    public void ShowLevel()
    {
        levelUI.Show();
    }
    public void HideLevel()
    {
        levelUI.Hide();
    }
    private void Start()
    {
        //startMenu?.Show(true);
        //levelDisplay?.gameObject.SetActive(false);
        //ingameUI?.Hide();
    }

    public void OnLevelLoaded()
    {
        startMenu?.Hide(true);
        levelDisplay?.gameObject.SetActive(false);
        ingameUI?.Show();
        ingameUI?.OnStart();
    }

    public void BackToMainMenu()
    {
        startMenu?.Show(true);
    }

    private void OnEnable()
    {
        GameEvents.onLevelStart += OnLevelLoaded;
    }

    private void OnDisable()
    {
        GameEvents.onLevelStart -= OnLevelLoaded;
    }
}
