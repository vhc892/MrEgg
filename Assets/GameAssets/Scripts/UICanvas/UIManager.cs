using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

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

    [SerializeField] GameObject startMenu;
    [SerializeField] LevelDisplay levelDisplay;

    public void OnLevelLoaded()
    {
        startMenu?.SetActive(false);
        levelDisplay?.gameObject.SetActive(false);
    }

    public void BackToMainMenu()
    {
        startMenu?.SetActive(true);
    }

    private void OnEnable()
    {
        GameEvents.onLevelStart += OnLevelLoaded;
    }

    private void OnDisable()
    {
        GameEvents.onLevelStart -= OnLevelLoaded;
    }
}
