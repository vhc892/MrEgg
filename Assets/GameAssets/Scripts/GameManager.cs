using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
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
        player.ResetPosition();
        LevelManager.Instance.cameraFollow.ResetPosition();
        Debug.Log("Camera Position: " + LevelManager.Instance.cameraFollow.transform.position);
        Debug.Log("GameObject Position: " + player.transform.position);
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
