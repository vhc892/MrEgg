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
        //LoadLevel(GameConfig.Instance.CurrentLevel);
    }
    public void LoadLevel(int level)
    {

        if (transform.childCount > 0)
            Destroy(transform.GetChild(0).gameObject);

        cameraFollow.isCameraFollow = false;
        GameManager.Instance.player.canJumpManyTimes = false;

        if (level == 6)
            cameraFollow.isCameraFollow = true;

        if (level == 9)
            GameManager.Instance.player.canJumpManyTimes = true;


        if (level < 0 || level > levelData.levelPrefabs.Count)
        {
            Debug.Log(level);
            Debug.LogError($"Level index {level} is out of range. Available levels: 1 to {levelData.levelPrefabs.Count}");
            return;
        }
        if (currentLevelPrefab != null)
        {
            Destroy(currentLevelPrefab);
        }
        if (PlayerPrefs.GetInt("HasPlayed", 0) == 0)
        {
            currentLevelPrefab = Instantiate(levelData.levelPrefabs[0].prefabs, this.transform);
            PlayerPrefs.SetInt("HasPlayed", 1);
            PlayerPrefs.Save();
        }
        else
        {
            currentLevelPrefab = Instantiate(levelData.levelPrefabs[level].prefabs, this.transform);
        }
        SetUpLevel setUpLevel = currentLevelPrefab.GetComponent<SetUpLevel>();
        setUpLevel.SetLevelData(levelData.levelPrefabs[level]);

        //GameManager.Instance.buttonSequence.StartSequence();

        //UIManager.Instance.OnLevelLoaded();
    }
}
