using Helper;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Hapiga.UI;

public class IngameUI : BaseUI
{
    [SerializeField] UIPanel panelPause;
    [SerializeField] TextMeshProUGUI txtLevel;
    [SerializeField] UIPanel levelCompletePopup;
    // Start is called before the first frame update

    public void OnStart()
    {
        txtLevel.text = "Level " + GameConfig.Instance.CurrentLevel;
    }
    public void LevelCompleted()
    {
        GameEvents.LevelFinish();
        SaveSystemData.SavePlayer(GameConfig.Instance.gameData);

        levelCompletePopup?.Show(true);
    }

    public void NextLevelButton()
    {
        //buttonSequence.DeactivateButtons();
        levelCompletePopup.Hide(true);
        int level = ++GameConfig.Instance.CurrentLevel;
        GameManager.Instance.LoadLevel(level);
    }
    public void BackMenu()
    {
        SaveSystemData.SavePlayer(GameConfig.Instance.gameData);
        UIManager.Instance.BackToMainMenu();
        SceneManager.LoadScene(0);
    }

    #region PAUSE
    public void Pause()
    {
        panelPause.Show(true);
    }

    public void Unpause()
    {
        panelPause.Hide(true);
    }
    #endregion
}