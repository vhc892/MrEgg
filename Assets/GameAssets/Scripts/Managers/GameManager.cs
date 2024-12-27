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

    }

    public void LoadLevel(int index)
    {
        levelManager.cameraFollow.ResetPosition();
        levelManager.LoadLevel(index); 
        player.startPosition = levelManager.levelData.levelPrefabs[index-1].startPos;
        player.enabled = true;
        GameEvents.LevelStart();
    }

    private void Start()
    {
        gameConfig = GameConfig.Instance;
        levelManager = LevelManager.Instance;
        LoadLevel(GameConfig.Instance.CurrentLevel);
    }
}
