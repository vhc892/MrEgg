using Hapiga.Ads;
using Hapiga.Tracking;
using Helper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.SimpleLocalization.Scripts;


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
        if (GameConfig.Instance.Session <= 1)
        {
            LoadLevel(0);
        }
        if(gameConfig.gameData.Session > 1)
        {
            AdManager.Instance.ShowAppOpenAds();
        }
    }

    public void LoadLevel(int index)
    {
        //int level = index % 10;
        GameMode = GameMode.Gameplay;
        levelManager.LoadLevel(index%30); 
        player.startPosition = levelManager.levelData.levelPrefabs[index%30].startPos;
        player.enabled = true;
        Time.timeScale = 1;
        GameEvents.LevelStart();
        Debug.LogError(LocalizationManager.Language);
        TrackingManager.TrackEvent(FirebaseParamater.START_LEVEL, FirebaseParamater.LEVEL, (GameConfig.Instance.CurrentLevel + 1).ToString());
    }

    private void RestartLevel()
    {
        /*LoadLevel(gameConfig.CurrentLevel); */
        AudioManager.Instance.PlaySFX("RestartLevel");
        GameMode = GameMode.Gameplay;
        levelManager.LoadLevel(GameConfig.Instance.CurrentLevel);
        player.startPosition = levelManager.levelData.levelPrefabs[GameConfig.Instance.CurrentLevel].startPos;
        player.enabled = true;
        Time.timeScale = 1;
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
        GameEvents.onLevelPause += PauseGame;
        GameEvents.onLevelResume += ResumeGame;
        GameEvents.onLevelRestart += RestartLevel;

    }

    private void OnDisable()
    {
        GameEvents.onLevelPause -= PauseGame;
        GameEvents.onLevelResume -= ResumeGame;
        GameEvents.onLevelRestart -= RestartLevel;

    }
}
