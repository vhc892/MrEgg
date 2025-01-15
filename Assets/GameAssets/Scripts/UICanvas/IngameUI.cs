using Helper;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Hapiga.UI;
using UnityEngine.UI;
using Hapiga.Tracking;
using Hapiga.Ads;

public class IngameUI : BaseUI
{
    [SerializeField] UIPanel panelPause;
    [SerializeField] UIPanel languagePanel;
    [SerializeField] TextMeshProUGUI txtLevel;
    [SerializeField] UIPanel levelCompletePopup;
    [SerializeField] BlackScreenUI blackScreen;
    [SerializeField] HintSystem hintSystem;
    [SerializeField] UIPanel topButton;
    [SerializeField] UIPanel bottomButton;
    [SerializeField] ToggleUI[] toggleUIs;
    public Button[] allButtons;
    public Button[] playerControll;
    public Button backMenuBtn;

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

        bool isLevel20 = GameConfig.Instance.CurrentLevel == 20;
        foreach (Button button in playerControll)
        {
            button.gameObject.SetActive(!isLevel20);
        }

        bool isLevel25 = GameConfig.Instance.CurrentLevel == 25;
        foreach (Button button in allButtons)
        {
            button.gameObject.SetActive(!isLevel25);
        }

        txtLevel.text = "Level " + (GameConfig.Instance.CurrentLevel + 1);
        //txtLevel.text = (GameConfig.Instance.CurrentLevel + 1).ToString();

        panelPause.Hide(true);
        levelCompletePopup.Hide(true);

        nextLevel = currentLevel + 1;
        OnLevelLoaded();
    }

    #region LEVEL

    public void OnLevelLoaded()
    {
        topButton.Show(true);
        bottomButton.Show(true);
        EnableAllButtons();
    }
    public void OnLevelCompleted()
    {
        AudioManager.Instance.PlaySFX("Win");
        topButton.Show(false);
        bottomButton.Show(false);
        TrackingManager.TrackEvent(FirebaseParamater.END_LEVEL, FirebaseParamater.LEVEL, (GameConfig.Instance.CurrentLevel + 1).ToString());
        gameConfig.CurrentLevel = nextLevel;
        SaveSystemData.SavePlayer(GameConfig.Instance.gameData);

        levelCompletePopup?.Show(true);
    }

    public void NextLevelButton()
    {
        AudioManager.Instance.PlaySFX("SelectButton");
        //buttonSequence.DeactivateButtons();
        levelCompletePopup.Hide(true);
        gameManager.LoadLevel(currentLevel);

        if (GameConfig.Instance.CurrentLevel >= GameConfig.Instance.configData.level_show_ads)
            AdManager.Instance.ShowInterstitialAds("end_level");
    }
    public void BackMenu()
    {
        panelPause.Hide(true);
        levelCompletePopup.Hide(true);
        hintSystem.Hide();

        SaveSystemData.SavePlayer(GameConfig.Instance.gameData);
        uiManager.BackToMainMenu();
        AudioManager.Instance.PlaySFX("SelectButton");
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
        AudioManager.Instance.PlaySFX("SelectButton");
        backMenuBtn.gameObject.SetActive(true);
        panelPause.Show(true);
        GameEvents.LevelPause();   
        foreach (ToggleUI toggleUI in toggleUIs)
        {
            toggleUI.OnStart();
        }
    }
    public void PauseInMenu()
    {
        AudioManager.Instance.PlaySFX("SelectButton");
        backMenuBtn.gameObject.SetActive(false);
        panelPause.Show(true);
        GameEvents.LevelPause();
        foreach (ToggleUI toggleUI in toggleUIs)
        {
            toggleUI.OnStart();
        }
    }
    public void LanguageOpen()
    {
        AudioManager.Instance.PlaySFX("SelectButton");
        languagePanel.Show(true);
    }
    public void LanguageClose()
    {
        AudioManager.Instance.PlaySFX("SelectButton");
        languagePanel.Hide(true);
    }

    public void Unpause()
    {
        AudioManager.Instance.PlaySFX("SelectButton");
        GameEvents.LevelResume();
        panelPause.Hide(true);
    }
    #endregion 

    #region RESTART
    public void Restart()
    {
        AudioManager.Instance.PlaySFX("SelectButton");
        StartCoroutine(RestartSequence());
    }

    private IEnumerator RestartSequence()
    {
        blackScreen.SetInfor();

        yield return new WaitForSeconds(0.15f);

        GameEvents.LevelRestart();

        //yield return new WaitForSeconds(0.15f);

        blackScreen.Close();
    }

    #endregion

    #region TIPS
    public void ShowHint()
    {
        AudioManager.Instance.PlaySFX("ShowHintPanel");
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