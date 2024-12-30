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
    }

    public void LoadLevel(int index)
    {
        int level = index % 10;
        levelManager.cameraFollow.ResetPosition();
        levelManager.LoadLevel(level); 
        player.startPosition = levelManager.levelData.levelPrefabs[level%10].startPos;
        player.enabled = true;
        GameEvents.LevelStart();
    }

    private void RestartLevel()
    {
        LoadLevel(gameConfig.CurrentLevel);
    }

    private void OnEnable()
    {
        GameEvents.onLevelRestart += RestartLevel;
    }

    private void OnDisable()
    {
        GameEvents.onLevelRestart -= RestartLevel;
    }
}
