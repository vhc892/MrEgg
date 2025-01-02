using Helper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject levelCompletePopup;
    public CharController player;
    //public ButtonSequence buttonSequence;

    private GameMode gameMode;
    public GameMode GameMode { get { return gameMode; } set { gameMode = value; } }

    GameConfig gameConfig;
    LevelManager levelManager;

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
        Input.multiTouchEnabled = false;
    }
    private void Start()
    {
        gameConfig = GameConfig.Instance;
        levelManager = LevelManager.Instance;
        LoadLevel(GameConfig.Instance.CurrentLevel);
        GameEvents.LevelStart();
    }

    public void LoadLevel(int index)
    {
        //int level = index % 10;
        GameMode = GameMode.Gameplay;
        levelManager.cameraFollow.ResetPosition();
        levelManager.LoadLevel(index); 
        player.startPosition = levelManager.levelData.levelPrefabs[index].startPos;
        player.enabled = true;
        Time.timeScale = 1;
    }

    private void RestartLevel()
    {
        LoadLevel(gameConfig.CurrentLevel);
    }

    public void PauseGame()
    {
        GameMode = GameMode.Pause;
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        GameMode = GameMode.Gameplay;
        Time.timeScale = 1;
    }

    private void OnEnable()
    {
        GameEvents.onLevelRestart += RestartLevel;
        GameEvents.onLevelPause += PauseGame;
        GameEvents.onLevelResume += ResumeGame;
    }

    private void OnDisable()
    {
        GameEvents.onLevelRestart -= RestartLevel;
        GameEvents.onLevelPause -= PauseGame;
        GameEvents.onLevelResume -= ResumeGame;
    }
}
