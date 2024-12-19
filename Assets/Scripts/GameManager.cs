using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject levelCompletePopup;
    public PlayerMovement player;

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
            levelCompletePopup.SetActive(true);
            Time.timeScale = 0f;
        }
        PlayerPrefs.SetInt("Level" + LevelManager.Instance.levelIndex + "Win", 1);
        PlayerPrefs.Save();
    }
    public void NextLevelButton()
    {
        player.ResetPosition();
        levelCompletePopup.SetActive(false);
        int level = ++LevelManager.Instance.levelIndex;
        LevelManager.Instance.LoadLevel(level);
        Time.timeScale = 1f;
    }
    public void BackMenu()
    {
        SceneManager.LoadScene(0);
    }
}
