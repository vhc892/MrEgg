using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfig : MonoBehaviour
{
    public static GameConfig Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this);
    }

    public GameData gameData;

    private void Start()
    {
        LoadData();
    }


    private int levelPass;
    private int currentLevel;
    public int LevelPass { get { return gameData.LevelPass; } set { gameData.LevelPass = value; } }
    public int CurrentLevel { get { return gameData.CurrentLevel; } set { gameData.CurrentLevel = value; } }


    void LoadData()
    {
        gameData = new GameData();
        PlayerData playerData = SaveSystemData.LoadPlayerData();

        if (playerData != null)
        {
            gameData.LevelPass = playerData.levelPass;
            gameData.CurrentLevel = playerData.currentLevel;
        }
        else
        {
            gameData.LevelPass = 0;
            gameData.CurrentLevel = 1;
        }
    }
}
