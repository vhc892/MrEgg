using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject levelCompletePopup;

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

    public void LevelCompleted()
    {
        if (levelCompletePopup != null)
        {
            levelCompletePopup.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
