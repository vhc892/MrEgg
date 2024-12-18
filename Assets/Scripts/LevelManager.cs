using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public GameObject[] levelPrefab;
    private GameObject currentLevelPrefab;
    public int levelIndex = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        levelIndex = PlayerPrefs.GetInt("SelectedLevel", 0);
        LoadLevel(levelIndex);
    }
    public void LoadLevel(int level)
    {
        if (level < 0 || level >= levelPrefab.Length)
        {
            Debug.LogError($"Level index {level} is out of range. Available levels: 0 to {levelPrefab.Length - 1}");
            return;
        }
        if (currentLevelPrefab != null)
        {
            Destroy(currentLevelPrefab);
        }
        currentLevelPrefab = Instantiate(levelPrefab[level - 1]);       
    }
    
}
