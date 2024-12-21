using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public ButtonSequence buttonSequence;
    public static GameManager Instance;
    public GameObject levelCompletePopup;
    public CharController player;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LevelCompleted()
    {
        if (levelCompletePopup != null)
        {
            StartCoroutine(WaitWinPopUp());
        }
        PlayerPrefs.SetInt("Level" + LevelManager.Instance.levelIndex + "Win", 1);
        PlayerPrefs.Save();
    }
    private IEnumerator WaitWinPopUp()
    {
        yield return new WaitForSeconds(0.1f);
        levelCompletePopup.SetActive(true);
        Time.timeScale = 0f;
    }
    public void NextLevelButton()
    {
        buttonSequence.DeactivateButtons();
        player.ResetPosition();
        LevelManager.Instance.cameraFollow.ResetPosition();
        levelCompletePopup.SetActive(false);
        int level = ++LevelManager.Instance.levelIndex;
        LevelManager.Instance.LoadLevel(level);
        Time.timeScale = 1f;
        StartCoroutine(WaitAndActivateButton());
    }
    public void BackMenu()
    {
        SceneManager.LoadScene(0);
    }
    private IEnumerator WaitAndActivateButton()
    {
        yield return null;
        buttonSequence.StartSequence();
    }
}
