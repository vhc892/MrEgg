using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public LevelPrefabData levelData;
    public static LevelManager Instance;
    private GameObject currentLevelPrefab;
    public CameraFollow cameraFollow;
    //public int levelIndex = 0;
    public TextMeshProUGUI levelText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        //levelIndex = PlayerPrefs.GetInt("SelectedLevel", 0);
        LoadLevel(GameConfig.Instance.CurrentLevel);
    }
    public void LoadLevel(int level)
    {
        if (transform.childCount > 0)
            Destroy(transform.GetChild(0).gameObject);

        cameraFollow.isCameraFollow = false;
        GameManager.Instance.player.canJumpManyTimes = false;

        if (level == 7)
            cameraFollow.isCameraFollow = true;

        if (level == 10)
            GameManager.Instance.player.canJumpManyTimes = true;


        if (level < 0 || level > levelData.levelPrefabs.Length)
        {
            Debug.Log(level);
            Debug.LogError($"Level index {level} is out of range. Available levels: 1 to {levelData.levelPrefabs.Length}");
            return;
        }
        if (currentLevelPrefab != null)
        {
            Destroy(currentLevelPrefab);
        }
        currentLevelPrefab = Instantiate(levelData.levelPrefabs[level - 1], this.transform);
        levelText.text = $"Level {level}";
        GameManager.Instance.buttonSequence.StartSequence();

        //UIManager.Instance.OnLevelLoaded();
        GameEvents.LevelStart();
    }

}
