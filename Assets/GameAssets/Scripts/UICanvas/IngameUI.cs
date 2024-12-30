using Helper;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Hapiga.UI;
using UnityEngine.UI;

public class IngameUI : BaseUI
{
    [SerializeField] UIPanel panelPause;
    [SerializeField] TextMeshProUGUI txtLevel;
    [SerializeField] UIPanel levelCompletePopup;
    [SerializeField] UIPanel blackScreen;
    [SerializeField] HintSystem hintSystem;
    [SerializeField] Button[] allButtons;

    GameManager gameManager;
    GameConfig gameConfig;
    UIManager uiManager;

    private int currentLevel;
    private int nextLevel;
    private void Awake()
    {
    }
    public void OnStart()
    {
        gameManager = GameManager.Instance;
        gameConfig = GameConfig.Instance;
        uiManager = UIManager.Instance;

        txtLevel.text = "Level " + (GameConfig.Instance.CurrentLevel + 1);
        panelPause.Hide(true);
        levelCompletePopup.Hide(true);

        nextLevel = currentLevel + 1;
        EnableAllButtons();
    }

    #region LEVEL
    public void LevelCompleted()
    {
        gameConfig.CurrentLevel = nextLevel;
        SaveSystemData.SavePlayer(GameConfig.Instance.gameData);

        levelCompletePopup?.Show(true);
        GameEvents.LevelFinish();
    }

    public void NextLevelButton()
    {
        //buttonSequence.DeactivateButtons();
        levelCompletePopup.Hide(true);
        gameManager.LoadLevel(currentLevel);
    }
    public void BackMenu()
    {
        panelPause.Hide(true);
        levelCompletePopup.Hide(true);
        hintSystem.Hide();

        SaveSystemData.SavePlayer(GameConfig.Instance.gameData);
        uiManager.BackToMainMenu();
        //SceneManager.LoadScene(0);
    }

    private void SetLevelIndex()
    {
        gameConfig = GameConfig.Instance;
        currentLevel = gameConfig.CurrentLevel;

        if (currentLevel > gameConfig.LevelPass)
        {
            gameConfig.LevelPass = currentLevel;
            Debug.Log(gameConfig.LevelPass);
        }
    }
    #endregion

    #region PAUSE

    public void EnableAllButtons()
    {
        foreach (var button in allButtons)
        {
            button.interactable = true;
        }
    }
    public void DisableAllButtons()
    {
        foreach (var button in allButtons)
        {
            button.interactable = false;
        }
    }
    public void Pause()
    {
        gameManager.GameMode = GameMode.Pause;
        GameEvents.LevelPause();   
        panelPause.Show(true);
    }

    public void Unpause()
    {
        panelPause.Hide(true);
        GameEvents.LevelResume();
    }
    #endregion

    #region RESTART
    public void Restart()
    {
        blackScreen.Show(true);
        GameEvents.LevelRestart();
        blackScreen.Hide(true);
    }
    #endregion

    #region TIPS
    public void ShowHint()
    {
        hintSystem.Show();
        hintSystem.SetUpLevelData(GameConfig.Instance.CurrentLevel);
    }
    #endregion


    private void OnEnable()
    {
        GameEvents.onLevelResume += EnableAllButtons;
        GameEvents.onLevelPause += DisableAllButtons;
        GameEvents.onLevelIndexChange += SetLevelIndex;
    }

    private void OnDisable()
    {
        GameEvents.onLevelResume -= EnableAllButtons;
        GameEvents.onLevelPause -= DisableAllButtons;
        GameEvents.onLevelIndexChange -= SetLevelIndex;
    }
}