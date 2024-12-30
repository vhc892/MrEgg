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

    //private int lightBulbCount;
    //[SerializeField] GameObject lightBulbFrame;
    //[SerializeField] TextMeshProUGUI lightBulbText;


    public override void Init()
    {
        base.Init();
        DontDestroyOnLoad(this.gameObject);
    }
    public void ShowLevel()
    {
        levelUI.SetInfor();
    }
    public void HideLevel()
    {
        levelUI.Close();
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
        HideLevel();
        ingameUI?.Show();
        ingameUI?.OnStart();
    }

    public void BackToMainMenu()
    {
        ingameUI?.Hide();
        startMenu?.Show(true);
        levelUI?.SetInfor();
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
