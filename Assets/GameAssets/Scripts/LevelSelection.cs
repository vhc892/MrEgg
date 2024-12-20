using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{

    [SerializeField] private bool unlocked;
    public Image lockImage;

    private void Start()
    {
        //PlayerPrefs.DeleteAll();
    }

    private void Update()
    {
        UpdateLevelImage();//TODO MOve this method later
        UpdateLevelStatus();//TODO MOve this method later
    }

    private void UpdateLevelStatus()
    {
        int previousLevelIndex = int.Parse(gameObject.name) - 1;
        if (PlayerPrefs.GetInt("Level" + previousLevelIndex + "Win", 0) == 1)
        {
            unlocked = true;
        }
    }

    private void UpdateLevelImage()
    {
        if (!unlocked)
        {
            lockImage.gameObject.SetActive(true);
        }
        else
        {
            lockImage.gameObject.SetActive(false);
        }
    }

    public void PressSelection(int levelId)
    {
        if (unlocked)
        {
            PlayerPrefs.SetInt("SelectedLevel", levelId);
            PlayerPrefs.Save();
            SceneManager.LoadScene(1);
        }
    }

}