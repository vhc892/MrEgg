using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject levelCompletePopup;
    public CharController player;
    public ButtonSequence buttonSequence;

    GameConfig gameConfig;

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

        gameConfig = GameConfig.Instance;
    }

    public void LevelCompleted()
    {
        SaveSystemData.SavePlayer(GameConfig.Instance.gameData);
        if (levelCompletePopup != null)
        {
            StartCoroutine(WaitWinPopUp());
        }
        Debug.Log("win");

    }
    private IEnumerator WaitWinPopUp()
    {
        yield return new WaitForSeconds(0.1f);
        levelCompletePopup.SetActive(true);
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.enabled = false;
    }
    public void NextLevelButton()
    {
        buttonSequence.DeactivateButtons();
        LevelManager.Instance.cameraFollow.ResetPosition();
        levelCompletePopup.SetActive(false);
        int level = ++GameConfig.Instance.CurrentLevel;
        LevelManager.Instance.LoadLevel(level);
        player.enabled = true;
    }
    public void BackMenu()
    {
        SaveSystemData.SavePlayer(GameConfig.Instance.gameData);
        UIManager.Instance.BackToMainMenu();
        SceneManager.LoadScene(0);
    }
}
